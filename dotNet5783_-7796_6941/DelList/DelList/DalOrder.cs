using DalApi;
using Do;

namespace Dal;

public class DalOrder : IOrder
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(Order myOrder)
    {
        int indexOfMyOrder = _DS._Orders.FindIndex(x => x.GetValueOrDefault().ID == myOrder.ID);

        if (indexOfMyOrder == -1) //myOrder.ID is not found in _OrderS
        {
            myOrder.ID = DataSource.Config.NextOrderNumber;
            _DS._Orders.Add(myOrder);
            return myOrder.ID;
        }

        if (_DS._Orders[indexOfMyOrder].GetValueOrDefault().IsDeleted == false)
            throw new Exception("The order you wish to add is already exists");

        _DS._Orders.Add(myOrder);
        return myOrder.ID;

    }

    public void Delete(int id)
    {
        int indexOfOrderById = _DS._Orders.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (indexOfOrderById == -1)
            throw new Exception("The order you wanted to delete is not found");


        Order myOrder = _DS._Orders[indexOfOrderById].GetValueOrDefault();

        if (myOrder.IsDeleted == true)
            throw new Exception("The order you wanted to delete has already been deleted");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = (Order?)myOrder;
    }

    public IEnumerable<Order> GetAll()
    {
        if (_DS._Orders.FirstOrDefault() == null)
            throw new Exception("there is not any orders");

        IEnumerable<Order?> allOrders = _DS._Orders.FindAll(x => true);
        return (IEnumerable<Order>)allOrders;
    }

    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.Find(x => x.GetValueOrDefault().ID == id
                                                  && x.GetValueOrDefault().IsDeleted == false);

        if (myOrder == null)
            throw new Exception("The Order is not found");

        return (Order)myOrder;
    }

    public void Update(Order item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new Exception("the order you wish to update does not exist");
        }

        Delete(item.ID);
        Add(item);
    }
}
