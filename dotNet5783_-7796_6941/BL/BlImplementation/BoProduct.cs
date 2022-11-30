using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BoProduct: IProduct
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    IEnumerable<ProductForList> GetAllProductForList()
    {
        var products = from P in dal.Product.GetAllExistsBy()
                       select new ProductForList()
                       {
                           ProductID = P.GetValueOrDefault().ID,
                           Name=P.GetValueOrDefault().nameOfBook,
                           Price=P.GetValueOrDefault().Price,
                           Category= (BL_CATEGORY?)P.GetValueOrDefault().Category
                       };

        if (products.Count() == 0)
            throw new Exception();//חריגה של- אין מוצרים,לעדכןןןןן

        return products;
    }

    BO.Product GetProductByID(int id)
    {
        var product = from P in dal.Product.GetAllExistsBy()
                      select new BO.Product()
                      {
                         ProductID=P.GetValueOrDefault().ID,
                         Price=P.GetValueOrDefault().Price,
                         Category= P.GetValueOrDefault().Category,
                         InStock=P.GetValueOrDefault().InStock,
                      };

        return product.First();
    }

    void AddProduct(BO.Product P)
    {

        if (P.ProductID < 1)
            throw new Exception("");//מספר שלילי

        if (P.name == "")
            throw new Exception();//כנל

        if (P.Price < 0)
            throw new Exception("");//מספר שלילי

        if (P.InStock < 0)
            throw new Exception("");//מספר שלילי


        Do.Product myNewP = new()
        {
            ID = P.ProductID,
            InStock = P.InStock,
            Category = P.Category,
            Price = P.Price,
            nameOfBook = P.name,
            IsDeleted = false,
        };
        
        dal.Product.Add(myNewP);

    }

    void DeleteProductByID(int id)
    {
     
    }

    ProductItem GetProductByID_forC(int id, Cart cart)
    {
       
    }

    void UpdateProductDetails(BO.Product product)
    {
        
    }
}
