using DalApi;
using Do;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    DataSource _DS = DataSource.Instance!;

    /// <summary>
    /// הוספת פריט הזמנה
    /// </summary>
    /// <param name="myOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Add(OrderItem myOrderItem)
    {
        int indexOfMyOrderItem = _DS._OrderItems.FindIndex(x => x?.ID == myOrderItem.ID);

        if (indexOfMyOrderItem == -1) //myOrderItem.OrderID is not found in _OrderItems
        {
            myOrderItem.ID = DataSource.Config.NextOrderItem;
            _DS._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;
        }

        if (_DS._OrderItems[indexOfMyOrderItem]?.IsDeleted == false)
            throw new AlreadyExistException("פריט ההזמנה שרצית להוסיף כבר קיים");

        _DS._OrderItems.Add(myOrderItem);
        return myOrderItem.ID;

    }

    /// <summary>
    /// מחיקת פריט הזמנה 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Delete(int id)
    {
        int indexOfOItemById = _DS._OrderItems.FindIndex(x => x?.ID == id && x?.IsDeleted != true);

        if (indexOfOItemById == -1)
            throw new DoesntExistException("פריט ההזמנה שרצית למחוק לא נמצא במערכת");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById] ?? new();

        if (myOItem.IsDeleted == true)
            throw new DoesntExistException("פריט ההזמנה שרצית למחוק-כבר נמחק מהמערכת");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = myOItem;
    }

    /// <summary>
    /// הפונקציה מקבלת מספר מזהה של פריט הזמנה ומחזירה פריט זה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistException"></exception>
    public OrderItem GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.FirstOrDefault(x => x?.ID == id && x?.IsDeleted != true);

        return OItem ?? throw new DoesntExistException("פריט ההזמנה שחיפשת לא נמצא במערכת");
    }

    /// <summary>
    /// עדכון של פריט בהזמנה
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Update(OrderItem item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new DoesntExistException("אי אפשר לעדכן פריט  הזמנה זה מכיוון שהוא לא נמצא במערכת");
        }
        Delete(item.ID);
        Add(item);
    }

    /// <summary>
    /// הפונקציה מקבלת מספר הזמנה ומחזירה רשימה של כל המוצרים שבהזמנה זו
    /// </summary>
    /// <param name="OrderID"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistException"></exception>
    public IEnumerable<OrderItem?> GetListByOrderID(int OrderID)
    {

        if (OrderID < 100000)
            throw new DoesntExistException("מספר הזמנה לא תקין");

        var list = from item in _DS._OrderItems
                   where item != null 
                   where (item?.OrderID == OrderID)
                   select item;

        if (list.Count() == 0)
            throw new DoesntExistException("לא נמצאו פריטי הזמנה או שהזמנה זו לא קיימת");

        return (IEnumerable<OrderItem?>)list;
    }

    /// <summary>
    /// הפונקציה מקבלת מזהה של מוצר ושל הזמנה ומחזירה את הפריט בהזמנה שמתאים לשני המזהים
    /// </summary>
    /// <param name="OrderID"></param>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistException"></exception>
    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem? OItem = _DS._OrderItems.FirstOrDefault(x => x.GetValueOrDefault().ID == OrderID &&
                     x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().ProductID == ProductID);

        if (OItem == null)
            throw new DoesntExistException("פריט ההזמנה לא נמצא ");

        return (OrderItem)OItem;

    }


    /// <summary>
    /// הפונקציה מחזירה את כל רשימת הפריטים בהזמנות לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    => from item in _DS._OrderItems
       where item != null
       where item?.IsDeleted == false
       where (filter != null) ? filter(item) : true
       select item;

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת הפריטים בהזמנות (כולל אלו שנמחקו)-לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAlldeleted(Func<Do.OrderItem?, bool>? filter = null)
    => from item in _DS._OrderItems
       where item != null
       where (filter==null)? true: filter(item)
       where item?.IsDeleted == true
       select item;
}

