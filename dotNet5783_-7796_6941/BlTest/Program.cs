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
        static BlApi.IBl bl = BlApi.Factory.GetBl() ?? throw new NullReferenceException("Missing BL");
        static int integer;
        static double dbl;
        //static DateTime date;
        static string s;
        static BO.Cart demoCart = new() { CustomerName = "demo name", CustomerEmail = "demo@email.com", CustomerAddress = "demo address", Items = new List<OrderItem>()! };


        private static void ProductsSubMenu()
        {
            char option;
            Console.WriteLine("Please choose one of the following options:\n" +
                "0. return to main menu\n" +
                "a. add a product\n" +
                "b. get a product (manager screen)\n" +
                "c. get a product (client screen)\n" +
                "d. get all products (manager screen)\n" +
                "e. get all products (client screen)\n"+
                "f. update a product\n" +
                "g. delete a product");
            bool success = char.TryParse(Console.ReadLine(), out option);
            switch (option)//TODO eliminate needless repetition with functions
            {
                case '0':
                    return;

                #region הוספת מוצר
                case 'a':
                    BO.Product newProduct = new();
                    Console.WriteLine("please enter the following details:");

                    Console.Write("Product ID: ");
                    if (int.TryParse(Console.ReadLine(), out integer) && integer >= 100000) newProduct.ID = integer;
                    else throw new InvalidDataException();

                    Console.Write("Product Name: ");
                    newProduct.NameOfBook = Console.ReadLine();

                    Console.Write("Product Name of author: ");
                    newProduct.AuthorName = Console.ReadLine();

                    Console.Write("Category Number: ");
                    if (int.TryParse(Console.ReadLine(), out integer) && Enum.IsDefined(typeof(DO.CATEGORY), integer)) newProduct.Category = (DO.CATEGORY)integer;
                    else throw new InvalidDataException();

                    Console.Write("Price: ");
                    if (double.TryParse(Console.ReadLine(), out dbl)) newProduct.Price = dbl;
                    else throw new InvalidDataException();

                    Console.Write("Amount in stock: ");
                    if (int.TryParse(Console.ReadLine(), out integer)) newProduct.InStock = integer;
                    else throw new InvalidDataException();

                    Console.WriteLine(bl.BoProduct.AddProduct_forM(newProduct));
                    break;
                #endregion

                #region הבאת פרטי מוצר למנהל
                case 'b':
                    Console.Write("Please insert an ID: ");
                    if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();

                    Console.WriteLine(bl.BoProduct.GetProductDetails_forM(integer));
                    break;
                #endregion

                #region הבאת פרטי מוצר עבור לקוח
                case 'c':
                    Console.Write("Please insert an ID: ");
                    if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();
                    Console.WriteLine(bl.BoProduct.GetProductDetails_forC(integer, demoCart));
                    break;
                #endregion

                #region הדפסת כל המוצרים עבור מנהל-כולל מוצרים עדכון מחיר
                case 'd':
                    foreach (var o in bl.BoProduct.GetAllProductForList_forM())
                    {
                        Console.WriteLine(o);
                    }
                    break;
                #endregion

                #region הדפסת כל המוצרים עבור לקוח
                case 'e':
                    foreach (var o in bl.BoProduct.GetAllProductForList_forC())
                    {
                        Console.WriteLine(o);
                    }
                    break;
                #endregion

                #region עדכון פרטי מוצר
                case 'f':
                    Console.Write("Please insert an ID: ");
                    if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();

                    Product product = bl.BoProduct.GetProductDetails_forM(integer);
                    Console.WriteLine(product);

                    Console.WriteLine("please enter the following details:\n" +
                        "insert values only in details you want to change");

                    Console.Write("Name of book: ");
                    s = Console.ReadLine()!;
                    if (s != "") product.NameOfBook = s;

                    Console.Write("Category: ");
                    s = Console.ReadLine()!;
                    if (s != "")
                    {
                        if (int.TryParse(s, out integer) && Enum.IsDefined(typeof(BO.CATEGORY), integer)) product.Category = (DO.CATEGORY)integer;
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
                    Console.Write("Please insert an ID: ");
                    if (!(int.TryParse(Console.ReadLine(), out integer) && integer >= 100000)) throw new InvalidDataException();

                    //Console.WriteLine(bl.BoProduct.DeleteProductByID_forM(integer));
                    bl.BoProduct.DeleteProductByID_forM(integer);
                    break;
                #endregion

                default:
                    if (!(success && option == 0)) Console.WriteLine("Bad command! Go stand in the corner!");
                    break;
            }
        }

        private static void CartsSubMenu()
        {
        }
        private static void OrdersSubMenu()
        {
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
                            if (!success) Console.WriteLine("Bad command! Go stand in the corner!");
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
 8: Kodesh");

        }


    }


}