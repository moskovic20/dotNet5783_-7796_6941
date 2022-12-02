using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//using BlApi;


namespace BlImplementation;

internal class BoProduct//: IProduct
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    IEnumerable<BO.ProductForList> GetAllProductForList()
    {
        var products = from P in dal.Product.GetAllExistsBy()
                       where P!=null
                       select BO.Tools.CopyPropertiesToNew(P,typeof( BO.ProductForList));

        if (products.Count() == 0)
            throw new Exception("");//לעשות חריגה מתאימה- אין מוצרים

        return (IEnumerable<ProductForList>)products;
    }

    BO.Product GetProductDetails_forM(int id)
    {
        try
        {
            Do.Product? myP = dal.Product.GetById(id) ?? throw new NullReferenceException("");

            return new BO.Product()
            {
                ID = myP.GetValueOrDefault().ID,
                Price = myP.GetValueOrDefault().Price,
                Category = myP.GetValueOrDefault().Category,
                InStock = myP.GetValueOrDefault().InStock,
            };
        }
        catch (Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this product", ex);
        }
    }

    void AddProduct(BO.Product productToAdd)
    {
        if (productToAdd.ID < 1)
            throw new BO.AddingProblemException("Negative ID");//מספר שלילי

        if (productToAdd.NameOfBook == "")
            throw new BO.AddingProblemException("Name of book is missing");

        if (productToAdd.Price < 0)
            throw new BO.AddingProblemException("Negative price");

        if (productToAdd.InStock < 0)
            throw new BO.AddingProblemException("Negative amount");


        try
        {
            Do.Product myNewP = new()
            {
                ID = productToAdd.ID,
                InStock = productToAdd.InStock,
                Category = productToAdd.Category,
                Price = productToAdd.Price,
                NameOfBook = productToAdd.NameOfBook,
                IsDeleted = false,
                AuthorName = null,
                path = null

            };

            dal.Product.Add(myNewP);
        }

        catch (Exception ex)
        {
            throw new BO.AddingProblemException("Can't add this product", ex);
        }

    }

    void DeleteProductByID(int id)
    {
        try
        {
            dal.Product.Delete(id);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.DeletedProblemException("Can't deleted this product", ex);
        }
    }

    BO.ProductItem GetProductDetails_forC(int id, BO.Cart cart)
    {
        try
        {
            Do.Product? myP = dal.Product.GetById(id);

            BO.ProductItem pForClient = new BO.ProductItem()
            {
                ID = myP.GetValueOrDefault().ID,
                Name = myP.GetValueOrDefault().NameOfBook,
                Price = myP.GetValueOrDefault().Price,
                Category = (BO.CATEGORY?)myP.GetValueOrDefault().Category,
                InStock = (myP.GetValueOrDefault().InStock > 0) ? true : false,
            };

            if (cart.Items == null)
            {
                pForClient.Amount = 0;
            }
            else
            {
                var myItems = from item in cart.Items
                              where item != null && item.ID == id
                              select item;
                pForClient.Amount = myItems.Count();

            }
            return pForClient;

        }
        catch ( Do.DoesntExistException ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this product", ex);
        }
    }

    void UpdateProductDetails(BO.Product productToUp)
    {
        if (productToUp.ID < 1)
            throw new BO.UpdateProblemException("Negative ID");//מספר שלילי

        if (productToUp.NameOfBook == "")
            throw new BO.UpdateProblemException("Name of book is missing");

        if (productToUp.Price < 0)
            throw new BO.UpdateProblemException("Negative price");

        if (productToUp.InStock < 0)
            throw new BO.UpdateProblemException("Negative amount");

        Do.Product DoProductToUp = new Do.Product()
        {
            ID = productToUp.ID,
            NameOfBook = productToUp.NameOfBook,
            AuthorName = null,
            Category = productToUp.Category,
            Price = productToUp.Price,
            InStock = productToUp.InStock,
            IsDeleted = false,
        };

        try
        {
            dal.Product.Update(DoProductToUp);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.UpdateProblemException("Can't update product", ex);
        }
    }
}
