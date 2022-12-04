using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class BoOrder //: IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    #region   חישוב סטטוס להזמנה וזריקת חריגות
    private static BO.OrderStatus calculateStatus(DateTime? DateO, DateTime? ShippingD, DateTime? DeliveryD ) 
    {

        #region חריגות אפשריות בזמנים
        if (DateO == null)
            throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        if (ShippingD == null && DeliveryD == null)
            throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        if (ShippingD == null && DeliveryD != null || ShippingD != null && DateO > ShippingD
                   || DeliveryD != null && DateO > DeliveryD || ShippingD != null && DeliveryD != null && ShippingD > DeliveryD)
            throw new ArgumentException("rong information,cant be possible");          /////////exceptions
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
                                AmountOfItems = O.CalculateAmountItems(),
                                TotalPrice = Dal.Tools.CalculatePriceOfAllItems(O)
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

    //BO.Order UpdateOrderShipping(int id)
    //{

    //}

    //BO.Order UpdateOrderDelivery(int id)
    //{

    //}

    //BO.OrderTracking GetOrderTracking(int id)
    //{

    //}

    void UpdateOrder(int id)
    {

    }

}
