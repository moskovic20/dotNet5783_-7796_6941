using DalApi;
using Do;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem myOrderItem)
    {
       
            int indexOfMyOrderItem = DataSource._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == myOrderItem.ID);

            if (indexOfMyOrderItem == -1) //myOrderItem.ID is not found in _OrderItems
        {
                myOrderItem.ID = DataSource.Config.NextOrderItem; 
                DataSource._OrderItems.Add(myOrderItem);
                return myOrderItem.ID;
            }

            if (DataSource._OrderItems[indexOfMyOrderItem].GetValueOrDefault().IsDeleted == false)
                throw new Exception("The order item you wish to add is already exists");

            DataSource._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;

        
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        int indexOfOItemById = DataSource._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (indexOfOItemById == -1)
            throw new Exception("The order item you wanted to delete is not found");


        OrderItem myOItem = DataSource._OrderItems[indexOfOItemById].GetValueOrDefault();

        if (myOItem.IsDeleted == true)
            throw new Exception("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        DataSource._OrderItems[indexOfOItemById] = (OrderItem?)myOItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (DataSource._OrderItems.FirstOrDefault() == null)
            throw new Exception("there is not any orderItem");

        IEnumerable<OrderItem?> allOrderItems =DataSource._OrderItems.FindAll(x=>true);
        return (IEnumerable<OrderItem>)allOrderItems;
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OItem=DataSource._OrderItems.Find(x=>x.GetValueOrDefault().ID==id
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
