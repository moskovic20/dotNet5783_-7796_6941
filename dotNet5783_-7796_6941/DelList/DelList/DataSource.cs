using Do;
using System.Text;
using System;
using System.Diagnostics;
using static Do.Enums;

namespace DalApi;

internal static class DataSource
{
    static readonly Random R = new Random();

    static internal List <Order?> _Order { get; } = new List<Order?> { };
    static internal List <OrderItems?> _OrderItems { get; } = new List <OrderItems?> { };
    static internal List<Product?> _Product { get; } = new List<Product?> { };

    static string[] NameOfBook = { "Harry Poter", "Anne of Green Gables", "Bible", "aya Pluto", "Raspberry juice" };  
    internal static class Config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }

        internal const int s_startProductNumber = 0;
        private static int s_nextProductNumber = s_startOrderNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

        internal const int s_startOrderItem = 0;
        private static int s_nextOrderItem = s_startOrderItem;
        internal static int NextOrderItem { get => ++s_nextOrderItem; }

    }

    static private void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        AddOrderItems();
    }

    static private void CreateProducts()
    {
        string[] NameOfBook = { "Harry Poter", "Anne of Green Gables", "Bible", "aya Pluto", "Raspberry juice" };
        //string[] Caterories = { "mystery", "fantasy", "history", "scinence", "childen", "romans", "cookingAndBaking", "psychology", "Kodesh" };
        //Enums.CATEGORY tempCategory = new Enums.CATEGORY(R.Next(0,9);
        for (int i = 0; i < 10; i++)
        {
            _Product.Add(
                new Product
                {
                    ID = Config.NextProductNumber,
                    Price = R.Next(20, 150),
                    nameOfBook = NameOfBook[R.Next(0, 5)],//string
                    Category = Enums.CATEGORY.fantasy,
                    InStock = R.Next(25, 86)

                }) ; 

           
           
        }
    }

    static private void CreateOrders()
    {
     
    }

    static private void AddOrderItems()
    {
        Product? product = _Product[R.Next(_Product.Count)];

        _OrderItems.Add(
        new OrderItems
        {
            ID= Config.NextOrderItem,
            IdOfProduct = product?.ID,
            IdOfOrder = R.Next(Config.s_startOrderNumber,Config.s_startOrderNumber+_Order.Count),
            priceOfOneItem= product?.Price,
            amountOfItem= R.Next(6)

        });
        
        
    }
}
 