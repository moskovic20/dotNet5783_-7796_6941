using DalApi;
using Do;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(OrderItem myOrderItem)
    {

        int indexOfMyOrderItem = _DS._OrderItems.FindIndex(x => x.ID == myOrderItem.ID);

        if (indexOfMyOrderItem == -1) //myOrderItem.ID is not found in _OrderItems
        {
            myOrderItem.ID = DataSource.Config.NextOrderItem;
            _DS._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;
        }

        if (_DS._OrderItems[indexOfMyOrderItem].IsDeleted == false)
            throw new Exception("The order item you wish to add is already exists");

        _DS._OrderItems.Add(myOrderItem);
        return myOrderItem.ID;

    }

    public void Delete(int id)
    {
        int indexOfOItemById = _DS._OrderItems.FindIndex(x => x.ID == id && x.IsDeleted!= true);  ///

        if (indexOfOItemById == -1)
            throw new Exception("The order item you wanted to delete is not found");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById];

        if (myOItem.IsDeleted == true)
            throw new Exception("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = myOItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_DS._OrderItems == null)
            throw new Exception("there is not any orderItem");

        IEnumerable<OrderItem>? allOrderItems = _DS._OrderItems.FindAll(x => x.IsDeleted != true);
        return allOrderItems;
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.ID == id && x.IsDeleted != true); 

        if (OItem.Value.ID == 0)
            throw new Exception("The order item is not found");

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
            throw new Exception("the order item can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    public List<OrderItem> GetListByOrderID(int OrderID)
    {
        List<OrderItem>? list = new List<OrderItem>();

        foreach (OrderItem OItem in _DS._OrderItems)
        {
            if (OItem.IdOfOrder == OrderID && OItem.IsDeleted !=true)
                list.Add(OItem);
        }
        if (list == null)
                throw new Exception("The order items are not found or this order is't exist");
        return list;
    }

    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.IdOfOrder == OrderID && 
                                        x.IsDeleted != true && x.IdOfProduct== ProductID);

        if (OItem == null)
            throw new Exception("The order item is not found");

        return (OrderItem)OItem;

    }
}
