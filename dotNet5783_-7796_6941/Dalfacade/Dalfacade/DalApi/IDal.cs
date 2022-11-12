
namespace DalApi
{
    internal interface IDal
    {
        IOrder order { get; }
        IProduct product { get; }
        IOrderItem item { get; }

    }
}
