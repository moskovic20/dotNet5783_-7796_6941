﻿using Do;
using DalApi;
using System.Collections.Generic;
using System.Text;

namespace Dal;

internal class DalProduct : IProduct
{

    DataSource _DS = DataSource.Instance!;


    public int Add(Product P)
    {
        //P.IsDeleted = false;
        Random random = new Random();
        int indexOfMyP = _DS._Products.FindIndex(x => x?.ID == P.ID);

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
            throw new AlreadyExistException("The product ID you entered already exists in the database");

        _DS._Products.Add(P);
        return P.ID;

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
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new DoesntExistException("the product doesn't exist");
    }

    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.FirstOrDefault(x => x?.ID == id && x?.IsDeleted==false);

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

    private bool IdIsFound(int myID)
    {
        int indexOfSameId = _DS._Products.FindIndex(x => x?.ID == myID);

        if (indexOfSameId == -1)
            return true;

        else
            return false;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    => from item in _DS._Products
       where item != null
       where filter != null ? filter(item) : true
       select item;


    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת הפריטים (כולל אלו שנמחקו)-לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAlldeletted(Func<Product?, bool>? filter = null)
    => from item in _DS._Products
       where item !=null
       where filter != null ? filter(item): true
       select item;
}
