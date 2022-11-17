using DalApi;
using Do;
using System.Collections.Generic;

namespace Dal;

public class DalProduct : IProduct
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(Product P)
    {
        Random random = new Random();
        int indexOfMyP = _DS._Products.FindIndex(x => x.ID == P.ID);

        if (indexOfMyP == -1) //this product is not found in the data
        {
            while (!IdIsFound(P.ID)) //check if this id is not taken.
            {
                P.ID = random.Next(100000, 999999999);
            }

            _DS._Products.Add(P);
            return P.ID;
        }

        if (_DS._Products[indexOfMyP].IsDeleted == false)
            throw new Exception("The product you wish to add is already exists");

        _DS._Products.Add(P);
        return P.ID;

    }

    public void Delete(int id)
    {
        int getIdOfProduct = _DS._Products.FindIndex(x => x.ID == id);

        if (getIdOfProduct > 0)
        {
            if (_DS._Products[getIdOfProduct].IsDeleted == true)
                throw new Exception("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = _DS._Products[getIdOfProduct];
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new Exception("the product doesn't exist");
    }

    public IEnumerable<Product> GetAll()
    {

        if (_DS._Products == null)
            throw new Exception("there is not any products");

        IEnumerable<Product>? allProducts = _DS._Products.FindAll(x => x.IsDeleted == null);
        return allProducts;
    }

    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.Find(x => x.GetValueOrDefault().ID == id
                                                          && x.GetValueOrDefault().IsDeleted == false);

        if (ProductById == null)
            throw new Exception("the product is not found");///ok?

        return (Product)ProductById;
    }

    public void Update(Product item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new Exception("the product can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    private bool IdIsFound(int myID)
    {
        int indexOfSameId = _DS._Products.FindIndex(x => x.GetValueOrDefault().ID == myID);

        if (indexOfSameId == -1)
            return true;

        else
            return false;
    }
}
