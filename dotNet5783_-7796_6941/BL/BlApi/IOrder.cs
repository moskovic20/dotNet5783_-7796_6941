using BO;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetAllOrderForList();
    BO.Order GetOrdertByID(int id);
    BO.Order UpdateOrderShipping(int id);
    BO.Order UpdateOrderDelivery(int id);
    OrderTracking GetOrderTracking(int id);
    void UpdateOrder(int id);

}
