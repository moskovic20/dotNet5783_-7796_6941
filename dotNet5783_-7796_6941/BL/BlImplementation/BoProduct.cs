using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using DalApi;
using BO;
using Do;

namespace BlImplementation;

internal class BoProduct//: IProduct
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    IEnumerable<ProductForList> GetAllProductForList()
    {
        var products = from P in dal.Product.GetAllExistsBy()
                       select new ProductForList()
                       {
                           ProductID = P.GetValueOrDefault().ID,
                           Name = P.GetValueOrDefault().NameOfBook,
                           Price = P.GetValueOrDefault().Price,
                           Category = (BL_CATEGORY?)P.GetValueOrDefault().Category
                       };

        if (products.Count() == 0)
            throw new Exception("");//לעשות חריגה מתאימה- אין מוצרים

        return products;
    }

    BO.Product GetProductDetails_forM(int id)
    {
        try
        {
            Do.Product? myP = dal.Product.GetById(id);

            return new BO.Product()
            {
                ID = myP.GetValueOrDefault().ID,
                Price = myP.GetValueOrDefault().Price,
                Category = myP.GetValueOrDefault().Category,
                InStock = myP.GetValueOrDefault().InStock,
            };
        }
        catch (DoesntExistException ex)
        {
            throw new GetDetailsProblemException("Can't get this product", ex);
        }
    }

    void AddProduct(BO.Product productToAdd)
    {
        if (productToAdd.ID < 1)
            throw new AddingProblemException("Negative ID");//מספר שלילי

        if (productToAdd.NameOfBook == "")
            throw new AddingProblemException("Name of book is missing");

        if (productToAdd.Price < 0)
            throw new AddingProblemException("Negative price");

        if (productToAdd.InStock < 0)
            throw new AddingProblemException("Negative amount");


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
            throw new AddingProblemException("Can't add this product", ex);
        }

    }

    void DeleteProductByID(int id)
    {
        try
        {
            dal.Product.Delete(id);
        }
        catch (DoesntExistException ex)
        {
            throw new DeletedProblemException("Can't deleted this product", ex);
        }
    }

    ProductItem GetProductDetails_forC(int id, Cart cart)
    {
        try
        {
            Do.Product? myP = dal.Product.GetById(id);

            BO.ProductItem pForClient = new BO.ProductItem()
            {
                ID = myP.GetValueOrDefault().ID,
                Name = myP.GetValueOrDefault().NameOfBook,
                Price = myP.GetValueOrDefault().Price,
                Category = (BL_CATEGORY?)myP.GetValueOrDefault().Category,
                InStock = (myP.GetValueOrDefault().InStock > 0) ? true : false,
            };

            if (cart.Items == null)
            {
                pForClient.Amount = 0;
            }
            else
                pForClient.Amount = cart.Items.Count(x =>  x.ID == id);


            return pForClient;

        }
        catch (DoesntExistException ex)
        {
            throw new GetDetailsProblemException("Can't get this product", ex);
        }
    }

    void UpdateProductDetails(BO.Product productToUp)
    {
        if (productToUp.ID < 1)
            throw new UpdateProblemException("Negative ID");//מספר שלילי

        if (productToUp.NameOfBook == "")
            throw new UpdateProblemException("Name of book is missing");

        if (productToUp.Price < 0)
            throw new UpdateProblemException("Negative price");

        if (productToUp.InStock < 0)
            throw new UpdateProblemException("Negative amount");

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
        catch (DoesntExistException ex)
        {
            throw new UpdateProblemException("Can't update product", ex);
        }
    }
}
