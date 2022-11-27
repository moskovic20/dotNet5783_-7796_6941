using Do;

using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    DataSource _DS = DataSource.GetInstance();

    public int Add(OrderItem myOrderItem)
    {
        myOrderItem.IsDeleted = false;

        int indexOfMyOrderItem = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == myOrderItem.ID);

        if (indexOfMyOrderItem == -1) //myOrderItem.ID is not found in _OrderItems
        {
            myOrderItem.ID = DataSource.Config.NextOrderItem;
            _DS._OrderItems.Add(myOrderItem);
            return myOrderItem.ID;
        }

        if (_DS._OrderItems[indexOfMyOrderItem].GetValueOrDefault().IsDeleted == false)
            throw new Exception("The order item you wish to add is already exists");

        _DS._OrderItems.Add(myOrderItem);
        return myOrderItem.ID;

    }

    public void Delete(int id)
    {
        int indexOfOItemById = _DS._OrderItems.FindIndex(x => x.GetValueOrDefault().ID == id && x.GetValueOrDefault().IsDeleted != true);  ///

        if (indexOfOItemById == -1)
            throw new Exception("The order item you wanted to delete is not found");


        OrderItem myOItem = _DS._OrderItems[indexOfOItemById].GetValueOrDefault();

        if (myOItem.IsDeleted == true)
            throw new Exception("The order item you wanted to delete has already been deleted");


        myOItem.IsDeleted = true;
        _DS._OrderItems[indexOfOItemById] = myOItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_DS._OrderItems == null)
            throw new Exception("there is not any orderItem");

        IEnumerable<OrderItem> allOrderItems = (IEnumerable<OrderItem>)_DS._OrderItems.FindAll(x => x.GetValueOrDefault().IsDeleted != true);
        return allOrderItems;
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID ==
                                                      id && x.GetValueOrDefault().IsDeleted != true);

        if (OItem.GetValueOrDefault().ID == 0)
            throw new Exception("The order item is not found");

        return (OrderItem)OItem;
    }

    public void Update(OrderItem item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new Exception("the order item can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    public List<OrderItem> GetListByOrderID(int OrderID)
    {
        List<OrderItem> list = new List<OrderItem>();

        foreach (OrderItem OItem in _DS._OrderItems)
        {
            if (OItem.IdOfOrder == OrderID && OItem.IsDeleted != true)
                list.Add(OItem);
        }
        if (list == null)
            throw new Exception("The order items are not found or this order is't exist");
        return list;
    }

    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem? OItem = _DS._OrderItems.Find(x => x.GetValueOrDefault().ID == OrderID &&
                     x.GetValueOrDefault().IsDeleted != true && x.GetValueOrDefault().IdOfProduct == ProductID);

        if (OItem == null)
            throw new Exception("The order item is not found");

        return (OrderItem)OItem;

    }
}

