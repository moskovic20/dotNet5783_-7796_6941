using DalApi;
using Do;

namespace Dal;

internal class DalOrder : IOrder
{
    DataSource _DS = DataSource.Instance!;

    public int Add(Order myOrder)
    {
        //Order temp = myOrder;

        int indexOfMyOrder = _DS._Orders.FindIndex(x => x?.OrderID == myOrder.OrderID);

        if (indexOfMyOrder == -1) //myOrder.OrderID is not found in _OrderS
        {
            myOrder.OrderID = DataSource.Config.NextOrderNumber;
            _DS._Orders.Add(myOrder);
            return myOrder.OrderID;
        }

        if (_DS._Orders[indexOfMyOrder]?.IsDeleted == false)
            throw new AlreadyExistException("The order you wish to add is already exists\n");

        _DS._Orders.Add(myOrder);
        return myOrder.OrderID;

    }

    public void Delete(int id)
    {
        int indexOfOrderById = _DS._Orders.FindIndex(x => x?.OrderID == id && x?.IsDeleted == false);

        if (indexOfOrderById == -1)
            throw new DoesntExistException("The order you wanted to delete is not found\n");


        Order myOrder = _DS._Orders[indexOfOrderById] ?? new();

        if (myOrder.IsDeleted == true)
            throw new DoesntExistException("The order you wanted to delete has already been deleted\n");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = myOrder;
    }

    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.FirstOrDefault(x => x?.OrderID == id && x?.IsDeleted == false);

        return myOrder ?? throw new DoesntExistException("The Order is not found\n"); ;
    }

    //public Order GetByCustomerName(string name)
    //{
    //    Order? myOrder = _DS._Orders.FirstOrDefault(x => x?.CustomerName == name && x?.IsDeleted == false);

    //    return myOrder ?? throw new DoesntExistException("The Order is not found\n"); ;
    //}

    public void Update(Order item)
    {
        try
        {
            GetById(item.OrderID);
        }
        catch
        {
            throw new DoesntExistException("the order you wish to update does not exist");
        }

        Delete(item.OrderID);
        Add(item);
    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת ההזמנות לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
   => from item in _DS._Orders
      where item != null
      where item?.IsDeleted == false
      where filter != null ? filter(item) : true
      select item;

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת ההזמנות (כולל הזמנות שנמחקו)-לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAlldeletted(Func<Order?, bool>? filter = null)
    => from item in _DS._Orders
       where filter is null ? true : item.Value.IsDeleted == true && filter(item)
       select item;

}