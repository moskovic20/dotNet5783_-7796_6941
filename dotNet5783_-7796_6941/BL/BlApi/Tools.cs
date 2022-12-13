using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {

            var value = item.GetValue(t, null);
            if (value is string)
                str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
            else
            {
                if (value is IEnumerable)
                {
                    str += $"\n{item.Name}: ";
                    foreach (var item2 in (IEnumerable)value)
                        str += item2.ToStringProperty("  ");
                }
                else
                    str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
            }
        }
        str += "\n";
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


    #region   חישוב סטטוס להזמנה
    public static BO.OrderStatus calculateStatus(this Do.Order or)
    {
        if (or.DeliveryDate != null)
            return OrderStatus.Completed;
        if (or.ShippingDate != null)
            return OrderStatus.Processing;
        else
            return OrderStatus.Pending;
    }
    #endregion


    #region חישוב מספר פריטים בכל הזמנה לפי מספר הזמנה
    public static int CalculateAmountItems(this Do.Order order)
    {

        int amountOfItems = 0;

        //if (order == null)
        //    throw new DoesntExistException("missing ID");

        List<Do.OrderItem?> listforAmount = (List<Do.OrderItem?>)dal.OrderItem.GetListByOrderID(order.ID);
        amountOfItems = listforAmount.Sum(o => o?.AmountOfItem ?? 0);

        return amountOfItems;
    }
    #endregion


    #region חישוב מחיר לסך כל ההזמנה על כל פריטיה
    public static double CalculatePriceOfAllItems(this Do.Order order)
    {
        double Price = 0;

        List<Do.OrderItem> listforAmount = (List<Do.OrderItem>)dal.OrderItem.GetListByOrderID(order.ID); //list of OrderItem in this current order from dal by his ID 
        Price = (double)listforAmount.Sum(o => o.AmountOfItem ?? 0 * o.PriceOfOneItem ?? throw new Exception("אין מחיר!!"));
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

    #region המרת רשימה של אובייקטים מסוג פריט-הזמנה משכבת הנתונים לשכבת הלוגיקה עם השינויים הנדרשים
    public static BO.OrderItem? ListFromDoToBo(this Do.OrderItem orderItems)
    => new BO.OrderItem() //casting from list <do.ordetitem > to list<bo.orderitem>
    {
            ID = orderItems.ID,
            NameOfBook = (dal.Product.GetById(orderItems.ID)).NameOfBook,//name of the product by his order ID
            PriceOfOneItem = orderItems.PriceOfOneItem,
            AmountOfItems = orderItems.AmountOfItem ?? 0,///
        TotalPrice = orderItems.PriceOfOneItem * orderItems.AmountOfItem
    };
        
    
    #endregion


    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool ValidationChecks( this BO.OrderItem item)
    {
        Do.Product product = dal.Product.GetById(item.ProductID);

        if (item.AmountOfItems < 1)
            throw new BO.InvalidValue_Exception("the amount of the book:" + item.NameOfBook + " is negative");

        if (product.InStock < item.AmountOfItems)
            throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:" + item.NameOfBook);

        return true; ;
    }


}