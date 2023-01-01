using BO;



namespace BLTest
{
    public class Program
    {
        static BlApi.IBl bl = BlApi.Factory.GetBl() ?? throw new NullReferenceException("Missing BL");
        static int integer;
        static double dbl;
        //static DateTime date;
        static string s = "";
        static char option;
        static BO.Cart demoCart = new() { CustomerName = "demo name", CustomerEmail = "demo@email.com", CustomerAddress = "demo address", Items = new()! };


        private static void ProductsSubMenu()
        {
            do
            {
                Console.WriteLine("\nPlease choose one of the following options:\n" +
                "0. return to main menu\n" +
                "a. add a product\n" +
                "b. get a product (manager screen)\n" +
                "c. get a product (client screen)\n" +
                "d. get all products (manager screen)\n" +
                "e. get all products (client screen)\n" +
                "f. update a product\n" +
                "g. delete a product");

                bool success = char.TryParse(Console.ReadLine(), out option);
                try
                {
                    switch (option)
                    {
                        case '0':
                            return;

                        #region הוספת מוצר
                        case 'a':
                            BO.Product newProduct = new();
                            Console.WriteLine("please enter the following details:");

                            Console.Write("Product orderID: ");
                            s = Console.ReadLine()!;
                            if (s == "") throw new BO.InvalidValue_Exception("You cannot add a product without orderID");
                            if (int.TryParse(s, out integer) && (integer >= 100000 || integer <= -100000)) newProduct.ID = integer;
                            else throw new BO.InvalidValue_Exception("orderID sould be with 6 digits at list");

                            Console.Write("Product Name: ");
                            s = Console.ReadLine()!;
                            if (s != "") newProduct.NameOfBook = s;
                            else throw new BO.InvalidValue_Exception("You cannot add a product without the product name");


                            Console.Write("Product Name of author: ");
                            s = Console.ReadLine()!;
                            if (s != "") newProduct.AuthorName = s;
                            else throw new BO.InvalidValue_Exception("You cannot add a product without the author name");

                            printCategories();
                            Console.Write("Category Number: ");
                            s = Console.ReadLine()!;
                            if (s == "") throw new BO.InvalidValue_Exception("You cannot add a product without category");
                            if (int.TryParse(s, out integer) && Enum.IsDefined(typeof(DO.CATEGORY), integer)) newProduct.Category = (BO.CATEGORY)integer;
                            else throw new InvalidDataException();

                            Console.Write("Price: ");
                            s = Console.ReadLine()!;
                            if (s == "") newProduct.Price = null;
                            else if (double.TryParse(s, out dbl)) newProduct.Price = dbl;
                            else throw new InvalidDataException();

                            Console.Write("Amount in stock: ");
                            s = Console.ReadLine()!;
                            if (s == "") newProduct.InStock = null;
                            else if (int.TryParse(s, out integer)) newProduct.InStock = integer;
                            else throw new InvalidDataException();


                            Console.Write("ProductImagePath for picture: ");
                            s = Console.ReadLine()!;
                            newProduct.ProductImagePath = (s != "") ? s : null;

                            Console.WriteLine(bl.BoProduct.AddProduct_forM(newProduct));
                            break;
                        #endregion

                        #region הבאת פרטי מוצר למנהל
                        case 'b':
                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000 || integer <= -100000)) throw new InvalidDataException("orderID can't be ");

                            Console.WriteLine(bl.BoProduct.GetProductDetails_forM(integer));
                            break;
                        #endregion

                        #region הבאת פרטי מוצר עבור לקוח
                        case 'c':
                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000 || integer <= -100000)) throw new InvalidDataException();
                            Console.WriteLine(bl.BoProduct.GetProductDetails_forC(integer, demoCart));
                            break;
                        #endregion

                        #region הדפסת כל המוצרים עבור מנהל-כולל מוצרים עדכון מחיר
                        case 'd':
                            //foreach (var o in bl.BoProduct.GetAllProductForList_forM())
                            //{
                            //    Console.WriteLine(o);
                            //}
                            Console.WriteLine(string.Join("\n", bl.BoProduct.GetAllProductForList_forM()));
                            break;
                        #endregion

                        #region הדפסת כל המוצרים עבור לקוח
                        case 'e':
                            //foreach (var o in bl.BoProduct.GetAllProductForList_forC())
                            //{
                            //    Console.WriteLine(o);
                            //}
                            Console.WriteLine(string.Join("\n", bl.BoProduct.GetAllProductForList_forC()));
                            break;
                        #endregion

