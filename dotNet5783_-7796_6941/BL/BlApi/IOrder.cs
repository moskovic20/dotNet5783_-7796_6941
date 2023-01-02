﻿using BO;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetAllOrderForList();
    OrderForList GetOrderForList(int orderID);
    BO.Order GetOrdertDetails(int id);
    BO.Order UpdateOrderShipping(int id);
    BO.Order UpdateOrderDelivery(int id);
    BO.OrderTracking GetOrderTracking(int id);
    void UpdateOrder(int id, int option);
    void CancleOrder_forM(int id);
    public void DeleteOrder_forM(int orderID);

   

}
