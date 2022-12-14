
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
        //{
        //    try
        //    {
        //        int num = 1;
        //        while (num != 0)
        //        {
        //            string? temp;
        //            int id;
        //            int stock;
        //            bool b;
        //            double price;
        //            BO.Product pr = new BO.Product();


        //            Console.WriteLine(@"test product:
        //                    Enter your choice:
        //                    0- EXIT
        //                    1 - ADD PRODUCT
        //                    2 - DELETE PRODUCT
        //                    3 - UPDATE PRODUCT
        //                    4 - GET LIST OF ALL THE PRODUCTS
        //                    5 - GET PRODUCT INFORMATION");//choose which operation they want to do
        //            string? option = Console.ReadLine();
        //            bool op = int.TryParse(option, out num);
        //            if (!op)
        //            {
        //                Console.WriteLine("ERROR");
        //                break;
        //            }
        //            switch (num)
        //            {
        //                case 1:
        //                    Console.WriteLine("enter product ID:");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out id);
        //                    if (!b)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    };
        //                    pr.ID = id;
        //                    Console.WriteLine("enter product Name:");
        //                    temp = Console.ReadLine();
        //                    pr.NameOfBook = temp;
        //                    Console.WriteLine("enter product Price:");
        //                    temp = Console.ReadLine();
        //                    b = double.TryParse(temp, out price);
        //                    pr.Price = price;
        //                    printCategories();
        //                    temp = Console.ReadLine();
        //                    pr.Category = (DO.CATEGORY)int.Parse(temp!);
        //                    Console.WriteLine("enter product stock:");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out stock);
        //                    if (!b)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    }
        //                    pr.InStock = stock;
        //                    product.AddProduct_forM(pr);
        //                    break;
        //                case 2:
        //                    Console.WriteLine("enter the id of the product you want delete:");
        //                    int delID;
        //                    temp = Console.ReadLine();
        //                    delID = int.Parse(temp!);
        //                    product.DeleteProductByID_forM(delID);//M or C
        //                    break;
        //                case 3:
        //                    Console.WriteLine("enter product ID:");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out id);
        //                    if (!b)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    }
        //                    pr.ID = id;
        //                    Console.WriteLine(@"enter product Name:");
        //                    temp = Console.ReadLine();
        //                    pr.NameOfBook = temp;
        //                    Console.WriteLine(@"enter product Price:");
        //                    temp = Console.ReadLine();
        //                    b = double.TryParse(temp, out price);
        //                    pr.Price = price;
        //                    printCategories();
        //                    temp = Console.ReadLine();
        //                    pr.Category = (DO.CATEGORY)int.Parse(temp!);
        //                    Console.WriteLine(@"enter product stock:");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out stock);
        //                    if (!b)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    }
        //                    pr.InStock = stock;
        //                    product!.UpdateProductDetails_forM(pr);//M or C
        //                    break;
        //                case 4:
        //                    IEnumerable<BO.ProductForList> products;
        //                    products = product.GetAllProductForList_forC();//M or C
        //                    foreach (var item in products)
        //                        Console.WriteLine(item);
        //                    break;
        //                case 5:
        //                    Console.WriteLine("Enter 1 to manager, 2 for customer\n");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out int choose);
        //                    if (!b)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    }
        //                    Console.WriteLine(@"Enter the id of the wanted product");
        //                    temp = Console.ReadLine();
        //                    b = int.TryParse(temp, out id);
        //                    if (!b || choose != 1 || choose != 2)
        //                    {
        //                        Console.WriteLine(@"ERROR");
        //                        break;
        //                    }
        //                    if (choose == 1)
        //                        product.DeleteProductByID_forM(id);//ok??
        //                    //else
        //                    //   // product.getProductInfoCustomer(id);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        }

        private static void CartsSubMenu()
        {
            //myCart = myCart ?? new BO.Cart();
            //Console.WriteLine("Enter your name:");
            //myCart.CustomerName = Console.ReadLine();
            //Console.WriteLine("Enter your address");
            //myCart.ShippingAddress = Console.ReadLine();
            //Console.WriteLine("Enter your email");
            //myCart.CustomerEmail = Console.ReadLine();
            //myCart.TotalPrice = 0;
            //myCart.Items = new List<BO.OrderItem?>();
            //try
            //{
            //    string? temp;
            //    int id;
            //    bool b;
            //    int num = 1;
            //    while (num != 0)
            //    {
            //        BO.Product pr = new BO.Product();
            //        Console.WriteLine(@"test product:
            //                Enter your choice:
            //                0- EXIT
            //                1 - ADD PRODUCT TO CART
            //                2 - UPDATE PRODUCT AMOUNT
            //                3 - CONFIRM ORDER");//choose which operation they want to do
            //        string? option = Console.ReadLine();
            //        bool op = int.TryParse(option, out num);
            //        if (!op)
            //        {
            //            Console.WriteLine("ERROR");
            //            break;
            //        }
            //        switch (num)
            //        {
            //            case 1:
            //                Console.WriteLine(@"enter id of the product:");
            //                temp = Console.ReadLine();
            //                b = int.TryParse(temp, out id);
            //                if (!b)
            //                {
            //                    Console.WriteLine(@"ERROR");
            //                    break;
            //                }
            //                cart?.AddProductToCart(myCart, id);
            //                break;
            //            case 2:
            //                Console.WriteLine(@"enter id of the product:");
            //                temp = Console.ReadLine();
            //                b = int.TryParse(temp, out id);
            //                if (!b)
            //                {
            //                    Console.WriteLine(@"ERROR");
            //                    break;
            //                }

            //                Console.WriteLine(@"enter the new amount of the product:");
            //                temp = Console.ReadLine();
            //                b = int.TryParse(temp, out int amount);
            //                if (!b)
            //                {
            //                    Console.WriteLine(@"ERROR");
            //                    break;
            //                }
            //                cart.UpdateProductAmountInCart(myCart, id, amount);
            //                break;
            //            case 3:
            //                cart.MakeOrder(myCart);
            //                num = 0; //when we confim the order, we exit from the cart
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }

        /*private static void testOrder(BLApi.IOrder order)
        {
            try
            {
                string? temp;
                int id;
                bool b;
                int num = 1;
                while (num != 0)
                {
                    
                    BO.Product pr = new BO.Product();
                    Console.WriteLine(@"test product:
                            Enter your choice:
                            0- EXIT
                            1 - GET ORDER LIST
                            2 - UPDATE SENT ORDER
                            3 - UPDATE DELIVERY ORDER
                            4 - GET ORDER INFORMATION
                            5 - ORDER TRACKING");//choose which operation they want to do
                    string? option = Console.ReadLine();
                    bool op = int.TryParse(option, out num);
                    if (!op)
                    {
                        Console.WriteLine("ERROR");
                        break;
                    }
                    switch (num)
                    {
                        case 1:
                            List<BO.OrderForList?> lst=new List<OrderForList?>();
                            lst = order.getOrderList().ToList();
                            foreach (BO.OrderForList? item in lst)
                                Console.WriteLine(item);
                            break;
                        case 2:
                            Console.WriteLine(@"enter id of the order:");
                            temp = Console.ReadLine();
                            b=int.TryParse(temp, out id);
                            if (!b)
                            {
                                Console.WriteLine(@"ERROR");
                                break;
                            }
                            order.updateSentOrder(id);
                            break;
                        case 3:
                            Console.WriteLine(@"enter id of the order:");
                            temp = Console.ReadLine();
                            b = int.TryParse(temp, out id);
                            if (!b)
                            {
                                Console.WriteLine(@"ERROR");
                                break;
                            }
                            order.updateDeliveryOrder(id);
                            break;
                        case 4:
                            Console.WriteLine(@"enter id of the order:");
                            temp = Console.ReadLine();
                            b = int.TryParse(temp, out id);
                            if (!b)
                            {
                                Console.WriteLine(@"ERROR");
                                break;
                            }
                            BO.Order? order1 = new BO.Order();
                            order1= order.getOrderInfo(id);
                            Console.WriteLine(order1);
                            break;
                        case 5:
                            Console.WriteLine(@"enter id of the order:");
                            temp = Console.ReadLine();
                            b = int.TryParse(temp, out id);
                            if (!b)
                            {
                                Console.WriteLine(@"ERROR");
                                break;
                            }
                            BO.OrderTracking ortrack = new BO.OrderTracking();
                            ortrack=order.orderTracking(id);
                            Console.WriteLine(ortrack);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }*/
       
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

        #region מיין של תמר
        //int num = 1;///
        //while (num != 0)
        //{
        //    Console.WriteLine(@"Hello!
        //                Enter your choice:
        //                0-exit
        //                1-test Product
        //                2-test Order
        //                3-test Cart");
        //    string? option = Console.ReadLine();
        //    bool b = int.TryParse(option, out num);/// // בונוסס
        //    if (!b)// if the option they choose is incorect
        //    {
        //        Console.WriteLine("ERROR");// we print error
        //        break;
        //    }
        //    switch (num)// 3 option: 1 2 or 3
        //    {
        //        case 1:
        //            testProduct(bl.BoProduct);// they want an operation on the product
        //            break;
        //        case 2:
        //            testOrder(bl.BoOrder);// they want an operation on the order
        //            break;
        //        case 3:
        //            BO.Cart myCart = new BO.Cart();
        //            testCart(bl.Cart!, myCart);// they want an operation on the cart
        //            break;
        //        default:
        //            break;
        //    }

        //}
        #endregion
    }


}