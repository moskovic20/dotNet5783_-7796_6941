﻿using Do;

namespace Dal;

internal static class DataSource
{ 
    static readonly Random R = new Random();

    static internal List<Order?> _Orders { get; } = new List<Order?> { };
    static internal List<OrderItem?> _OrderItems { get; } = new List<OrderItem?> { };
    static internal List<Product?> _Products { get; } = new List<Product?> { };

    internal static class Config
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }

        internal const int s_startOrderItem = 100000;
        private static int s_nextOrderItem = s_startOrderItem;
        internal static int NextOrderItem { get => ++s_nextOrderItem; }

    }

    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        CreateOrderItems();
    }

    private static void CreateProducts()
    {
        string[] NameOfBook = { "Harry Poter", "Anne of Green Gables", "Bible", "aya Pluto", "Raspberry juice" };
        
        for (int i = 0; i < 10; i++)
        {
            Product myP = new Product
            {
                ID=R.Next(100000,1000000000),
                Price = R.Next(20, 150),
                nameOfBook = NameOfBook[R.Next(0, 5)],//string
                Category = (Enums.CATEGORY)R.Next(0, 9),
                InStock = R.Next(25, 86)

            };

            #region Makes sure id is unique
            int pWithTheSameId = _Products.FindIndex(x => x.GetValueOrDefault().ID == myP.ID);

            while (pWithTheSameId != -1)//To make sure this ID is unique.
            {
                myP.ID = R.Next(100000, 1000000);
                pWithTheSameId = _Products.FindIndex(x => x.GetValueOrDefault().ID == myP.ID);
            }
            #endregion

            _Products.Add(myP);
            
        }
    }

    private static void CreateOrders()
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
                DateOrder = DateTime.Now - new TimeSpan(R.Next(11,41),R.Next(24),R.Next(60),R.Next(60)),
                NameCustomer = customerNames[R.Next(0, 21)],
                ShippingAddress = address[R.Next(0, 21)],
            };

            myOrder.Email = myOrder.NameCustomer + "@gmail.com";
            myOrder.ShippingDate = myOrder.DateOrder - new TimeSpan(R.Next(6,11),R.Next(24), R.Next(6), R.Next(60)); //להבין מה כתוב בתוך
            myOrder.DeliveryDate = myOrder.ShippingDate - new TimeSpan(R.Next(6), R.Next(24), R.Next(6), R.Next(60)); 

            _Orders.Add(myOrder);
        }
    }

    private static void CreateOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            Product? product = _Products[R.Next(_Products.Count)];

            _OrderItems.Add(
            new OrderItem
            {
                ID = Config.NextOrderItem,
                IdOfProduct = product?.ID ?? 0,
                IdOfOrder = R.Next(Config.s_startOrderNumber, Config.s_startOrderNumber + _Orders.Count),
                priceOfOneItem = product?.Price ?? 0,
                amountOfItem = R.Next(5)

            });
        }


    }
}