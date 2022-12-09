﻿using DalApi;
using Do;
using System.Collections.Generic;

namespace Dal;

internal class DalOrder : IOrder
{
    DataSource _DS = DataSource.Instance();

    public int Add(Order myOrder)
    {
        Order temp = myOrder;

        int indexOfMyOrder = _DS._Orders.FindIndex(x => x?.ID == myOrder.ID);

        if (indexOfMyOrder == -1) //myOrder.ID is not found in _OrderS
        {
            myOrder.ID = DataSource.Config.NextOrderNumber;
            _DS._Orders.Add(myOrder);
            return myOrder.ID;
        }

        if (_DS._Orders[indexOfMyOrder]?.IsDeleted == false)
            throw new AlreadyExistException("The order you wish to add is already exists\n");

        _DS._Orders.Add(myOrder);
        return myOrder.ID;

    }

    public void Delete(int id)
    {
        int indexOfOrderById = _DS._Orders.FindIndex(x => x?.ID == id && x?.IsDeleted == false);

        if (indexOfOrderById == -1)
            throw new DoesntExistException("The order you wanted to delete is not found\n");


        Order myOrder = _DS._Orders[indexOfOrderById]?? new();

        if (myOrder.IsDeleted == true)
            throw new DoesntExistException("The order you wanted to delete has already been deleted\n");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = myOrder;
    }

    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.FirstOrDefault(x => x?.ID == id && x?.IsDeleted == false);

        return myOrder ?? throw new DoesntExistException("The Order is not found\n"); ;
    }

    public void Update(Order item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new DoesntExistException("the order you wish to update does not exist");
        }

        Delete(item.ID);
        Add(item);
    }

    public IEnumerable<Order> GetAll()
    {
        return (IEnumerable<Order>)_DS._Orders ?? throw new DoesntExistException("there is not any orders");
    }

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת המוצרים (כולל אלו שנמחקו) לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotFounfException"></exception>
    public IEnumerable<Order> GetAllBy(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
        {
            var notFilterList = from item in _DS._Orders
                                where item != null
                                select item;

            return (IEnumerable<Order>)notFilterList;
        }


        var Filterlist = from item in _DS._Orders
                         where item != null && filter(item)
                         select item;

        return (IEnumerable<Order>)Filterlist;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים בהזמנות הקיימים, לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Order> GetAllExistsBy(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
        {
            var notFilterList = from item in _DS._Orders
                                where item != null && item?.IsDeleted == false
                                select item;

            return (IEnumerable<Order>)notFilterList;
        }


        var Filterlist = from item in _DS._Orders
                         where item != null && item?.IsDeleted == false && filter(item)
                         select item;

        return (IEnumerable<Order>)Filterlist;
    }

}