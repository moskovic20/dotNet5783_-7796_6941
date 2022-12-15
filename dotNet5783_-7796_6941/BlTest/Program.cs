
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using BlImplementation;
using System.Security.Cryptography;



namespace BLTest
{
    public class Program
    {
       static BlApi.IBl bl = BlApi.Factory.GetBl() ?? throw new NullReferenceException("Missing Bl");

        static int integer;
        static double dbl;
        static DateTime date;
        static string s;
        static Cart demoCart = new Cart() { CustomerName = "demo name", CustomerEmail = "demo@email.com", CustomerAddress = "demo address", Items = new List<OrderItem>()! };

        private static void ProductsSubMenu()
        { 
        
        }

        private static void CartsSubMenu()
        {
           
        }
        static void OrdersSubMenu()
        {

            try
            {
                char option;
                Console.WriteLine("Please choose one of the following options:\n" +
                    "0. return to main menu\n" +
                    "a. get all orders\n" +
                    "b. get an order\n" +
                    "c. report shipping\n" +
                    "d. report delivery\n" +
                    "e. update an order\n" +
                    "f. track an order");
                bool success = char.TryParse(Console.ReadLine(), out option);
                switch (option)//TODO eliminate needless repetition with functions
                {
                    case '0':

                        return;

                    case 'a': //printing all the orders

                        IEnumerable<BO.OrderForList> orders;
                        orders = bl.BoOrder.GetAllOrderForList(); //להבין למה זה הגישה הנכונה!
                        foreach (var item in orders)
                            Console.WriteLine(item);
                        break;
                    
                    case 'b': //printing the requested order

                        Console.Write("Please insert an ID: ");
                        if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                        Console.WriteLine(bl.BoOrder.GetOrdertDetails(integer));
                        break;

                    case 'c': //send update

                        Console.Write("Please insert an ID: ");
                        if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                        Console.WriteLine(bl.BoOrder.UpdateOrderShipping(integer));
                        break;

                    case 'd': //delivart update

                        Console.Write("Please insert an ID: ");
                        if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                        Console.WriteLine(bl.BoOrder.UpdateOrderDelivery(integer));
                        break;

                    case 'e': //update order
                        //int orderID, productID, newAmount;
                        //Console.Write("Please insert an order ID: ");
                        //if (!(int.TryParse(Console.ReadLine(), out orderID) && orderID > 0)) throw new InvalidDataException();
                        //Order order = bl.BoOrder.GetOrdertDetails(orderID);
                        //Console.WriteLine(order);
                        //Console.Write("Please insert a product ID: ");
                        //if (!(int.TryParse(Console.ReadLine(), out productID) && productID >= 100000)) throw new InvalidDataException();
                        //Console.Write("Please insert a new amount: ");
                        //if (!(int.TryParse(Console.ReadLine(), out newAmount) && integer >= 0)) throw new InvalidDataException();                       
                        //bl.BoOrder.UpdateOrder(orderID/*, productID, newAmount*/);
                        Console.WriteLine("not redy for that option yet....:):)");
                        break;

                    case 'f':

                        Console.Write("Please insert an ID: ");
                        if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                        Console.WriteLine(bl.BoOrder.GetOrderTracking(integer));
                        break;

                    default:

                        if (!(success && option == 0)) Console.WriteLine("Bad command! Go stand in the corner!");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Main(string[] args)
        {

            int option;
            bool success;
            try
            {
                do
                {
                    Console.WriteLine("Welcome to the test menu!\n" +//TODO menues should use enums
                        "Please choose one of the following options:\n" +
                        "0. Exit\n" +
                        "1. Check Products\n" +
                        "2. Check Carts\n" +
                        "3. Check Orders");
                    success = int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1:
                            ProductsSubMenu();
                            break;
                        case 2:
                            CartsSubMenu();
                            break;
                        case 3:
                            OrdersSubMenu();
                            break;
                        default:
                            if (!(success && option == 0)) Console.WriteLine("Bad command! Try again or Exit");//לא הצליח להמיר או שהמשתמש הכניס קלט לא טוב
                            break;
                    }
                } while (!(success && option == 0));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        static void printCategories()
        {
            Console.WriteLine(@"Choose a category by the number:
                         1: mystery
                         2: fantasy
                         3: history
                         4: scinencechilden
                         5: romans
                         6: cookingAndBaking
                         7: psychology
                         8: Kodesh"
            );

        }
                
    }

}