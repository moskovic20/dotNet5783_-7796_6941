﻿using DalApi;
using Do;

namespace Dal;

internal class DalProduct : IProduct
{

    DataSource _DS = DataSource.Instance!;


    //public int Add(Product P)
    //{
    //    //P.IsDeleted = false;
    //    Random random = new Random();
    //    int indexOfMyP = _DS._Products.FindIndex(x => x?.OrderID == P.OrderID);

    //    if (indexOfMyP == -1) //this product is not found in the data
    //    {
    //        while (!IdIsFound(P.OrderID)) //check if this id is not taken.
    //        {
    //            P.OrderID = random.Next(100000, 999999999);
    //        }

    //        _DS._Products.Add(P);
    //        return P.OrderID;
    //    }

    //    if (_DS._Products[indexOfMyP].GetValueOrDefault().IsDeleted != true)
    //        throw new AlreadyExistException("The product OrderID you entered already exists in the database");

    //    _DS._Products.Add(P);
    //    return P.OrderID;

    //}

    public int Add(Product P)
    {
        List<Product?> listOfThisID = _DS._Products.FindAll(x => x?.ID == P.ID);
        int indexOfExist = listOfThisID.FindIndex(x => x?.IsDeleted != true);

        if (indexOfExist == -1)
        {
            _DS._Products.Add(P);
            return P.ID;
        }
        else
            throw new AlreadyExistException("The product OrderID you entered already exists in the database");

    }

    public void Delete(int id)
    {
        int getIdOfProduct = _DS._Products.FindIndex(x => x?.ID == id && x?.IsDeleted != true);

        if (getIdOfProduct != -1)
        {
            if (_DS._Products[getIdOfProduct]?.IsDeleted == true)
                throw new DoesntExistException("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = _DS._Products[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                ChangingStatusOfProductIsdeleted.ProductImagePath = null;
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new DoesntExistException("the product doesn't exist");
    }

    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.FirstOrDefault(x => x?.ID == id && x?.IsDeleted == false);

        return ProductById ?? throw new DoesntExistException("the product is not found");
    }

    public Product GetByName(string name)
    {
        Product? ProductById = _DS._Products.FirstOrDefault(x => x?.NameOfBook==name && x?.IsDeleted == false);

        return ProductById ?? throw new DoesntExistException("the product is not found");
    }

    public void Update(Product item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new DoesntExistException("the product doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    => from item in _DS._Products
       where item != null
       where item?.IsDeleted == false
       where filter != null ? filter(item) : true
       select item;


    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת הפריטים (כולל אלו שנמחקו)-לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAlldeletted(Func<Product?, bool>? filter = null)
    => from item in _DS._Products
       where item != null
       where filter != null ? filter(item) : true
       select item;
}
