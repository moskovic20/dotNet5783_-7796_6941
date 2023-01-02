using BlApi;
using BO;

namespace BlImplementation;

internal class BoOrder : IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    /// <summary>
    /// הכנסת כל ההזמנות הלא ריקות לרשימה
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
                                OrderID = p.ID,
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
            var tempItems = dal.OrderItem.GetListByOrderID(myOrder.ID);
            order.Items = tempItems.Select(x => x.ListFromDoToBo()).ToList();//casting from list<do.ordetitem> to list<bo.orderitem> _________watch it in Tools__________
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

    //public BO.OrderForList 

    /// <summary>
    /// עדכון שילוח הזמנה 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order UpdateOrderShipping(int id)
    {
        if (id < 0)
            throw new BO.Update_Exception("Negative OrderID");
        try
        {
            BO.Order UpOrd = new();
            UpOrd = dal.Order.GetById(id).CopyPropTo(UpOrd);

            if (UpOrd.DeliveryDate != null)//evrything allready got heandeled
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר בוצעה");

            if (UpOrd.ShippingDate != null)//evrything allready got heandeled
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר נשלחה");

            if (UpOrd.ShippingDate == null && UpOrd.DeliveryDate == null) //____we can update like we was asked for____
            {

                if (UpOrd.DateOrder > DateTime.Now)
                    throw new BO.InvalidValue_Exception("wrong information,cant be possible that DateOrder > ShippingDate");
                else
                {
                    UpOrd.ShippingDate = DateTime.Now;//עדכון תאריך שליחה
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
    public BO.Order UpdateOrderDelivery(int id)
    {

        if (id < 0)
            throw new BO.GetDetails_Exception("Negative OrderID");
        try
        {
            // Do.Order myOrder = dal.Order.GetById(id);//בדיקות אם קיים בכלל...
            BO.Order UpOrd = new();
            UpOrd = dal.Order.GetById(id).CopyPropTo(UpOrd);

            if (UpOrd.DeliveryDate != null)//evrything allready got heandeled
                throw new BO.InvalidValue_Exception("לא יכול לעדכן הזמנה זו, מאחר והיא כבר נשלחה");

            if (UpOrd.ShippingDate != null && UpOrd.DeliveryDate == null) //____we can update like we was asked for____
            {

                if (UpOrd.ShippingDate > DateTime.Now)
                    throw new BO.InvalidValue_Exception("Wrong information,cant be possible that ShippingDate > DeliveryDate");
                else
                {
                    UpOrd.DeliveryDate = DateTime.Now;
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
                OrderID = myOrder.ID,
                Status = myOrder.calculateStatus(),///------אופציה להוסיף כבונוס תאריך משוער למה שלא קיים לו ערך-------
                Tracking = myOrder.TrackingHealper()
            };
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("לא יכול להגיע להזמנה זו", ex);
        }
    }

    /* 
     לבונוס - עדכון הזמנה (עבור מסך מנהל)
יאפשר הוספה \ הורדה \ שינוי כמות של מוצר בהזמנה ע"י 
    המנהל (שימו לב מתי מותר לעשות את זה!)
אין יותר פירוט (כי זה לבונוס) - אך 
    ניקוד הבונוס יינתן (בפרויקט הסופי) רק במקרה של 
    השלמת כל הפונקציונליות (כולל בשכבת התצוגה) בצורה מלאה.
 //throw new NotImplementedException("sorry, I'm not redy yet");
    */

    public void UpdateOrder(int id, int option)
    {
        //BO.Order ordToUp = new();
        //ordToUp=GetOrdertDetails(id);
        //switch(option){
        //    case 0:

        //}

        throw new NotImplementedException("sorry, I'm not redy yet");
    }

    public void DeleteOrder_forM(int orderID)
    {
        dal.Order.Delete(orderID);
    }

    public void CancleOrder_forM(int orderID)
    {
        Order myO = GetOrdertDetails(orderID);
     
        if (myO.Items==null)
        { 
            dal.Order.Delete(orderID);
            return;
        }

        //myO.Items.Select(orderItem => orderItem.UpdateInStockAfterDeleteO());

        foreach(OrderItem o in myO.Items)
        {
            o?.UpdateInStockAfterDeleteO();
        }


    }
}