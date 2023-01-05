using Do;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    /// <summary>
    /// return list for all the orderitems in the order by the order OrderID
    /// </summary>
    /// <param name="OrderID"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetListByOrderID(int OrderID);

    /// <summary>
    /// find OrderItem by data of his OrderID and ProductID
    /// </summary>
    /// <param name="OrderID"></param>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID);

}