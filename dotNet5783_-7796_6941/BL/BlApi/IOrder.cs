using BO;

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
    void DeleteOrder_forM(int orderID);
    IEnumerable<BO.OrderForList> getAllOrderOfClaient(string name);
    //public BO.Order GetOrdertDetails(string CustomerName);



}
