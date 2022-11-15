using Do;
using Dal;
using System.Collections.Generic;
using System.Security.Cryptography;
using static Do.Enums;
using System.Diagnostics;

namespace Dal;

internal class Program
{
    private static DalProduct myP = new();
    private static DalOrder myO = new();
    private static DalOrderItem myOI = new();

    static void Main(string[] arg)
    {
        int choice;
        do
        {
            Console.WriteLine(@"Select one of the following data entities:
    0: exit""
    1: product
    2: order
    3: orderItem");


            if (!int.TryParse(Console.ReadLine(), out choice))
                throw new Exception("The conversion failed");

            switch (choice)
            {
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

                    p.Category = (Enums.CATEGORY)category;

                    myP.Add(p);
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

                                p.Category = (Enums.CATEGORY)category;
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

                default:
                    Console.WriteLine("ERROR");
                    break;
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
            #endregion

            switch (choose)
            {
                case 'a':
                    #region add new order.input details from the user
                    Console.WriteLine("Enter order details:id,name of costumer,email" +
                        ",address,creat order date,shipping date and delivery date");
                    Console.ReadLine();

                    if (!int.TryParse(Console.ReadLine(), out id))
                        throw new Exception("The conversion failed");
                    ord.ID = id;

                    ord.NameCustomer = Console.ReadLine();
                    ord.Email = Console.ReadLine();
                    ord.ShippingAddress = Console.ReadLine();

                    if(!DateTime.TryParse(Console.ReadLine(),out dt))
                        throw new Exception("The conversion failed");
                    ord.DateOrder = dt;

                    if (!DateTime.TryParse(Console.ReadLine(), out dt))
                        throw new Exception("The conversion failed");
                    ord.ShippingDate = dt;

                    if (!DateTime.TryParse(Console.ReadLine(), out dt))
                        throw new Exception("The conversion failed");
                    ord.DeliveryDate = dt;

                    myO.Add(ord);
                    break;
                #endregion

                case 'b':
                    #region print order by id
                    Console.WriteLine("enter the the ID of the order: ");

                    if(!int.TryParse(Console.ReadLine(),out id))
                        throw new Exception("The conversion failed");

                    ord = myO.GetById(id);
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
                    #region update an existing product

                    Console.WriteLine("enter the id of the order yow want update: ");

                    if (!int.TryParse(Console.ReadLine(), out id))
                        throw new Exception("The conversion failed");
                    ord = myO.GetById(id);

                    do
                    {
                        Console.WriteLine(@"Which field do you want to update?
e: exit
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
                                ord.NameCustomer = Console.ReadLine();
                                break;

                            case 'm':
                                ord.Email = Console.ReadLine();
                                break;

                            case 'a':
                                ord.ShippingAddress = Console.ReadLine();
                                break;

                            case 'c':
                                if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                    throw new Exception("The conversion failed");
                                ord.DateOrder = dt;
                                break;

                            case 's':
                                if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                    throw new Exception("The conversion failed");
                                ord.ShippingDate = dt;
                                break;

                            case 'd':
                                if (!DateTime.TryParse(Console.ReadLine(), out dt))
                                    throw new Exception("The conversion failed");
                                ord.DeliveryDate = dt;
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

                    if(!int.TryParse(Console.ReadLine(),out id))
                        throw new Exception("The conversion failed");

                    myO.Delete(id);
                    break;
                    #endregion
            }


        } while (choose != 'f');
    }

    static void subMenueOrderItem()
    {
        #region print menue for order item and user choose
        Console.WriteLine(@"Choose the action you want:
    a: add a new OrderItem
    b: get OrderItem by id
    c: get all the OrderItems
    d: update OrderItem
    e: delete OrderItem
    
    f: exit");

        char choose = (char)Console.Read();
        Console.ReadLine();
        #endregion

        OrderItem OI = new();//for use in the next loop
        int id,idOrder,idProduct, numOfItem;
        double price;

        while (choose != 'f')
        {
            switch (choose)
            {
                case 'a':
                    #region add new orderItem.input details from the user

                    Console.WriteLine(@"Enter book's order details:id,IdOfOrder,IdOfProduct,priceOfOneItem and amountOfItem ");

                    if (!int.TryParse(Console.ReadLine(), out id))
                        throw new Exception("The conversion failed");
                    OI.ID = id;

                    if (!int.TryParse(Console.ReadLine(), out idOrder))
                        throw new Exception("The conversion failed");
                    OI.IdOfOrder = idOrder;

                    if (!int.TryParse(Console.ReadLine(), out idProduct))
                        throw new Exception("The conversion failed");
                    OI.IdOfProduct = idProduct;

                    if (!double.TryParse(Console.ReadLine(), out price))
                        throw new Exception("The conversion failed");

                    OI.priceOfOneItem = price;

                    if (!int.TryParse(Console.ReadLine(), out numOfItem))
                        throw new Exception("The conversion failed");

                    OI.amountOfItem = numOfItem;

                    myOI.Add(OI);
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

                    Console.WriteLine(@"Which field do you want to update?
o: IdOfOrder
d: IdOfProduct
p: price
m: amount

f: to finish the update");

                    choose = (char)Console.Read(); //id,IdOfOrder,IdOfProduct,priceOfOneItem and amountOfItem
                    Console.ReadLine();

                    while (choose != 'f')
                    {
                        switch (choose)
                        {
                            case 'o':
                                if (!int.TryParse(Console.ReadLine(), out idOrder))
                                    throw new Exception("The conversion failed");
                                OI.IdOfOrder = idOrder;
                                break;

                            case 'd':
                                if (!int.TryParse(Console.ReadLine(), out idProduct))
                                    throw new Exception("The conversion failed");
                                OI.IdOfProduct = idProduct;
                                break;

                            case 'p':
                                if (!double.TryParse(Console.ReadLine(), out price))
                                    throw new Exception("The conversion failed");
                                OI.priceOfOneItem = price;
                                break;

                            case 'm':
                                if (!int.TryParse(Console.ReadLine(), out numOfItem))
                                    throw new Exception("The conversion failed");
                                OI.amountOfItem = numOfItem;
                                break;

                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                    }

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

                default:
                    Console.WriteLine("ERROR");
                    break;
            }
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