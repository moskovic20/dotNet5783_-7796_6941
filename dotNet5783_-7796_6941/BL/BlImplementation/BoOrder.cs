using BlApi;
using BO;


namespace BlImplementation;

internal class BoOrder : BlApi.IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    /// <summary>
    /// רשימה של כל ההזמנות הקיימות
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetAllOrderForList()
    {
        try
        {
            var orderList = from O in dal.Order.GetAll()
                            let p = O.GetValueOrDefault()
                            select new BO.OrderForList()
                            {
                                OrderID = p.OrderID,
                                CustomerName = p.CustomerName,
                                Status = p.calculateStatus(),
                                AmountOfItems = p.CalculateAmountItems(),
                                TotalPrice = p.CalculatePriceOfAllItems()
                            };
            return orderList;
        }
        catch (Exception ex)
        {
            throw new BO.GetAllForList_Exception("cant give all the orders for list", ex);
        }
    }

    /// <summary>
    /// מחזיר אודרליסט לפי תז אחרי המרות כנדרש
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public OrderForList GetOrderForList(int orderID)
    {
        BO.Order order = GetOrdertDetails(orderID);
        OrderForList or = order.CopyPropTo(new OrderForList());
        or.AmountOfItems = order.Items!.Count();
        return or;
    }

    /// <summary>
    /// שליחת פרטי הזמנה לפי נתונים שמתאימים לשכבת הלוגיקה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    public BO.Order GetOrdertDetails(int id)
    {
        if (id < 0)
            throw new BO.GetDetails_Exception("Negative OrderID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            BO.Order order = new BO.Order();
            order = myOrder.CopyPropTo(order);
            order.Status = myOrder.calculateStatus();
            order.PaymentDate = myOrder.DateOrder;
            var tempItems = dal.OrderItem.GetListByOrderID(myOrder.OrderID);
            order.Items = tempItems?.Select(x => x.ListFromDoToBo()).ToList();
            order.TotalPrice = myOrder.CalculatePriceOfAllItems();
            return order;
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't get this order", ex);
        }
        catch (BO.InvalidValue_Exception ex)
        {
            throw new BO.GetDetails_Exception("Can't get this order", ex);
        }

    }

    /// <summary>
    /// עדכון שילוח הזמנה 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order UpdateOrderShipping(int id,DateTime? dt=null)
    {
        if (id < 0)
            throw new BO.Update_Exception("Negative OrderID");
        try
        {
            BO.Order UpOrd = new();
            UpOrd = dal.Order.GetById(id).CopyPropTo(UpOrd);

            if (UpOrd.DeliveryDate != null)
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר בוצעה");

            if (UpOrd.ShippingDate != null)
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר נשלחה");

            if (UpOrd.ShippingDate == null && UpOrd.DeliveryDate == null) //we can update date like we was asked for
            {

                //if (UpOrd.DateOrder > DateTime.Now)
                //    throw new BO.InvalidValue_Exception("wrong information,cant be possible that DateOrder > ShippingDate");
                //else
                {
                    UpOrd.ShippingDate = (dt == null) ? DateTime.Now : dt;
                    Do.Order myOrder = new();
                    dal.Order.Update(UpOrd.CopyPropToStruct(myOrder));
                    return GetOrdertDetails(id);
                }
            }
            else
                throw new BO.GetDetails_Exception("Can't update this order correct ditales");
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't update shipping date", ex);
        }


    }

    /// <summary>
    /// עדכון הגעת הזמנה ליעד
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    public BO.Order UpdateOrderDelivery(int id, DateTime? dt = null)
    {

        if (id < 0)
            throw new BO.GetDetails_Exception("Negative OrderID");
        try
        {
            BO.Order UpOrd = new();
            UpOrd = dal.Order.GetById(id).CopyPropTo(UpOrd);

            if (UpOrd.DeliveryDate != null)
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר נשלחה");

            if (UpOrd.ShippingDate != null && UpOrd.DeliveryDate == null) //we can update like we was asked for
            {

                //if (UpOrd.ShippingDate > DateTime.Now)
                //    throw new BO.InvalidValue_Exception("Wrong information,cant be possible that ShippingDate > DeliveryDate");
                //else
                {
                    UpOrd.DeliveryDate = (dt == null) ? DateTime.Now : dt;
                    Do.Order myOrder = new();
                    dal.Order.Update(UpOrd.CopyPropToStruct(myOrder));
                    return GetOrdertDetails(id);
                }
            }
            else
                throw new BO.GetDetails_Exception("לא ניתן לעדכן, אין לנו את כל הפרטים הנדרשים");
        }
        catch (Do.AlreadyExistException ex)
        {
            throw new BO.GetDetails_Exception("לא יכול להגיע להזמנה זו", ex);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("לא יכול להגיע להזמנה זו", ex);
        }

    }

    /// <summary>
    /// אובייקט להסבר מצב תהליך ההזמנה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    public BO.OrderTracking GetOrderTracking(int id)
    {
        if (id < 0)
            throw new BO.GetDetails_Exception("Negative OrderID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            return new BO.OrderTracking()
            {
                OrderID = myOrder.OrderID,
                Status = myOrder.calculateStatus(),
                Tracking = myOrder.TrackingHealper()
            };
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("לא יכול להגיע להזמנה זו", ex);
        }
    }

    /// <summary>
    /// מחיקת הזמנה שהושלמה
    /// </summary>
    /// <param name="orderID"></param>
    public void DeleteOrder_forM(int orderID)
    {
        Order myO = GetOrdertDetails(orderID);
        myO?.Items?.ForEach(orderItem => dal.OrderItem.Delete(orderItem?.ID ?? 0));
        dal.Order.Delete(orderID);
    }

    /// <summary>
    /// ביטול הזמנה שהתקבלה
    /// </summary>
    /// <param name="orderID"></param>
    public void CancleOrder_forM(int orderID)
    {
        Order myO = GetOrdertDetails(orderID);


        //myO.Items.Select(orderItem => orderItem.UpdateInStockAfterDeleteO());
        if (myO.Items != null)
        {
            foreach (OrderItem? o in myO.Items)
                o?.UpdateInStockAfterDeleteO();
        }

        dal.Order.Delete(orderID);


    }

    /// <summary>
    /// החזרת כל ההזמנות של לקוח לפי שם
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<BO.OrderForList> GetAllOrderOfClaient(string name)
    {
        return from o in dal.Order.GetAll(x => x?.CustomerName?.Contains(name) ?? false)
               select GetOrderForList(o?.OrderID ?? throw new Exception("problem"));
    }

    /// <summary>
    ///מביא את כל ההזמנות שהמספר שהתקבל מוכל במספר ההזמנה שלהן
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetAllOrdersByNumber(int number)
    {
        var query = (from order in GetAllOrderForList()
                     where BlApi.Tools.ContainsNumber(number, order.OrderID)
                     select order).ToList();
        return query;
    }

    /// <summary>
    /// פונקציה שמחזירה את כל ההזמנות שנמחקו
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.Order> GetAllDeletedOrders()
    {
        return from order in dal.Order.GetAlldeleted()
               select new BO.Order()
               {
                   OrderID = order?.OrderID ?? 0,
                   CustomerAddress = order?.CustomerAddress,
                   CustomerEmail = order?.CustomerEmail,
                   CustomerName = order?.CustomerName,
                   DateOrder = order?.DateOrder ?? new DateTime(),
                   DeliveryDate = order?.DeliveryDate,
                   ShippingDate = order?.ShippingDate,
                   PaymentDate = order?.DateOrder ?? new DateTime(),
                   TotalPrice = order?.CalculatePriceOfAllItems() ?? -1,
                   Status = (BO.OrderStatus)order?.calculateStatus()!,
               };
    }

    /// <summary>
    /// פונקציה שמחזירה את כל פרטי ההזמנה שנמחקו.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderItem> GetAllDeletedOrderItems()
    {
        var list= from orderItem in dal.OrderItem.GetAlldeleted()
               select orderItem.CopyPropTo(new OrderItem());

        Func<OrderItem, bool>? initializationTotalPrice = (OrderItem item) => 
        {
            item.TotalPrice = item.AmountOfItems * item.PriceOfOneItem;
            return true;
        };

        return from item in list
               where initializationTotalPrice(item)
               select item;

    }

}