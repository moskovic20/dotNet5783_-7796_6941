using Do;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    new int Add(OrderItem item);
    new OrderItem GetById(int id);
    new void Update(OrderItem item);
    new void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    new IEnumerable<OrderItem> GetAll();
}