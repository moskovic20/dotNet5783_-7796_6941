using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Do;
//using BO;

namespace BlImplementation;

internal class BoOrder: IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    #region   חישוב סטטוס להזמנה וזריקת חריגות
    private static BO.OrderStatus calculateStatus(DateTime? DateO, DateTime? ShippingD, DateTime? DeliveryD ) 
    {

        #region חריגות אפשריות בזמנים
        //if (DateO == null)
        //    throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        //if (ShippingD == null && DeliveryD == null)
        //    throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        //if (ShippingD == null && DeliveryD != null || ShippingD != null && DateO > ShippingD
        //           || DeliveryD != null && DateO > DeliveryD || ShippingD != null && DeliveryD != null && ShippingD > DeliveryD)
        //    throw new ArgumentException("rong information,cant be possible");          /////////exceptions
        #endregion

        ///-------------calculateStatus--------------

        if (ShippingD == null)
            return BO.OrderStatus.Pending;
        if (DeliveryD == null)
            return BO.OrderStatus.Processing;
        else
            return BO.OrderStatus.Completed;
    }

    #endregion

    /// <summary>
    /// הכנסת כל ההזמנות הלא ריקות לרשימה
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.OrderForList> GetAllOrderForList()
    {
        try
        {
            var orderList = from O in dal.Order.GetAllExistsBy()
                            select new BO.OrderForList()
                            {
                                OrderID = O.GetValueOrDefault().ID,
                                CuustomerName = O.GetValueOrDefault().NameCustomer,
                                Status = calculateStatus(O.GetValueOrDefault().DateOrder, O.GetValueOrDefault().ShippingDate, O.GetValueOrDefault().DeliveryDate),
                                AmountOfItems = BO.Tools.CalculateAmountItems(O),
                                TotalPrice = BO.Tools.CalculatePriceOfAllItems(O)

                            };
            return orderList;
        }
        catch (Exception ex)
        {
            throw new BO.GetAllForListProblemException("cant give all the orders for list", ex);
        }
    }

    BO.Order GetOrdertDetails(int id)
    {
        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");


        try
        {
            Do.Order? myOrder = dal.Order.GetById(id);
            return new BO.Order()
            {
                ID = myOrder.GetValueOrDefault().ID,
                Email = myOrder.GetValueOrDefault().Email,
                ShippingAddress = myOrder.GetValueOrDefault().ShippingAddress,
                DateOrder = myOrder.GetValueOrDefault().DateOrder,
                Status = calculateStatus(myOrder.GetValueOrDefault().DateOrder, myOrder.GetValueOrDefault().ShippingDate,
                                                                                  myOrder.GetValueOrDefault().DeliveryDate),
                PaymentDate = DateTime.MinValue,//לתקןןן!! לשים פה ערך תקין
                ShippingDate = (DateTime)myOrder.GetValueOrDefault().ShippingDate,

            };
        }
        catch(Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order",ex);
        }


    }

     //if (DateO == null)
     //       throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
     //   if (ShippingD == null && DeliveryD == null)
     //       throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
     //   if (ShippingD == null && DeliveryD != null || ShippingD != null && DateO > ShippingD
     //              || DeliveryD != null && DateO > DeliveryD || ShippingD != null && DeliveryD != null && ShippingD > DeliveryD)
     //       throw new ArgumentException("rong information,cant be possible");          /////////exceptions

    /// <summary>
    /// עדכון שילוח הזמנה 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Order UpdateOrderShipping(int id)
    {
        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");
        try
        {
            Do.Order? myOrder = dal.Order.GetById(id)/*?? throw new DoesntExistException("")*/;//בדיקות אם קיים בכלל...
            if (myOrder.GetValueOrDefault().DateOrder == null)
                throw new ArgumentNullException("cant update status, there is no info");// בדיקות אם קיים בכלל עם מה לעבוד
                        
            if (myOrder.GetValueOrDefault().ShippingDate == null && myOrder.GetValueOrDefault().DeliveryDate != null)
                throw new ArgumentException("order allredy delivered but there is no info about the shipping date");
            
            if ( myOrder.GetValueOrDefault().ShippingDate != null && myOrder.GetValueOrDefault().DateOrder > myOrder.GetValueOrDefault().ShippingDate)
                throw new ArgumentException("rong information,cant be possible that DateOrder > ShippingDate");
           
            if(myOrder.GetValueOrDefault().DeliveryDate != null && myOrder.GetValueOrDefault().DateOrder > myOrder.GetValueOrDefault().DeliveryDate)
                throw new ArgumentException("rong information,cant be possiblecant be possible that DateOrder > DeliveryDate");
             
            if(myOrder.GetValueOrDefault().ShippingDate != null && myOrder.GetValueOrDefault().DeliveryDate != null 
                                                             && myOrder.GetValueOrDefault().ShippingDate > myOrder.GetValueOrDefault().DeliveryDate)
                throw new ArgumentException("rong information,cant be possible ShippingDate > DeliveryDate");

            if (myOrder.GetValueOrDefault().ShippingDate != null && myOrder.GetValueOrDefault().DeliveryDate != null)//evrything allready got heandeled
                throw new ArgumentException("order allredy delivered");

            else //(ShippingDate == null && DeliveryDate == null) ____we can update like we was asked for____
            { 
                Order order = myOrder.GetValueOrDefault();
                order.ShippingDate = DateTime.Now;
                dal.Order.Update(order);
                return GetOrdertDetails(id); ////לטפל בחריגות מהפונק הזאת   
            }
            
        }
        catch (Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order", ex);
        }


    }

    BO.Order UpdateOrderDelivery(int id)
    {

    }

    BO.OrderTracking GetOrderTracking(int id)
    {

    }

    void UpdateOrder(int id)
    {

    }

}
