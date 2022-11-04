using Do;

namespace DalApi;

public interface IOrder :ICrud<Order>
{
    int Add(Order item);
    Order GetById(int id);
    void Update(Order item);
    void Delete(int id);

    IEnumerable<Order> GetAll();
}

