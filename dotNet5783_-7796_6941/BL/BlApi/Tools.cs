using BO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlApi;

public static class Tools
{
    static private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");
    private static IEnumerable<Do.OrderItem?> listforAmount = new List<Do.OrderItem?>();


    #region הרחבה לטו סטרינג כולל טפלים
    public static string ToStringProperty<T>(this T t)
    {
        (string toStringProp, bool isTuple) = HelpToStringProperty(t, false);

        return isTuple ? new Regex(@"(Item1:|Item2:|True|,|\(|\))").Replace(toStringProp, "") : toStringProp;
    }
    #endregion

    #region  שיטת הרחבה עבור ToString 
    public static (string, bool) HelpToStringProperty<T>(this T t, bool isTupleOrOrderItem, string suffix = "")
    {

        string str = "";
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {

            var value = item.GetValue(t, null);
            if (value is string)
                str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";

            else if (value is IEnumerable)
            {
                var items = (IEnumerable)value;
                str += $"\n{item.Name}: ";
                var types = item.PropertyType.GetGenericArguments();
                if (types.Length > 0 && types != null && types[0].FullName.StartsWith("System.Tuple"))
                    isTupleOrOrderItem = true;

                if (types?.Length > 0 && types != null && types[0].FullName.EndsWith("BO.OrderItem"))
                    isTupleOrOrderItem = true;

                foreach (var item1 in items)
                    str += item1.HelpToStringProperty(isTupleOrOrderItem, "  ");
            }
            else
                str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
        }
        str += "\n";
        return (str, isTupleOrOrderItem);
    }
    #endregion

    #region   חישוב סטטוס להזמנה
    public static BO.OrderStatus calculateStatus(this Do.Order or)
    {
        if (or.DeliveryDate != null)
            return OrderStatus.Completed;
        if (or.ShippingDate != null)
            return OrderStatus.Processing;
        else
            return OrderStatus.Accepted;
    }
    #endregion

    #region חישוב מספר פריטים בכל הזמנה לפי מספר הזמנה
    public static int CalculateAmountItems(this Do.Order order)
    {

        int amountOfItems = 0;

        //if (order == null)
        //    throw new DoesntExistException("missing OrderID");

        listforAmount = dal.OrderItem.GetListByOrderID(order.ID);
        amountOfItems = listforAmount.Sum(o => (o?.AmountOfItems != null) ? 1 : 0);

        return amountOfItems;
    }
    #endregion

    #region  חישוב מחיר לסך כל ההזמנה על כל פריטיה
    public static double CalculatePriceOfAllItems(this Do.Order order)
    {
        double Price = 0;

        IEnumerable<Do.OrderItem?> listforAmount = dal.OrderItem.GetListByOrderID(order.ID); //list of OrderItem in this current order from dal by his OrderID 
        Price = listforAmount.Sum(o => (o?.AmountOfItems ?? 0) * (o?.PriceOfOneItem ?? 0));
        return Price;
    }
    #endregion

    #region  תחזור רשימה עם 3 איברים Tupleחישוב מסע ההזמנה ותיעוד ב


    public static List<Tuple<DateTime, string>?>? TrackingHealper(this Do.Order or)
    {
        List<Tuple<DateTime, string>?> list = new List<Tuple<DateTime, string>?>();

        //if (or.DateOrder != null)
        list.Add(new Tuple<DateTime, string>((DateTime)or.DateOrder, "order ordered"));
        if (or.ShippingDate != null)
            list.Add(new Tuple<DateTime, string>((DateTime)or.ShippingDate, "order shipped"));
        if (or.DeliveryDate != null)
            list.Add(new Tuple<DateTime, string>((DateTime)or.DeliveryDate, "order delivered"));

        return list;
    }
    #endregion

    #region המרת רשימה של אובייקטים מסוג פריט-הזמנה משכבת הנתונים לשכבת הלוגיקה עם השינויים הנדרשים
    public static BO.OrderItem? ListFromDoToBo(this Do.OrderItem? orderItems)
    {
        BO.OrderItem oIHelp = orderItems.CopyPropTo(new BO.OrderItem());
        oIHelp.NameOfBook = dal.Product.GetById(orderItems?.ProductID ?? 0).NameOfBook;//name of the product by his order OrderID
        oIHelp.TotalPrice = (orderItems?.PriceOfOneItem ?? 0) * (orderItems?.AmountOfItems ?? 0);
        return oIHelp;
    }
    #endregion

    #region המרת רשימה של אובייקטים מסוג פריט-הזמנה משכבת הלוגיקה (מהעגלה) לשכבת הנתונים עם השינויים הנדרשים
    public static Do.OrderItem ListFromBoToDo(this BO.OrderItem orItem, int OrId)
    {
        Do.Product product = dal.Product.GetById(orItem.ProductID); //הבאת פרטי מוצר

        Do.OrderItem orderItem = new();
        orderItem = orItem.CopyPropToStruct(orderItem);
        orderItem.OrderID = OrId;

        dal.OrderItem.Add(orderItem);

        Do.Product newProduct = new();//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
        newProduct = product.CopyPropToStruct(newProduct);
        newProduct.InStock -= orItem.AmountOfItems;

        dal.Product.Update(newProduct);

        return orderItem;
    }
    #endregion

    #region בדיקת תקינות קלט עבור כתובת אימייל
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
    #endregion

    #region בדיקת זמינות של מוצרים
    public static bool ValidationChecks(this BO.OrderItem item)
    {
        Do.Product product = dal.Product.GetById(item.ProductID);

        if (item.AmountOfItems < 1)
            throw new BO.InvalidValue_Exception("the amount of the book:" + item.NameOfBook + " is negative");

        if (product.InStock < item.AmountOfItems)
            throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:" + item.NameOfBook);

        return true;
    }
    #endregion

    ////PLפונקציה ממש לא יפה וחסכונית בעקרון רק בשביל להעביד את ה
    //public static OrderForList CastingOrToList(this BO.Order order)
    //{
    //    return new BO.OrderForList()
    //    {
    //        OrderID = order.orderID,
    //        CustomerName = dal.Order.GetById(order.orderID).CustomerName,
    //        Status = dal.Order.GetById(order.orderID).calculateStatus(),
    //        AmountOfItems = dal.Order.GetById(order.orderID).CalculateAmountItems(),
    //        TotalPrice = dal.Order.GetById(order.orderID).CalculatePriceOfAllItems()
    //    };
    //}
}