using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using BO;
using Do;

namespace BlImplementation;

internal class BoOrder: IOrder
{
    private IDal Dal = new Dal.DalList();
    IEnumerable<OrderForList> GetAllOrderForList()
    {
       // dal.DalOrder.GetAll()
       var orderList = from O in Dal.Order.GetAllExistsBy()
                       select new OrderForList()
                       {
                           OrderID = O.GetValueOrDefault().ID,
                           CuustomerName= O.GetValueOrDefault().NameCustomer,
                           Status= O.GetValueOrDefault().

                       };
    }
    BO.Order GetOrdertByID(int id)
    {

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
