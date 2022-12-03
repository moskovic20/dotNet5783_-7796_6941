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
    #region חישוב מספר פריטים בכל הזמנה לפי מספר הזמנה
    public static int CalculateAmountItems(this Do.Order? order)
    {
        DalOrderItem help = new DalOrderItem();

        int amountOfItems = 0;

        if (order == null)
            throw new DoesntExistException("missing ID");

        List<Do.OrderItem?> listforAmount = help.GetListByOrderID(order.GetValueOrDefault().ID);
        amountOfItems = (int)listforAmount.Sum(o => o.GetValueOrDefault().amountOfItem??0);

        return amountOfItems;
    }
    #endregion

    #region חישוב מחיר לסך כל ההזמנה על כל פריטיה
    public static double CalculatePriceOfAllItems(this Do.Order? order)
    {
        DalOrderItem help = new DalOrderItem();
        int Price = 0;

        List<Do.OrderItem?> listforAmount = help.GetListByOrderID(order.GetValueOrDefault().ID);
        Price =(int)listforAmount.Sum(o => o.GetValueOrDefault().amountOfItem??0*o.GetValueOrDefault().priceOfOneItem?? throw new Exception("אין מחיר!!"));
        return Price;
    }

    #endregion
}
