using DalApi;
using Do;

namespace Dal;

internal class DalOrder :IOrder
{

    public int Add(Order item)
    {
        return 6;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Order item)
    {
        throw new NotImplementedException();
    }
}
