using Do;
using DalApi;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using System;
using DO;

namespace Dal;

internal class Program
{
    public static DalProduct myP = new();
    public static DalOrder myO = new();
    public static DalOrderItem myOI = new();

    static void Main(string[] arg)
    {
        int choice = 4;
        do
        {
            Console.WriteLine(@"Select one of the following data entities:
    0: exit
    1: product
    2: order
    3: orderItem");

            try
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                    throw new Exception("The conversion failed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    subMenueProduct();
                    break;

                case 2:
                    subMenueOrder();
                    break;

                case 3:
                    subMenueOrderItem();
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;
            }

        }
        while (choice != 0);

    }


    static void subMenueProduct()
    {
        #region Variables we will use in the next loop
        char choose;
        Product p = new Product();
        int id, inStock, category;
        double price;
        #endregion

        do
        {
            #region print menue for product and user choose
            Console.WriteLine(@"Choose the action you want:
    a: add a new product
    b: get product by id
    c: get all the products
    d: update product
    e: delete product
    
    f: exit");

            choose = (char)Console.Read();
            Console.ReadLine();
            #endregion
            try
            {
                switch (choose)
                {
                    case 'a':
                        #region add new product.input details from the user

                        Console.WriteLine(@"Enter book's details: id, titel ,author price and amount");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        p.ID = id;

                        p.nameOfBook = Console.ReadLine();
                        p.authorName = Console.ReadLine();

                        if (!double.TryParse(Console.ReadLine(), out price))
                            throw new Exception("The conversion failed");

                        p.Price = price;

                        if (!int.TryParse(Console.ReadLine(), out inStock))
                            throw new Exception("The conversion failed");

                        p.InStock = inStock;

                        printCategories();

                        if (!int.TryParse(Console.ReadLine(), out category))
                            throw new Exception("The conversion failed");

                        p.Category = (CATEGORY)category;

                        id = myP.Add(p);
                        Console.WriteLine("\n the id of this product is: {0}\n", id);

                        break;
                    #endregion

                    case 'b':
                        #region print product by id

                        Console.WriteLine("enter the the ID of the product: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        p = myP.GetById(id);
                        Console.WriteLine(p);

                        break;
                    #endregion

                    case 'c':
                        #region print all the product
                        IEnumerable<Product> allP = myP.GetAll();
                        foreach (Product product in allP)
                            Console.WriteLine("\n" + product + "\n");
                        break;
                    #endregion

                    case 'd':
                        #region update an existing product

                        Console.WriteLine("enter the id of the product yow want to update: ");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");
                        p = myP.GetById(id);

                        do
                        {
                            Console.WriteLine(@"Which field do you want to update?
e: exit
t: titel
a: author of name
p: price
m: amount
c: category");

                            choose = (char)Console.Read();
                            Console.ReadLine();

                            switch (choose)
                            {
                                case 't':
                                    p.nameOfBook = Console.ReadLine();
                                    break;

                                case 'a':
                                    p.authorName = Console.ReadLine();
                                    break;

                                case 'p':
                                    if (!double.TryParse(Console.ReadLine(), out price))
                                        throw new Exception("The conversion failed");

                                    p.Price = price;
                                    break;

                                case 'm':
                                    if (!int.TryParse(Console.ReadLine(), out inStock))
                                        throw new Exception("The conversion failed");

                                    p.InStock = inStock;
                                    break;

                                case 'c':
                                    printCategories();
                                    if (!int.TryParse(Console.ReadLine(), out category))
                                        throw new Exception("The conversion failed");

                                    p.Category = (CATEGORY)category;
                                    break;
                                case 'e':
                                    break;

                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }

                        } while (choose != 'e');

                        myP.Update(p);

                        #endregion
                        break;

                    case 'e':
                        #region delete product by id
                        Console.WriteLine("enter the the ID of the product you want delete: ");

                        if (int.TryParse(Console.ReadLine(), out id))
                            myP.Delete(id);
                        break;
                    #endregion

                    case 'f':
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (choose != 'f');

    }

    static void subMenueOrder()
    {
        #region Variables we will use in the next loop
        Order ord = new Order();//for use in the next loop
        char choose;
        int id;
        DateTime dt;
        #endregion
        do
        {
            #region print menue for order and user choose
            Console.WriteLine(@"Choose the action you want:
    a: add a new order
    b: get order by id
    c: get all the orders
    d: update order
    e: delete order
    
    f: exit");

            choose = (char)Console.Read();
            Console.ReadLine();
            #endregion

            try
            {
                switch (choose)
                {
                    case 'a':
                        #region add new order.input details from the user
                        Console.WriteLine("Enter order details: name of costumer,email" +
                            ",address,creat order date,shipping date and delivery date");

                        //if (!int.TryParse(Console.ReadLine(), out id))
                        //    throw new Exception("The conversion failed");
                        //ord.ID = id;

                        ord.NameCustomer = Console.ReadLine();
                        ord.Email = Console.ReadLine();
                        ord.ShippingAddress = Console.ReadLine();

                        if (!DateTime.TryParse(Console.ReadLine(), out dt))
                            throw new Exception("The conversion failed");
                        ord.DateOrder = dt;

                        if (!DateTime.TryParse(Console.ReadLine(), out dt))
                            throw new Exception("The conversion failed");
                        ord.ShippingDate = dt;

                        if (!DateTime.TryParse(Console.ReadLine(), out dt))
                            throw new Exception("The conversion failed");
                        ord.DeliveryDate = dt;

                        id = myO.Add(ord);
                        Console.WriteLine("\n The id of this order will be :{0}\n", id);
                        break;
                    #endregion

                    case 'b':
                        #region print order by id
                        Console.WriteLine("enter the ID of the order: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        ord = myO.GetById(id);
                        Console.WriteLine(ord);

                        break;
                    #endregion

                    case 'c':
                        #region print all the order
                        IEnumerable<Order> allO = myO.GetAll();
                        foreach (Order order in allO)
                            Console.WriteLine(order);
                        break;
                    #endregion

                    case 'd':
                        #region update an existing product

                        Console.WriteLine("enter the id of the order yow want update: ");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");
                        ord = myO.GetById(id);

                        do
                        {
                            Console.WriteLine("\n" + @"Which field do you want to update?
e: end of update
n: name of costumer
m: email
a: address
c: creat order date
s: shipping date
d: delivery date");

                            choose = (char)Console.Read();
                            Console.ReadLine();

                            switch (choose)
                            {
                                case 'n':
                                    Console.WriteLine("enter the new name of customer:");
                                    ord.NameCustomer = Console.ReadLine();
                                    break;

                                case 'm':
                                    Console.WriteLine("enter the new email:");
                                    ord.Email = Console.ReadLine();
                                    break;

                                case 'a':
                                    Console.WriteLine("enter the new shipping address:");
                                    ord.ShippingAddress = Console.ReadLine();
                                    break;

                                case 'c':
                                    Console.WriteLine("enter the new date order:");
                                    if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                        throw new Exception("The conversion failed");
                                    ord.DateOrder = dt;
                                    break;

                                case 's':
                                    Console.WriteLine("enter the new shipping date:");
                                    if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                        throw new Exception("The conversion failed");
                                    ord.ShippingDate = dt;
                                    break;

                                case 'd':
                                    Console.WriteLine("enter the new delivery date:");
                                    if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                        throw new Exception("The conversion failed");
                                    ord.DeliveryDate = dt;
                                    break;

                                case 'e':
                                    break;

                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }

                        } while (choose != 'e');

                        myO.Update(ord);

                        #endregion
                        break;

                    case 'e':
                        #region delete order by id
                        Console.WriteLine("enter the the ID of the order you want delete: ");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        myO.Delete(id);
                        break;
                    #endregion

                    case 'f':
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (choose != 'f');
    }

    static void subMenueOrderItem()
    {
        #region Variables we will use in the next loop
        OrderItem OI = new();//for use in the next loop
        int id, idOrder, idProduct, numOfItem;
        //double price;
        char choose;
        List<OrderItem>? listOrderItems = new List<OrderItem>();
        #endregion

        do
        {
            #region print menue for order item and user choose
            Console.WriteLine(@"Choose the action you want:
    a: add a new OrderItem
    b: get OrderItem by id
    c: get all the OrderItems
    d: update OrderItem
    e: delete OrderItem
    f: get OrderItem by id of order and id of product
    g: get all the items in requested order
    
    h: exit");

            choose = (char)Console.Read();
            Console.ReadLine();
            #endregion

            try
            {
                switch (choose)
                {
                    case 'a':
                        #region add new orderItem.input details from the user

                        Console.WriteLine(@"Enter book's order details: IdOfOrder ,IdOfProduct and amountOfItem ");

                        //if (!int.TryParse(Console.ReadLine(), out id))
                        //    throw new Exception("The conversion failed");
                        //OI.ID = id;

                        if (!int.TryParse(Console.ReadLine(), out idOrder))
                            throw new Exception("The conversion failed");
                        OI.IdOfOrder = idOrder;

                        if (!int.TryParse(Console.ReadLine(), out idProduct))
                            throw new Exception("The conversion failed");
                        OI.IdOfProduct = idProduct;

                        if (!int.TryParse(Console.ReadLine(), out numOfItem))
                            throw new Exception("The conversion failed");

                        OI.amountOfItem = numOfItem;

                        id = myOI.Add(OI);
                        Console.WriteLine("\n the id of this ordrr item is:{0}\n", id);
                        break;
                    #endregion

                    case 'b':
                        #region print orderItem by id
                        Console.WriteLine("enter the the ID of the orderItem: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        OI = myOI.GetById(id);
                        Console.WriteLine(OI);
                        break;
                    #endregion

                    case 'c':
                        #region print all the orderItem
                        IEnumerable<OrderItem> allOI = myOI.GetAll();
                        foreach (OrderItem Oitem in allOI)
                            Console.WriteLine("\n" + Oitem + "\n");
                        break;
                    #endregion

                    case 'd':
                        #region update an existing orderItem

                        Console.WriteLine("enter the id of the orderItem yow want to update: ");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        OI = myOI.GetById(id);

                        do
                        {
                            Console.WriteLine(@"Which field do you want to update?
a: IdOfOrder
b: IdOfProduct
c: amount
d: to finish the update");

                            choose = (char)Console.Read(); //id,IdOfOrder,IdOfProduct,priceOfOneItem and amountOfItem
                            Console.ReadLine();



                            switch (choose)
                            {
                                case 'a':
                                    Console.WriteLine("enter the new ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out idOrder))
                                        throw new Exception("The conversion failed");
                                    OI.IdOfOrder = idOrder;
                                    break;

                                case 'b':
                                    Console.WriteLine("enter the new ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out idProduct))
                                        throw new Exception("The conversion failed");
                                    OI.IdOfProduct = idProduct;
                                    break;

                                case 'c':
                                    Console.WriteLine("enter the new amount: ");
                                    if (!int.TryParse(Console.ReadLine(), out numOfItem))
                                        throw new Exception("The conversion failed");
                                    OI.amountOfItem = numOfItem;
                                    break;

                                case 'd':
                                    break;

                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }
                        }
                        while (choose != 'd');

                        myOI.Update(OI);

                        #endregion
                        break;

                    case 'e':
                        #region delete orderIrem by id
                        Console.WriteLine("enter the the ID of the orderItem you want delete: ");

                        if (int.TryParse(Console.ReadLine(), out id))
                            myOI.Delete(id);
                        #endregion
                        break;

                    case 'f':
                        #region Get OrderItem by Order and Product ID
                        Console.WriteLine("enter the id of the Order number corresponding to the product id: ");
                        if (!int.TryParse(Console.ReadLine(), out idOrder))
                            throw new Exception("The conversion failed");
                        if (!int.TryParse(Console.ReadLine(), out idProduct))
                            throw new Exception("The conversion failed");

                        OI = myOI.GetByOrderIDProductID(idOrder, idProduct);
                        Console.WriteLine(OI);
                        break;
                    #endregion

                    case 'g':
                        #region give all the products in a specific order
                        Console.WriteLine("\nenter id of order: ");

                        if (!int.TryParse(Console.ReadLine(), out id))
                            throw new Exception("The conversion failed");

                        OI.ID = id;

                        listOrderItems = myOI.GetListByOrderID(id);
                        foreach (OrderItem item in listOrderItems)
                            Console.WriteLine("\n" + item);

                        #endregion
                        break;

                    case 'h':
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        while (choose != 'h');
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