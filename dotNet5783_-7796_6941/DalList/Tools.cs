using DalApi;
using Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public static class Tools
{

    public static int CalculateAmountItems(this Do.Order? order)
    {
        DalOrderItem help = new DalOrderItem();
        int amountOfItems = 0;
        if (order == null)
            throw new NotFounfException("missing ID");
        List<Do.OrderItem> listforAmount = help.GetListByOrderID(order.ID);
        amountOfItems = (int)listforAmount.Sum(o => o.amountOfItem);
        return amountOfItems;
    }

    public static int CalculatePriceOfAllItems(this Do.Order? order)
    {
        DalOrderItem help = new DalOrderItem();
        int Price = 0;

        List<Do.OrderItem> listforAmount = help.GetListByOrderID(order.ID);
        Price = (int)listforAmount.Sum(o => o.amountOfItem*o.priceOfOneItem);
        return Price;
    }
}
