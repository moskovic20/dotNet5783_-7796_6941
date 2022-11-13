using Do;
using Dal;
using DelList;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Dal;

internal class Program
{
    private static DalProduct myP = new();
    private static DalOrder myO = new();
    private static DalOrderItem myOI = new();

    static void main(string[] arg)
    {

        Console.WriteLine(@"Select one of the following data entities:
    1: product
    2: order
    3: orderItem
                                    
    0: exit");

        int number;
        if (!int.TryParse(Console.ReadLine(), out number))
            Console.WriteLine("the pars is not success");

        while (number != 0)
        {
            switch (number)
            {
                case 1:
                    subMenueProduct();
                    break;

                case 2:
                    subMenueOrder();
                    break;

                case 3:
                    subMenueOrder();
                    break;
            }
        }


    }



    static void subMenueProduct()
    {
        #region print menue for product and user choose
        Console.WriteLine(@"Choose the action you want:
    a: add a new product
    b: get produdt by id
    c: get all the products
    d: update product
    e: delete product
    
    f: exit");

        char choose =(char)Console.Read();
        Console.ReadLine();
        #endregion

        Product p =new Product();//for use in the next loop

        while (choose != 'f')
        {
            switch (choose)
            {
                case 'a':
                    #region add new product.input details from the user
                    Console.WriteLine(@"Enter product details:id,name,category,price and amount");
                    p.ID = int.Parse(Console.ReadLine());
                    p.nameOfBook=Console.ReadLine();
                    p.Category = (Enums.CATEGORY)Console.Read();
                    p.Price=double.Parse(Console.ReadLine());
                    p.InStock=int.Parse(Console.ReadLine());
                    myP.Add(p);
                    break;
                #endregion

                case 'b':
                    #region print product by id
                    Console.WriteLine("enter the the ID of the product: ");
                    p = myP.GetById(int.Parse(Console.ReadLine()));
                    Console.WriteLine(p);
                    break;
                #endregion

                case 'c':
                    #region print all the product
                    IEnumerable< Product > allP=myP.GetAll();
                    foreach(Product product in allP)
                        Console.WriteLine("\n"+product+"\n");
                    break;
                #endregion

                case 'd':
                    break;

                case 'e':
                    #region delete product by id
                    Console.WriteLine("enter the the ID of the product you want delete: ");
                    myP.Delete(int.Parse(Console.ReadLine()));
                    break;
                    #endregion
            }
        }


    }

    static void subMenueOrder() 
    {
        #region print menue for order and user choose
        Console.WriteLine(@"Choose the action you want:
    a: add a new order
    b: get order by id
    c: get all the orders
    d: update order
    e: delete order
    
    f: exit");

        char choose = (char)Console.Read();
        #endregion

        Order ord = new Order();//for use in the next loop

        while (choose != 'f')
        {
            switch (choose)
            {
                case 'a':
                    #region add new order.input details from the user
                    Console.WriteLine(@"Enter order details:id,name of costumer,email,address,creat order date,shipping date and delivery date");
                    ord.ID = int.Parse(Console.ReadLine());
                    //
                    //
                    //
                    myO.Add(ord);
                    break;
                #endregion

                case 'b':
                    #region print order by id
                    Console.WriteLine("enter the the ID of the order: ");
                    ord= myO.GetById(int.Parse(Console.ReadLine()));
                    Console.WriteLine(ord);
                    break;
                #endregion

                case 'c':
                    #region print all the order
                    IEnumerable<Order> allO = myO.GetAll();
                    foreach (Order order in allO)
                        Console.WriteLine("\n" + order + "\n");
                    break;
                #endregion

                case 'd':
                    break;

                case 'e':
                    #region delete order by id
                    Console.WriteLine("enter the the ID of the order you want delete: ");
                    myO.Delete(int.Parse(Console.ReadLine()));
                    break;
                    #endregion
            }
        }


    }

    static void subMenueOrderItem() { }
}