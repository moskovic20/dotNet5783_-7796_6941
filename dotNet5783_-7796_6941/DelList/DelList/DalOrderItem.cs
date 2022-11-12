
using DalApi;
using Do;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem item)
    {
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
            throw new Exception("The OrderItem is not found");

        return (OrderItem)OItem;
    }

    public void Update(OrderItem item)
    {
        throw new NotImplementedException();
    }
}
