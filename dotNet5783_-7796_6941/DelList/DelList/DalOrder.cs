using DalApi;
using Do;

namespace Dal;

internal class DalOrder : IOrder
{

    public int Add(Order item)
    {
        try
        {
            Order myOrder = GetById(item.ID);
            if (myOrder.IsDeleted == false)
                throw new Exception("this item is already exists");




        }
        catch (Exception e)
        {
            if (e.Message == "this item is already exists")
                throw new Exception("this item is already exists");
        }

        DataSource._Order.Add(item);
        return item.ID;
    }

    public void Delete(int id)
    {
        int indexOfOrderById = DataSource._Order.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (indexOfOrderById == -1)
            throw new Exception("The order you wanted to delete is not found");


        Order myOrder = DataSource._Order[indexOfOrderById].GetValueOrDefault();

        if (myOrder.IsDeleted==true)
            throw new Exception("The order you wanted to delete has already been deleted");


        myOrder.IsDeleted = true;
        DataSource._Order[indexOfOrderById] = (Order?)myOrder;
    }

    public IEnumerable<Order> GetAll()
    {
        IEnumerable<Order?> allOrders= DataSource._Order.FindAll(x => true);
        return (IEnumerable<Order>)allOrders;
    }

    public Order GetById(int id)
    {
        Order? myOrder = DataSource._Order.Find(x => x.GetValueOrDefault().ID == id);

        if (myOrder == null)
            throw new Exception("The Order is not found");

        return (Order)myOrder;
    }

    public void Update(Order item)
    {
        try { GetById(item.ID); }

        catch
        {
            throw new Exception("the order you wish to update does not exist");
        }

        Delete(item.ID);
        Add(item);
    }
}
