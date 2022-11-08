using Do;
using System.Text;
using System;
using System.Diagnostics;
using static Do.Enums;

namespace DalApi;

internal class DataSource
{
    static readonly Random R = new Random();

    internal List <Order?> _Order { get; } = new List<Order?> { };
    internal List <OrderItems?> _OrderItems { get; } = new List <OrderItems?> { };
    internal List<Product?> _Product { get; } = new List<Product?> { };

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

    public DataSource()
    {
        s_Initialize();
    }

    public void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        CreateOrderItems();
    }

    private void CreateProducts()
    {
        string[] NameOfBook = { "Harry Poter", "Anne of Green Gables", "Bible", "aya Pluto", "Raspberry juice" };
        
        for (int i = 0; i < 10; i++)
        {
            _Product.Add(
                new Product
                {
                    ID = Config.NextProductNumber,
                    Price = R.Next(20, 150),
                    nameOfBook = NameOfBook[R.Next(0, 5)],//string
                    Category = (Enums.CATEGORY)(R.Next(0, 9)),
                    InStock = R.Next(25, 86)

                }) ; 

           
           
        }
    }

    private void CreateOrders()
    {
        #region arrays: customerNames,customerEmails and address.
        string[] customerNames = {"Hila","Moriya","Shay","Shira","Adel","Dan","Orly","Neta","Otral","Gil",
            "Noam","Tal","David","Yehoda","Ariel","Harel","Reot","Adi","Yoav","Mikel"};

       string [] customerEmails = {"a@gmail.com", "ab@gmail.com", "abc@gmail.com", "abcd@gmail.com",
            "abcde@gmail.com","abcdef@gmail.com","abcdefg@gmail.com","aaa@gmail.com","sss@gmail.com","uuu@gmail.com",
            "hhh@gmail.com","kkk@gmail.com","lll@gmail.com","ppp@gmail.com","rrr@gmail.com","www@gmail.com","893@gmail.com",
            "pp@gmail.com","tt7@gmail.com","p99@gmail.com"};

        string[] address = {"a","v","b","bb","hj","nn","vo","dr","xc","ss","tt","we","ww","xx","pp","ss",
            "ty","io","sdf","ioj"};

        #endregion

        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new Order
            {
                ID = Config.NextOrderNumber,
                DateOrder = DateTime.Now - new TimeSpan(R.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                NameCustomer=customerNames[R.Next(0,21)],
                Email=customerEmails[R.Next(0,21)],
                ShippingAddress=address[R.Next(0,21)],
            };

            myOrder.ShippingDate = myOrder.DateOrder - new TimeSpan(R.NextInt64(10L * 1000L * 3600L * 24L * 100L)); //להבין מה כתוב בתוך
            myOrder.DeliveryDate = myOrder.ShippingDate - new TimeSpan(R.NextInt64()); //לבדוק מה לרשום בתוך
            
            _Order.Add(myOrder);
        }
    }

    private void CreateOrderItems()
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
 