                        #region עדכון פרטי מוצר
                        case 'f':
                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000 || integer <= -100000)) throw new InvalidDataException();

                            Product product = bl.BoProduct.GetProductDetails_forM(integer);
                            Console.WriteLine(product);

                            Console.WriteLine("please enter the following details:\n" +
                                "insert values only in details you want to change");

                            Console.Write("Name of book: ");
                            s = Console.ReadLine()!;
                            if (s != "") product.NameOfBook = s;

                            Console.Write("Name of author: ");
                            s = Console.ReadLine()!;
                            if (s != "") product.AuthorName = s;

                            Console.Write("Category: ");
                            s = Console.ReadLine()!;
                            if (s != "")
                            {
                                if (int.TryParse(s, out integer) && Enum.IsDefined(typeof(BO.CATEGORY), integer)) product.Category = (BO.CATEGORY)integer;
                                else throw new InvalidDataException();
                            }

                            Console.Write("Price: ");
                            s = Console.ReadLine()!;
                            if (s != "")
                            {
                                if (double.TryParse(s, out dbl)) product.Price = dbl;
                                else throw new InvalidDataException();
                            }

                            Console.Write("Amount in stock: ");
                            s = Console.ReadLine()!;
                            if (s != "")
                            {
                                if (int.TryParse(s, out integer)) product.InStock = integer;
                                else throw new InvalidDataException();
                            }

                            //Console.WriteLine(bl.BoProduct.UpdateProductDetails_forM(product));
                            bl.BoProduct.UpdateProductDetails_forM(product);

                            break;
                        #endregion

                        #region מחיקת מוצר עבור מנהל
                        case 'g':
                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();

                            //Console.WriteLine(bl.BoProduct.DeleteProductByID_forM(integer));
                            bl.BoProduct.DeleteProductByID_forM(integer);
                            break;
                        #endregion

                        default:
                            if (!(success && option == 0)) Console.WriteLine("Bad command! Try again or Exsit ");//אם לא הצליח להמיר או שהפקודה לא תואמת לסוויצ קייסים
                            break;
                    }
                }
                catch (Exception ex) { Console.WriteLine("\n" + ex + "\n"); }

            } while (option != '0');

        }

        private static void CartsSubMenu()
        {
            do
            {
                Console.WriteLine("Please choose one of the following options:\n" +
                    "0. return to main menu\n" +
                    "a. add a product\n" +
                    "b. update amount\n" +
                    "c. checkout\n");

                bool success = char.TryParse(Console.ReadLine(), out option);
                try
                {
                    switch (option)//TODO eliminate needless repetition with functions
                    {
                        case '0':
                            return;

                        #region הוספת מוצר לסל הקניות
                        case 'a':
                            Console.Write("Please insert a product orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();
                            Console.WriteLine(bl.Cart.AddProductToCart(demoCart, integer));
                            break;
                        #endregion

                        #region עדכון כמות של מוצר בסל הקניות
                        case 'b':
                            int productID, amount;
                            Console.Write("Please insert a product orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out productID) && integer >= 100000)) throw new InvalidDataException();
                            Console.Write("Please insert a new amount: ");
                            if (!(int.TryParse(Console.ReadLine(), out amount) && integer >= 0)) throw new InvalidDataException();
                            Console.WriteLine(bl.Cart.UpdateProductAmountInCart(demoCart, productID, amount));
                            break;
                        #endregion

                        #region ביצוע ההזמנה של סל הקניות
                        case 'c':
                            Console.WriteLine("Please enter customer's name, Email and address (separeated with the Enter key)");
                            Console.WriteLine("Your order number is: " + bl.Cart.MakeOrder(demoCart));
                            break;
                        #endregion

                        default:
                            if (!(success && option == 0)) Console.WriteLine("Bad command! Go stand in the corner!");
                            break;
                    }

                }
                catch (Exception ex) { Console.WriteLine("\n" + ex + "\n"); }

            } while (option != '0');


        }

        static void OrdersSubMenu()
        {
            do
            {
                Console.WriteLine("Please choose one of the following options:\n" +
                    "0. return to main menu\n" +
                    "a. get all orders\n" +
                    "b. get an order\n" +
                    "c. report shipping\n" +
                    "d. report delivery\n" +
                    "e. update an order\n" +
                    "f. track an order");
                bool success = char.TryParse(Console.ReadLine(), out option);
                try
                {
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

                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException("orderID cant be negatine- check this error");// חריגה על מספר שלילי , צריך לבדא האם זה תקין לבדוק פה
                            Console.WriteLine(bl.BoOrder.GetOrdertDetails(integer));
                            break;

                        case 'c': //send update

                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                            Console.WriteLine(bl.BoOrder.UpdateOrderShipping(integer));
                            break;

                        case 'd': //delivart update

                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                            Console.WriteLine(bl.BoOrder.UpdateOrderDelivery(integer));
                            break;

                        case 'e': //update order
                            //int orderID/*, productID, newAmount*/;


                            //Console.Write("Please insert an order orderID: ");
                            //if (!(int.TryParse(Console.ReadLine(), out orderID) && orderID > 0)) throw new InvalidDataException();
                            //Order order = bl.BoOrder.GetOrdertDetails(orderID);
                            //Console.WriteLine(order);
                            //Console.WriteLine(@"Please choose one of the following options:\n" +
                            //                 "0. return to menu\n" +
                            //                 "a. Changing the customer's email address\n" +
                            //                 "b. Change order destination address\n" +
                            //                 "c. Changing the quantity of products in the order\n");
                            //char op;
                            //bool orOption = char.TryParse(Console.ReadLine(), out op);
                            //switch(op){
                            //    case 0:
                            //        break;
                            //}


                            //Console.Write("Please insert a product orderID: ");
                            //if (!(int.TryParse(Console.ReadLine(), out productID) && productID >= 100000)) throw new InvalidDataException();
                            //Console.Write("Please insert a new amount: ");
                            //if (!(int.TryParse(Console.ReadLine(), out newAmount) && integer >= 0)) throw new InvalidDataException();                       
                            //bl.BoOrder.UpdateOrder(orderID/*, productID, newAmount*/);
                            Console.WriteLine("not redy for that option yet....:):)");
                            break;

                        case 'f':

                            Console.Write("Please insert an orderID: ");
                            if (!(int.TryParse(Console.ReadLine(), out integer) && integer > 0)) throw new InvalidDataException();
                            Console.WriteLine(bl.BoOrder.GetOrderTracking(integer));
                            break;

                        default:

                            if (!(success && option == '0')) Console.WriteLine("Bad command! Try again or Exsit ");//אם לא הצליח להמיר או שהפקודה לא תואמת לסוויצ קייסים
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (option != '0');

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
                            if (option != 0 || !success) Console.WriteLine("\nPlease enter a number between 0-3\n");
                            break;
                    }
                } while (option != 0 || !success);
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