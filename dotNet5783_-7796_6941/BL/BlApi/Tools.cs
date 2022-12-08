﻿using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public static class Tools
{
    static private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    /// <summary>
    /// שיטת הרחבה עבור ToString
    /// </summary>
    /// <typeparam name="T">generic type</typeparam>
    /// <param name="t">"this" type</param>
    /// <returns></returns>
    public static string ToStringProperty<T>(this T t, string suffix = "") //מעבר כולל גם על אוספים
    {
        string str = "";
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {

            var value = item.GetValue(t, null);
            if (value is IEnumerable)
            {
                str += $"\n{item.Name}: ";
                foreach (var item2 in (IEnumerable)value)
                    str += item2.ToStringProperty("  ");
            }
            else
                str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
        }
        return str;
    }

    //public static string ToStringProperty<T>(this T? t)                   
    //{

    //    string str = "";
    //    if (t == null) return str;

    //    foreach (PropertyInfo item in t.GetType().GetProperties())
    //        str += "\n" + item.Name ?? "no name" + ": " + item.GetValue(t, null);
    //    return str;
    //}

    //copy elements of BO to DO and vice versa
    public static void CopyPropertiesTo<T, S>(this S from, T to)
    {
        foreach (PropertyInfo propTo in to.ToStringProperty().GetType().GetProperties())//loop on all the properties in the new object
        {
            PropertyInfo? propFrom = typeof(S).GetProperty(propTo.Name);//check if there is property with the same name in the source object and get it
            if (propFrom == null)
                continue;
            var value = propFrom.GetValue(from, null);//get the value of the prperty
            if (value is ValueType || value is string)
                propTo.SetValue(to, value);//insert the value to the suitable property
        }
    }

    public static object? CopyPropertiesToNew<S>(this S from, Type type)//get the typy we want to copy to 
    {
        object? to = Activator.CreateInstance(type); // new object of the Type
        from.CopyPropertiesTo(to);//copy all value of properties with the same name to the new object
        return to;
    }


    #region   חישוב סטטוס להזמנה וזריקת חריגות
    public static OrderStatus calculateStatus(this Do.Order order)
    {
        DateTime? DateO = order.DateOrder;
        DateTime? ShippingD = order.ShippingDate;
        DateTime? DeliveryD = order.DeliveryDate;

        #region חריגות אפשריות בזמנים
        if (DateO == null)
            throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        if (ShippingD == null && DeliveryD == null)
            throw new ArgumentNullException("cant calculate status, there is no info"); ////////exceptions
        if (ShippingD == null && DeliveryD != null || ShippingD != null && DateO > ShippingD
                   || DeliveryD != null && DateO > DeliveryD || ShippingD != null && DeliveryD != null && ShippingD > DeliveryD)
            throw new ArgumentException("rong information,cant be possible");          /////////exceptions
        #endregion

        // -------------Calculate the Status--------------

        if (ShippingD == null)
            return OrderStatus.Pending;
        if (DeliveryD == null)
            return OrderStatus.Processing;
        else
            return OrderStatus.Completed;
    }
    #endregion

    #region חישוב מספר פריטים בכל הזמנה לפי מספר הזמנה
    public static int CalculateAmountItems(this Do.Order order)
    {

        int amountOfItems = 0;

        //if (order == null)
        //    throw new DoesntExistException("missing ID");

        List<Do.OrderItem?> listforAmount = (List<Do.OrderItem?>)dal.OrderItem.GetListByOrderID(order.ID);
        amountOfItems = listforAmount.Sum(o => o?.amountOfItem ?? 0);

        return amountOfItems;
    }
    #endregion


    #region חישוב מחיר לסך כל ההזמנה על כל פריטיה
    public static double CalculatePriceOfAllItems(this Do.Order order)
    {
        double Price = 0;

        List<Do.OrderItem> listforAmount = (List<Do.OrderItem>)dal.OrderItem.GetListByOrderID(order.ID);
        Price = (double)listforAmount.Sum(o => o.amountOfItem ?? 0 * o.priceOfOneItem ?? throw new Exception("אין מחיר!!"));
        return Price;
    }
    #endregion

    #region Tupleחישוב מסע ההזמנה ותיעוד ב
    public static List<Tuple<DateTime, string>?>? TrackingHealper(this Do.Order or)
    {
        List<Tuple<DateTime, string>?> list = new List<Tuple<DateTime, string>?>()
        {
                (or.DateOrder!= null)? new Tuple<DateTime, string>((DateTime)or.DateOrder, "order ordered"):null,
                (or.ShippingDate!= null)? new Tuple<DateTime, string>((DateTime)or.ShippingDate  , "order shipped" ):null,
                (or.DeliveryDate!= null)? new Tuple<DateTime, string>((DateTime)or.DeliveryDate , "order delivered"):null,
        };
        return list;
    }
    #endregion
    
}