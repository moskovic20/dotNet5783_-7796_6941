using DalApi;
using Do;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    DataSource _DS = DataSource.s_instance;

    public int Add(OrderItem myOrderItem)
    {

        int indexOfMyOrderItem = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == myOrderItem.ID);

        if (indexOfMyOrderItem == -1) //myOrderItem.ID is not found in _OrderItems
        {
            myOrderItem.ID = DataSource.Config.NextOrderItem;
            _DS._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;
        }

        if (_DS._OrderItems[indexOfMyOrderItem].GetValueOrDefault().IsDeleted == false)
            throw new Exception("The order item you wish to add is already exists");

        _DS._OrderItems.Add(myOrderItem);
        return myOrderItem.ID;


        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        int indexOfOItemById = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (indexOfOItemById == -1)
            throw new Exception("The order item you wanted to delete is not found");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById].GetValueOrDefault();

        if (myOItem.IsDeleted == true)
            throw new Exception("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = (OrderItem?)myOItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_DS._OrderItems.FirstOrDefault() == null)
            throw new Exception("there is not any orderItem");

        IEnumerable<OrderItem?> allOrderItems = _DS._OrderItems.FindAll(x => true);
        return (IEnumerable<OrderItem>)allOrderItems;
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID == id
                                                  && x.GetValueOrDefault().IsDeleted == false);

        if (OItem == null)
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
}
