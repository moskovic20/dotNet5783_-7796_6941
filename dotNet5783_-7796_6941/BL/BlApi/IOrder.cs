using BO;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetAllOrderForList();
    OrderForList GetOrderForList(int orderID);
    BO.Order GetOrdertDetails(int id);
    BO.Order UpdateOrderShipping(int id, DateTime? dt = null);
    BO.Order UpdateOrderDelivery(int id, DateTime? dt = null);
    BO.OrderTracking GetOrderTracking(int id);
    void CancleOrder_forM(int id);
    void DeleteOrder_forM(int orderID);
    IEnumerable<BO.OrderForList> GetAllOrderOfClaient(string name);
    public IEnumerable<OrderForList> GetAllOrdersByNumber(int number);
    public IEnumerable<BO.Order> GetAllDeletedOrders();
    public IEnumerable<BO.OrderItem> GetAllDeletedOrderItems();



}
