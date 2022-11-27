using DalApi;
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
            throw new NotFounfException("The order you wanted to delete is not found\n");


        Order myOrder = _DS._Orders[indexOfOrderById].GetValueOrDefault();

        if (myOrder.IsDeleted == true)
            throw new NotFounfException("The order you wanted to delete has already been deleted\n");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = myOrder;
    }

    public IEnumerable<Order> GetAll()
    {
        if (_DS._Orders == null)
            throw new NotFounfException("there is not any orders");

        //IEnumerable<Order> allOrders = (IEnumerable<Order>)_DS._Orders.FindAll(x => x.GetValueOrDefault().IsDeleted == false);
        return (IEnumerable<Order>)_DS._Orders;
    }

    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.Find(x => x.GetValueOrDefault().ID == id && x.GetValueOrDefault().IsDeleted == false);

        if (myOrder.GetValueOrDefault().ID == 0)
            throw new NotFounfException("The Order is not found\n");

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
            throw new NotFounfException("the order you wish to update does not exist");
        }

        Delete(item.ID);
        Add(item);
    }
}