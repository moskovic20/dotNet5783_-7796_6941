using Do;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItems>
{
    int Add(OrderItems item);
    OrderItems GetById(int id);
    void Update(OrderItems item);
    void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItems> GetAll();
}