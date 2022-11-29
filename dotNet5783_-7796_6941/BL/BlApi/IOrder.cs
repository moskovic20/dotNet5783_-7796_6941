using BO;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetAllOrderForList();
}
