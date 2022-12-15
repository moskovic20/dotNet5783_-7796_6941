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
        var products = dal.Product.GetAll();

        if (products.Count() == 0)
            throw new BO.GetAllForList_Exception("There are no products");

        return products.CopyListTo<Do.Product?, BO.ProductForList>();
    }

    public IEnumerable<BO.ProductForList> GetAllProductForList_forC()
    {//לסנן את מחיר
        var products = dal.Product.GetAll((Do.Product? p) => p?.Price == null ? false : true);

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

    public int AddProduct_forM(BO.Product productToAdd)
    {
        int id=0;

        try
        {
            if (productToAdd == null)
                throw new ArgumentNullException("missing product to add");

            if (productToAdd.ID < 1)
                throw new BO.Adding_Exception("Can't add because the negative ID");//מספר שלילי

            if (productToAdd.NameOfBook == null)
                throw new BO.Adding_Exception("Can't add because the name of book is missing");

            if (productToAdd.Price < 0)
                throw new BO.Adding_Exception("Can't add because the negative price");

            if (productToAdd.InStock < 0)
                throw new BO.Adding_Exception("Can't add because the negative amount");

            Do.Product myNewP = new();
            myNewP=productToAdd.CopyPropToStruct(myNewP);

            id = dal.Product.Add(myNewP);
            return id;
        }
        catch (Do.AlreadyExistException ex) { throw new BO.Adding_Exception("Can't add this product", ex); }
        catch (Exception) { }
        return id;
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
