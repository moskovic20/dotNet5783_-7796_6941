
namespace DalApi
{
    public interface IDal
    {
        IOrder order { get; }
        IProduct product { get; }
        IOrderItem item { get; }

    }
}
