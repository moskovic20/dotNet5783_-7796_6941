﻿using Do;
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

    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.FirstOrDefault(x => x?.ID == id && x?.IsDeleted != true);

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
            throw new DoesntExistException("the product can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add((Product)item);
    }

    private bool IdIsFound(int myID)
    {
        int indexOfSameId = _DS._Products.FindIndex(x => x?.ID == myID);

        if (indexOfSameId == -1)
            return true;

        else
            return false;
    }

    public IEnumerable<Product> GetAll()
    {
        return (IEnumerable<Product>)_DS._Products?? throw new DoesntExistException("there is not any products");
    }

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת המוצרים (כולל אלו שנמחקו) לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotFounfException"></exception>
    public IEnumerable<Product> GetAllBy(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
        {
            var notFilterList = from item in _DS._Products
                                where item != null
                                select item;

            return (IEnumerable<Product>)notFilterList;
        }


        var Filterlist = from item in _DS._Products
                         where item!=null && filter(item)
                         select item;

        return (IEnumerable<Product>)Filterlist;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים הקיימים לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product> GetAllExistsBy(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
        {
            var notFilterList = from item in _DS._Products
                                where item != null && item.Value.IsDeleted == false
                                select item;

            return (IEnumerable<Product>)notFilterList;
        }


        var filterList= from item in _DS._Products
               where item != null && item.Value.IsDeleted == false && filter(item)
               select item;

        return (IEnumerable<Product>)filterList;
    }
}
