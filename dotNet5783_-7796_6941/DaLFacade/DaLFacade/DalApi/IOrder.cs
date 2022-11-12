using Do;


namespace DalApi;

public interface IOrder :ICrud<Order>
{
    new int Add(Order item);
    new Order GetById(int id);
    new void Update(Order item);
    new void Delete(int id);

    new IEnumerable<Order> GetAll();
}

