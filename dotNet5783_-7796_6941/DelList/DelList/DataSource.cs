using Do;

namespace DalApi;

internal static class DataSource
{
    static readonly Random R = new Random();

    static internal List <Order?> _Order { get; } = new List<Order?> { };
    static internal List <OrderItems?> _OrderItems { get; } = new List <OrderItems?> { };
    static internal List<Product?> _Product { get; } = new List<Product?> { };

    internal static class Config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrserNumber = s_startOrderNumber;
        internal static int NextOrserNumber { get => ++s_nextOrserNumber; }

        internal const int s_startProductNumber = 0;
        private static int s_nextProductNumber = s_startOrderNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }
    }

    static private void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        AddOrderItems();
    }

    static private void CreateProducts()
    {
        for(int i = 0; i < 10; i++)
        {
            _Product.Add(
                new Product
                {
                    ID = Config.NextProductNumber,
                    Price = R.Next(20,150),



                }); 
        }
    }

    static private void CreateOrders()
    {
     
    }

    static private void AddOrderItems()
    {
        
    }
}
 