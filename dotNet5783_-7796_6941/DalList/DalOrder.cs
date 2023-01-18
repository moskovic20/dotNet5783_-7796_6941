using DalApi;
using Do;

namespace Dal;

internal class DalOrder : IOrder
{
    DataSource _DS = DataSource.Instance!;

    /// <summary>
    /// הוספת הזמנה 
    /// </summary>
    /// <param name="myOrder"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Add(Order myOrder)
    {
        int indexOfMyOrder = _DS._Orders.FindIndex(x => x?.OrderID == myOrder.OrderID);

        if (indexOfMyOrder == -1) //myOrder.OrderID is not found in _OrderS
        {
            myOrder.OrderID = DataSource.Config.NextOrderNumber;
            _DS._Orders.Add(myOrder);
            return myOrder.OrderID;
        }

        if (_DS._Orders[indexOfMyOrder]?.IsDeleted == false)
            throw new AlreadyExistException("ההזמנה שאתה רוצה להוסיף כבר קיימת\n");

        _DS._Orders.Add(myOrder);
        return myOrder.OrderID;

    }

    /// <summary>
    ///מחיקת הזמנה
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Delete(int id)
    {
        int indexOfOrderById = _DS._Orders.FindIndex(x => x?.OrderID == id && x?.IsDeleted == false);

        if (indexOfOrderById == -1)
            throw new DoesntExistException("ההזמנה שרצית למחוק לא נמצאה במערכת\n");


        Order myOrder = _DS._Orders[indexOfOrderById] ?? new();

        if (myOrder.IsDeleted == true)
            throw new DoesntExistException("ההזמנה שרצית למחוק-כבר נמחקה\n");


        myOrder.IsDeleted = true;
        _DS._Orders[indexOfOrderById] = myOrder;
    }

    /// <summary>
    /// הפונקציה מקבלת מספר מוצר ומחזירה אותו
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistException"></exception>
    public Order GetById(int id)
    {
        Order? myOrder = _DS._Orders.FirstOrDefault(x => x?.OrderID == id && x?.IsDeleted == false);

        return myOrder ?? throw new DoesntExistException("ההזמנה לא נמצאה במערכת\n"); ;
    }

    /// <summary>
    /// עדכון ההזמנה
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Update(Order item)
    {
        try
        {
            GetById(item.OrderID);
        }
        catch
        {
            throw new DoesntExistException("ההזמנה שרצית לעדכן לא נמצאה במערכת");
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
    public IEnumerable<Order?> GetAlldeleted(Func<Do.Order?, bool>? filter = null)
    => from item in _DS._Orders
       where item != null
       where item?.IsDeleted == true
       where (filter==null)? true:filter(item)
       select item;

}