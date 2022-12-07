﻿using DalApi;
using Do;

namespace Dal;

internal class DalOrder : IOrder
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(Order myOrder)
    {
        Order temp = myOrder;//= false;

        int indexOfMyOrder = _DS._Orders.FindIndex(x => x.GetValueOrDefault().ID == myOrder.ID);

        if (indexOfMyOrder == -1) //myOrder.ID is not found in _OrderS
        {
            myOrder.ID = DataSource.Config.NextOrderNumber;
            _DS._Orders.Add(myOrder);
            return myOrder.ID;
        }

        if (_DS._Orders[indexOfMyOrder].GetValueOrDefault().IsDeleted == false)
            throw new AlreadyExistException("The order you wish to add is already exists\n");

        _DS._Orders.Add(myOrder);
        return myOrder.ID;

    }

    public void Delete(int id)
    {
        int indexOfOrderById = _DS._Orders.FindIndex(x => x.GetValueOrDefault().ID
                                    == id && x.GetValueOrDefault().IsDeleted == false);

        if (indexOfOrderById == -1)
            throw new DoesntExistException("The order you wanted to delete is not found\n");


        Order myOrder = _DS._Orders[indexOfOrderById].GetValueOrDefault();

        if (myOrder.IsDeleted == true)
            throw new DoesntExistException("The order you wanted to delete has already been deleted\n");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = myOrder;
    }

    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.Find(x => x.GetValueOrDefault().ID == id && x.GetValueOrDefault().IsDeleted == false);

        return myOrder ?? throw new DoesntExistException("The Order is not found\n"); ;
    }

    public void Update(Order? item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        try
        {
            GetById(item.GetValueOrDefault().ID);
        }
        catch
        {
            throw new DoesntExistException("the order you wish to update does not exist");
        }

        Delete(item.GetValueOrDefault().ID);
        Add((Order)item);
    }

    public IEnumerable<Order> GetAll()
    {
        if (_DS._Orders == null)
            throw new DoesntExistException("there is not any orders");

        return (IEnumerable<Order>)_DS._Orders;
    }

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת המוצרים (כולל אלו שנמחקו) לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotFounfException"></exception>
    public IEnumerable<Order?> GetAllBy(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
            return _DS._Orders;

        var list = from item in _DS._Orders
                   where item!=null && filter(item)
                   select item;

        if (list.Count() == 0)
            throw new DoesntExistException("there is not any orders");

        return list;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים בהזמנות הקיימים, לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAllExistsBy(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
            return from item in _DS._Orders
                    where item!=null&& item.Value.IsDeleted == false
                    select item;

        var list = from item in _DS._Orders
                   where item.Value.IsDeleted == false
                   where filter(item)
                   select item;

        return list;
    }

}