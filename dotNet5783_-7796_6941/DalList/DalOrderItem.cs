using Do;

using DalApi;
using System.Collections.Generic;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(OrderItem myOrderItem)
    {
        myOrderItem.IsDeleted = false;

        int indexOfMyOrderItem = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == myOrderItem.ID);

        if (indexOfMyOrderItem == -1) //myOrderItem.ID is not found in _OrderItems
        {
            myOrderItem.ID = DataSource.Config.NextOrderItem;
            _DS._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;
        }

        if (_DS._OrderItems[indexOfMyOrderItem].GetValueOrDefault().IsDeleted == false)
            throw new AlreadyExistException("The order item you wish to add is already exists");

        _DS._OrderItems.Add(myOrderItem);
        return myOrderItem.ID;

    }

    public void Delete(int id)
    {
        int indexOfOItemById = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == id && x.GetValueOrDefault().IsDeleted != true);  ///

        if (indexOfOItemById == -1)
            throw new DoesntExistException("The order item you wanted to delete is not found");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById].GetValueOrDefault();

        if (myOItem.IsDeleted == true)
            throw new DoesntExistException("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = myOItem;
    }

    public OrderItem? GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID ==
                                                      id && x.GetValueOrDefault().IsDeleted != true);

        if (OItem==null)
            throw new DoesntExistException("The order item is not found");

        return OItem;
    }

    public void Update(OrderItem? item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        try
        {
            GetById(item.GetValueOrDefault().ID);
        }
        catch
        {
            throw new DoesntExistException("the order item can't be update because he doesn't exist");
        }
        Delete(item.GetValueOrDefault().ID);
        Add((OrderItem)item);
    }

    public List<OrderItem?> GetListByOrderID(int OrderID)
    {
        List<OrderItem?> list = new List<OrderItem?>();

        list = _DS._OrderItems.FindAll(x => x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().IdOfOrder == OrderID);
        if (list == null)
            throw new DoesntExistException("The order items are not found or this order is't exist");
       
        return list;
    }

    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID == OrderID &&
                     x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().IdOfProduct == ProductID);

        if (OItem == null)
            throw new DoesntExistException("The order item is not found");

        return (OrderItem)OItem;

    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_DS._OrderItems == null)
            throw new DoesntExistException("there is not any order items");

        return (IEnumerable<OrderItem>)_DS._OrderItems;
    }

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת המוצרים (כולל אלו שנמחקו) לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotFounfException"></exception>
    public IEnumerable<OrderItem?> GetAllBy(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
            return _DS._OrderItems;

        var list = from item in _DS._OrderItems
                   where item != null && filter(item)
                   select item;

        if (list.Count() == 0)
            throw new DoesntExistException("there is not any order items");

        return list;
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים בהזמנות הקיימים, לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAllExistsBy(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
            return from item in _DS._OrderItems
                   where item!=null && item.Value.IsDeleted == false
                   select item;

        var list = from item in _DS._OrderItems
                   where item.Value.IsDeleted == false
                   where filter(item)
                   select item;

        return list;
    }
}

