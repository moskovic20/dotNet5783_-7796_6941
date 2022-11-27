using Do;
using DalApi;
using System.Collections.Generic;

namespace Dal;

internal class DalProduct : IProduct
{

    DataSource _DS = DataSource.GetInstance();


    public int Add(Product P)
    {
        P.IsDeleted = false;
        Random random = new Random();
        int indexOfMyP = _DS._Products.FindIndex(x => x.GetValueOrDefault().ID == P.ID);

        if (indexOfMyP == -1) //this product is not found in the data
        {
            while (!IdIsFound(P.ID)) //check if this id is not taken.
            {
                P.ID = random.Next(100000, 999999999);
            }

            _DS._Products.Add(P);
            return P.ID;
        }

        if (_DS._Products[indexOfMyP].GetValueOrDefault().IsDeleted != true)
            throw new AlreadyExistException("The product you wish to add is already exists");

        _DS._Products.Add(P);
        return P.ID;

    }

    public void Delete(int id)
    {
        int getIdOfProduct = _DS._Products.FindIndex(x => x.GetValueOrDefault().ID == id && x.GetValueOrDefault().IsDeleted != true);

        if (getIdOfProduct != -1)
        {
            if (_DS._Products[getIdOfProduct].GetValueOrDefault().IsDeleted == true)
                throw new NotFounfException("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = _DS._Products[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new NotFounfException("the product doesn't exist");
    }

    public IEnumerable<Product> GetAll()
    {

        if (_DS._Products == null)
            throw new NotFounfException("there is not any products");

        IEnumerable<Product> allProducts = (IEnumerable<Product>)_DS._Products.FindAll(x => x.GetValueOrDefault()
                                            .IsDeleted != true);
        return allProducts;
    }

    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.Find(x => x.GetValueOrDefault().ID ==
                                                        id && x.GetValueOrDefault().IsDeleted != true);

        if (ProductById.GetValueOrDefault().ID == 0)
            throw new NotFounfException("the product is not found");///ok?

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
            throw new NotFounfException("the product can't be update because he doesn't exist");
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
