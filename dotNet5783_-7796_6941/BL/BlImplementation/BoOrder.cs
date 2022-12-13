using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class BoOrder : IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    //

    #region חריגות זמנים אופציונליות
    private static void datePosibleExceptiones(Do.Order? or)
    {

        if (or.GetValueOrDefault().DateOrder == null)
            throw new BO.InvalidValue_Exception("There is no date for creating an order");// בדיקות אם קיים בכלל עם מה לעבוד

        if (or.GetValueOrDefault().ShippingDate == null && or.GetValueOrDefault().DeliveryDate != null) //לא בטוח שצריך,לשים לב
            throw new BO.InvalidValue_Exception("order allredy delivered but there is no info about the shipping date");


        //----------------לא נכון בשלב שכבה זו לבדוק כאלה חריגות---------------------
        //if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DateOrder > or.GetValueOrDefault().ShippingDate)
        //    throw new ArgumentException("rong information,cant be possible that DateOrder > ShippingDate");

        //if (or.GetValueOrDefault().DeliveryDate != null && or.GetValueOrDefault().DateOrder > or.GetValueOrDefault().DeliveryDate)
        //    throw new ArgumentException("rong information,cant be possiblecant be possible that DateOrder > DeliveryDate");

        //if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DeliveryDate != null
        //                                                 && or.GetValueOrDefault().ShippingDate > or.GetValueOrDefault().DeliveryDate)
        //    throw new ArgumentException("rong information,cant be possible ShippingDate > DeliveryDate");

        if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DeliveryDate != null)//evrything allready got heandeled
            throw new ArgumentException("order allredy delivered");
    }
    #endregion

    /// <summary>
    /// הכנסת כל ההזמנות הלא ריקות לרשימה
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetAllOrderForList()
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
                        }
                        ?? throw new BO.GetAllForList_Exception("there is no orders in the list");
        return orderList;

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
            throw new BO.GetDetails_Exception("Negative ID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            BO.Order order = new BO.Order();
            order = myOrder.CopyPropTo(order);
            order.Status = myOrder.calculateStatus();
            order.PaymentDate = myOrder.DateOrder;//should be nullable?                                  
            order.Items = (List<BO.OrderItem?>?)dal.OrderItem.GetListByOrderID(myOrder.ID).Select(x => x.ListFromDoToBo()); //casting from list<do.ordetitem> to list<bo.orderitem> _________watch it's Tools___________
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
    public BO.Order UpdateOrderShipping(int id)
    {
        if (id < 0)
            throw new BO.Update_Exception("Negative ID");
        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            datePosibleExceptiones(myOrder);//exceptions

            if (myOrder.ShippingDate == null && myOrder.DeliveryDate == null) //____we can update like we was asked for____
            {

                if (myOrder.DateOrder > DateTime.Now)
                    throw new ArgumentException("rong information,cant be possible that DateOrder > ShippingDate"); //exceptions
                else
                {
                    myOrder.ShippingDate = DateTime.Now;//עדכון תאריך שליחה
                    dal.Order.Update(myOrder);//לעדכן גם את הבסיס נתונים בהתאם
                    return GetOrdertDetails(id);//החזרת ההזמנה המעודכנת//    לטפל בחריגות מהפונק הזאת   
                }
            }
            else
                throw new BO.GetDetails_Exception("Can't update this order correct ditales");//////ok exception?
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't update shipping date", ex);
        }
        catch (BO.InvalidValue_Exception ex)
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
            throw new BO.GetDetails_Exception("Negative ID");
        try
        {
            Do.Order myOrder = dal.Order.GetById(id);//בדיקות אם קיים בכלל...
            datePosibleExceptiones(myOrder);

            if (myOrder.ShippingDate != null && myOrder.DeliveryDate == null) //____we can update like we was asked for____
            {

                if (myOrder.ShippingDate > DateTime.Now)
                    throw new ArgumentException("rong information,cant be possible that ShippingDate > DeliveryDate");
                else
                {
                    myOrder.DeliveryDate = DateTime.Now;
                    dal.Order.Update(myOrder);
                    return GetOrdertDetails(id); ////לטפל בחריגות מהפונק הזאת  
                }
            }
            else
                throw new BO.GetDetails_Exception("Can't get this order correct ditales");//////ok exception?

        }
        catch (Exception ex)
        {
            throw new BO.GetDetails_Exception("Can't get this order", ex);
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
            throw new BO.GetDetails_Exception("Negative ID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            return new BO.OrderTracking()
            {
                ID = myOrder.ID,
                Status = myOrder.calculateStatus(),///------אופציה להוסיף כבונוס תאריך משוער למה שלא קיים לו ערך-------
                Tracking = myOrder.TrackingHealper()
            };
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't get this order", ex);
        }
    }

    public void UpdateOrder(int id)
    {
        throw new NotImplementedException();
    }
}