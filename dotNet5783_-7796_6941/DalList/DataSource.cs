using Do;
using DalApi;
using DO;

namespace Dal;

public class DataSource
{
    static readonly Random R = new Random();
    
    private static DataSource? instance ;

    private static readonly object key = new(); //Thread Safe

    public static DataSource? Instance
    {
        get
        {
            if (instance == null) //Lazy Initialization
            {
                lock (key)
                {
                    if (instance == null)
                        instance = new DataSource();
                }
            }

            return instance;
        }
    }

    static DataSource() { }

    //private DataSource() { }
    internal List<Order?> _Orders { get; } = new List<Order?> { };
    internal List<OrderItem?> _OrderItems { get; } = new List<OrderItem?> { };
    internal List<Product?> _Products { get; } = new List<Product?> { };

    internal static class Config
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }

        internal const int s_startOrderItem = 100000;
        private static int s_nextOrderItem = s_startOrderItem;
        internal static int NextOrderItem { get => ++s_nextOrderItem; }

    }

    private DataSource()
    {
        s_Initialize();
    }

    private void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        CreateOrderItems();
    }

    private void CreateProducts()
    {
        string[] NameOfBook = { "Harry Poter", "Anne of Green Gables", "Bible", "aya Pluto",
            "Raspberry juice", "Tell no one","the candidate","Alone in the battle","the giver","Broken Heart" };
        string[] NamesOfWriters = { "jeik.r", "mor.s", "noaa.f", "gaie.g", "noi.a", "doni.j", "rom.k" };

        for (int i = 0; i < 10; i++)
        {
            Product myP = new Product
            {
                ID = R.Next(100000, 999999999),
                Price = R.Next(40, 150),
                NameOfBook = NameOfBook[i],
                AuthorName = NamesOfWriters[R.Next(0, 7)],
                Category = (CATEGORY) R.Next(0, 9),
                InStock = (i != 0) ? R.Next(20, 100) : 0,

            };

            #region Makes sure id is unique
            int pWithTheSameId = _Products.FindIndex(x => x.GetValueOrDefault().ID == myP.ID);

            while (pWithTheSameId !=-1)//To make sure this ID is unique.
            {
                myP.ID = R.Next(100000, 999999999);
                pWithTheSameId = _Products.FindIndex(x => x.GetValueOrDefault().ID == myP.ID);
            }
            #endregion

            _Products.Add(myP);

        }
    }

    private void CreateOrders()
    {
        #region arrays: customerNames,customerEmails and address.
        string[] customerNames = {"Hila","Moriya","Shay","Shira","Adel","Dan","Orly","Neta","Otral","Gil",
            "Noam","Tal","David","Yehoda","Ariel","Harel","Reot","Adi","Yoav","Mikel"};

        string[] address = {"a","v","b","bb","hj","nn","vo","dr","xc","ss","tt","we","ww","xx","pp","ss",
            "ty","io","sdf","ioj"};

        #endregion

        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new Order
            {
                ID = Config.NextOrderNumber,
                DateOrder = DateTime.Now - new TimeSpan(R.Next(11, 41), R.Next(24), R.Next(60), R.Next(60)),
                CustomerName = customerNames[R.Next(0, 20)],
                ShippingAddress = address[R.Next(0, 20)],
            };

            myOrder.CustomerEmail = myOrder.CustomerName + "@gmail.com";

            myOrder.ShippingDate = (i < 16) ? DateTime.Now - new TimeSpan(R.Next(6, 11), R.Next(24), R.Next(60), R.Next(60)) : null;
            myOrder.DeliveryDate = (i < 10) ? DateTime.Now - new TimeSpan(R.Next(6), R.Next(24), R.Next(60), R.Next(60)) : null;

            _Orders.Add(myOrder);
        }
    }

    private void CreateOrderItems()
    {

        for (int i = 0; i < 20; i++)
        {
            int _orderId = Config.s_startOrderNumber+i;
            int numOfItems = R.Next(1, 5);
            for (int j = 0; j < numOfItems; j++)
            {
                Product? product = _Products[R.Next(_Products.Count)]; //choose random product to put into the orderitems list

                OrderItem _orderItem = new OrderItem
                {
                    ID = Config.NextOrderItem,
                    OrderID = _orderId,
                    ProductID = product?.ID ?? 0,
                    PriceOfOneItem = product?.Price ?? 0,
                    AmountOfItems = R.Next(1, 5),
                    //Image = "",
                    IsDeleted = false
                };

                _OrderItems.Add(_orderItem);
            }
        }
    }
}