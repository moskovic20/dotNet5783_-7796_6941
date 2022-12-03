using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using DalApi;
using BO;
//using Do;
//using Dal;
using Order = BO.Order;


namespace BlImplementation;

internal class BoOrder: IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");


    private static OrderStatus calculateStatus(DateTime? DateO, DateTime? ShippingD, DateTime? DeliveryD)
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
            return OrderStatus.Pending;
        if (DeliveryD == null)
            return OrderStatus.Processing;
        else
            return OrderStatus.Completed;
    }





    /// <summary>
    /// הכנסת כל ההזמנות הלא ריקות לרשימה
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.OrderForList> GetAllOrderForList()
    {
        try
        {
            // dal.DalOrder.GetAll()
            var orderList = from O in dal.Order.GetAllExistsBy()
                            select new BO.OrderForList()
                            {
                                OrderID = O.GetValueOrDefault().ID,
                                CuustomerName = O.GetValueOrDefault().NameCustomer,
                                Status = calculateStatus(O.GetValueOrDefault().DateOrder, O.GetValueOrDefault().ShippingDate, O.GetValueOrDefault().DeliveryDate),  
                                AmountOfItems = O.CalculateAmountItems(),
                                TotalPrice = O.CalculatePriceOfAllItems()
                            };
            return orderList;
        }
        catch (GetDetailsProblemException ex)
        {
            ;
        }
    }
    BO.Order GetOrdertByID(int id)
    {
        if (id > 0)
        {
            Order order;
            

        }
        else
            throw new Do.DoesntExistException("can't be founf");

    }
    BO.Order UpdateOrderShipping(int id)
    {

    }
    BO.Order UpdateOrderDelivery(int id)
    {

    }
    OrderTracking GetOrderTracking(int id)
    {

    }
    void UpdateOrder(int id)
    {

    }

}