/*
Select one of the following data entities:
    0: exit
    1: product
    2: order
    3: orderItem
3
Choose the action you want:
    a: add a new OrderItem
    b: get OrderItem by id
    c: get all the OrderItems
    d: update OrderItem
    e: delete OrderItem
    f: get OrderItem by id of order and id of product
    g: get all the items in requested order
    h: exit
c
        Ordered product ID : 100001
        Order ID : 100002
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 1
        Ordered product ID : 100002
        Order ID : 100004
    Product ID : 908035788
    Price of one item : 125
    The amount of items : 2
        Ordered product ID : 100003
        Order ID : 100019
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 4
        Ordered product ID : 100004
        Order ID : 100005
    Product ID : 599210032
    Price of one item : 119
    The amount of items : 4
        Ordered product ID : 100005
        Order ID : 100002
    Product ID : 483839896
    Price of one item : 75
    The amount of items : 0
        Ordered product ID : 100006
        Order ID : 100007
    Product ID : 948646327
    Price of one item : 89
    The amount of items : 0
        Ordered product ID : 100007
        Order ID : 100009
    Product ID : 908035788
    Price of one item : 125
    The amount of items : 3
        Ordered product ID : 100008
        Order ID : 100006
    Product ID : 908035788
    Price of one item : 125
    The amount of items : 1
        Ordered product ID : 100009
        Order ID : 100000
    Product ID : 40302314
    Price of one item : 120
    The amount of items : 0
        Ordered product ID : 100010
        Order ID : 100009
    Product ID : 40302314
    Price of one item : 120
    The amount of items : 0
        Ordered product ID : 100011
        Order ID : 100014
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 0
        Ordered product ID : 100012
        Order ID : 100006
    Product ID : 483839896
    Price of one item : 75
    The amount of items : 3
        Ordered product ID : 100013
        Order ID : 100005
    Product ID : 908035788
    Price of one item : 125
    The amount of items : 1
        Ordered product ID : 100014
        Order ID : 100014
    Product ID : 40302314
    Price of one item : 120
    The amount of items : 4
        Ordered product ID : 100015
        Order ID : 100014
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 3
        Ordered product ID : 100016
        Order ID : 100015
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 1
        Ordered product ID : 100017
        Order ID : 100012
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 1
        Ordered product ID : 100018
        Order ID : 100001
    Product ID : 934266859
    Price of one item : 131
    The amount of items : 4
        Ordered product ID : 100019
        Order ID : 100000
    Product ID : 948646327
    Price of one item : 89
    The amount of items : 1
        Ordered product ID : 100020
        Order ID : 100014
    Product ID : 934266859
    Price of one item : 131
    The amount of items : 0
        Ordered product ID : 100021
        Order ID : 100003
    Product ID : 599210032
    Price of one item : 119
    The amount of items : 4
        Ordered product ID : 100022
        Order ID : 100008
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 1
        Ordered product ID : 100023
        Order ID : 100000
    Product ID : 908035788
    Price of one item : 125
    The amount of items : 2
        Ordered product ID : 100024
        Order ID : 100010
    Product ID : 948646327
    Price of one item : 89
    The amount of items : 2
        Ordered product ID : 100025
        Order ID : 100017
    Product ID : 483839896
    Price of one item : 75
    The amount of items : 3
        Ordered product ID : 100026
        Order ID : 100017
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 1
        Ordered product ID : 100027
        Order ID : 100005
    Product ID : 40302314
    Price of one item : 120
    The amount of items : 3
        Ordered product ID : 100028
        Order ID : 100013
    Product ID : 948646327
    Price of one item : 89
    The amount of items : 0
        Ordered product ID : 100029
        Order ID : 100012
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 4
        Ordered product ID : 100030
        Order ID : 100005
    Product ID : 934266859
    Price of one item : 131
    The amount of items : 3
        Ordered product ID : 100031
        Order ID : 100015
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 0
        Ordered product ID : 100032
        Order ID : 100010
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 4
        Ordered product ID : 100033
        Order ID : 100001
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 4
        Ordered product ID : 100034
        Order ID : 100017
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 4
        Ordered product ID : 100035
        Order ID : 100015
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 0
        Ordered product ID : 100036
        Order ID : 100010
    Product ID : 759070960
    Price of one item : 73
    The amount of items : 0
        Ordered product ID : 100037
        Order ID : 100014
    Product ID : 539548419
    Price of one item : 132
    The amount of items : 2
        Ordered product ID : 100038
        Order ID : 100013
    Product ID : 934266859
    Price of one item : 131
    The amount of items : 3
        Ordered product ID : 100039
        Order ID : 100018
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 1
        Ordered product ID : 100040
        Order ID : 100005
    Product ID : 934266859
    Price of one item : 131
    The amount of items : 0
Choose the action you want:
    a: add a new OrderItem
    b: get OrderItem by id
    c: get all the OrderItems
    d: update OrderItem
    e: delete OrderItem
    f: get OrderItem by id of order and id of product
    g: get all the items in requested order
    h: exit
g
enter id of order:
100018
        Ordered product ID : 100039
        Order ID : 100018
    Product ID : 391438361
    Price of one item : 81
    The amount of items : 1
Choose the action you want:
    a: add a new OrderItem
    b: get OrderItem by id
    c: get all the OrderItems
    d: update OrderItem
    e: delete OrderItem
    f: get OrderItem by id of order and id of product
    g: get all the items in requested order
    h: exit
h
Select one of the following data entities:
    0: exit
    1: product
    2: order
    3: orderItem
0 
*/