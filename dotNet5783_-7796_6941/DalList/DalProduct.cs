using Do;
using DalApi;
using System.Collections.Generic;
using System.Text;

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
                throw new DoesntExistException("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = _DS._Products[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new DoesntExistException("the product doesn't exist");
    }

    public Product? GetById(int id)
    {
        Product? ProductById = _DS._Products.Find(x => x!=null&&x.GetValueOrDefault().ID ==
                                                        id && x.GetValueOrDefault().IsDeleted != true);

        if (ProductById==null)
            throw new DoesntExistException("the product is not found");///ok?

        return ProductById;
    }

    public void Update(Product? item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        try
        {
            GetById(item.GetValueOrDefault().ID);
        }
        catch
        {
            throw new DoesntExistException("the product can't be update because he doesn't exist");
        }
        Delete(item.GetValueOrDefault().ID);
        Add((Product)item);
    }

    private bool IdIsFound(int myID)
    {
        int indexOfSameId = _DS._Products.FindIndex(x => x.GetValueOrDefault().ID == myID);

        if (indexOfSameId == -1)
            return true;

        else
            return false;
    }

    public IEnumerable<Product> GetAll()
    {
        if (_DS._Products == null)
            throw new DoesntExistException("there is not any products");

        return (IEnumerable<Product>)_DS._Products;
    }

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת המוצרים (כולל אלו שנמחקו) לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotFounfException"></exception>
    public IEnumerable<Product?> GetAllBy(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return _DS._Products;

        var list = from item in _DS._Products
                   where item!=null&&filter(item)
                   select item;

        if (list.Count() == 0)
            throw new DoesntExistException("there is not any products");

        return list;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים הקיימים לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAllExistsBy(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return from item in _DS._Products
                    where item!=null&&item.Value.IsDeleted == false
                    select item;

        var list = from item in _DS._Products
                   where item.Value.IsDeleted == false
                   where filter(item)
                   select item;

        return list;
    }
}
