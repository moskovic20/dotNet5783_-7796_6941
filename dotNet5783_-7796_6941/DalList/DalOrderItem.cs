using Do;

using DalApi;

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
            throw new NotFounfException("The order item you wanted to delete is not found");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById].GetValueOrDefault();

        if (myOItem.IsDeleted == true)
            throw new NotFounfException("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = myOItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_DS._OrderItems == null)
            throw new NotFounfException("there is not any orderItem");

       
        return (IEnumerable<OrderItem>)_DS._OrderItems; //as IEnumerable<OrderItem>;
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID ==
                                                      id && x.GetValueOrDefault().IsDeleted != true);

        if (OItem.GetValueOrDefault().ID == 0)
            throw new NotFounfException("The order item is not found");

        return (OrderItem)OItem;
    }

    public void Update(OrderItem item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new NotFounfException("the order item can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    public List<OrderItem> GetListByOrderID(int OrderID)
    {
        List<OrderItem> list = new List<OrderItem>();

        //foreach (OrderItem OItem in _DS._OrderItems)
        //{
        //    if (OItem.IdOfOrder == OrderID && OItem.IsDeleted != true)
        //        list.Add(OItem);
        //}

        list = _DS._OrderItems.FindAll(x => x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().IdOfOrder == OrderID);
        if (list == null)
            throw new NotFounfException("The order items are not found or this order is't exist");
        return list;
    }

    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID == OrderID &&
                     x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().IdOfProduct == ProductID);

        if (OItem == null)
            throw new NotFounfException("The order item is not found");

        return (OrderItem)OItem;

    }
}

