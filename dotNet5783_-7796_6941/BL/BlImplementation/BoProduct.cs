using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class BoProduct : IProduct
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    public IEnumerable<BO.ProductForList> GetAllProductForList_forM()
    {
        var products = from P in dal.Product.GetAll()
                       select P.CopyPropTo(typeof(BO.ProductForList));

        if (products.Count() == 0)
            throw new BO.GetAllForList_Exception("There are no products");

        return (IEnumerable<BO.ProductForList>)products;
    }

    public IEnumerable<BO.ProductForList> GetAllProductForList_forC()
    {
        var products = dal.Product.GetAll(); 

        if (products.Count() == 0)
            throw new BO.GetAllForList_Exception("There are no products");

        return products.CopyListTo<Do.Product?, BO.ProductForList>();
    }

    public BO.Product GetProductDetails_forM(int id)
    {
        try
        {
            Do.Product? myP = dal.Product.GetById(id);
            BO.Product BoMyP = new();
            myP.CopyPropertiesTo(BoMyP);
            return BoMyP;
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("cant give details of this product", ex);
        }
    }

    public void AddProduct_forM(BO.Product productToAdd)
    {

        if (productToAdd == null)
            throw new ArgumentNullException("missing product to add");

        if (productToAdd.ID < 1)
            throw new BO.Adding_Exception("Can't add becouse the negative ID");//מספר שלילי

        if (productToAdd.NameOfBook == null)
            throw new BO.Adding_Exception("Can't add becouse the name of book is missing");

        if (productToAdd.Price < 0)
            throw new BO.Adding_Exception("Can't add becouse the negative price");

        if (productToAdd.InStock < 0)
            throw new BO.Adding_Exception("Can't add becouse the negative amount");


        Do.Product myNewP = new();
        productToAdd.CopyPropToStruct(myNewP);

        try
        {
            dal.Product.Add(myNewP);
        }

        catch (Do.AlreadyExistException ex)  { throw new BO.Adding_Exception("Can't add this product", ex);}
        catch (Exception) { }
    }

    public void DeleteProductByID_forM(int id)
    {
        try
        {
            dal.Product.Delete(id);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.Deleted_Exception("Can't deleted this product", ex);
        }
    }

    public BO.ProductItem GetProductDetails_forC(int id, BO.Cart cart)
    {
        try
        {
            Do.Product myP = dal.Product.GetById(id);//הבאת המוצר הרצוי

            BO.ProductItem pForClient = new();
            myP.CopyPropertiesTo(pForClient);

            pForClient.InStock = (myP.InStock > 0) ? true : false;

            if (cart.Items == null)
            {
                pForClient.Amount = 0;
            }
            else
            {
                var myItems = from item in cart.Items
                              where item != null && item.ProductID == id
                              select item;
                pForClient.Amount = myItems.Count();

            }

            return pForClient;

        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't get this product", ex);
        }
    }

    public void UpdateProductDetails_forM(BO.Product productToUp)
    {
        if (productToUp == null)
            throw new ArgumentNullException("missing product");

        if (productToUp.ID < 1)
            throw new BO.Update_Exception("cant gets Negative ID");//מספר שלילי

        if (productToUp.NameOfBook == "")
            throw new BO.Update_Exception("Name of book is missing");

        if (productToUp.Price < 0)
            throw new BO.Update_Exception("cant gets Negative price");

        if (productToUp.InStock < 0)
            throw new BO.Update_Exception("cant gets Negative amount");

        Do.Product DoProductToUp = new();
        productToUp.CopyPropertiesTo(DoProductToUp);

        try
        {
            dal.Product.Update(DoProductToUp);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.Update_Exception("Can't update product", ex);
        }
    }
}
