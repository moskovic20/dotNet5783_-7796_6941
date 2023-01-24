using BlApi;
namespace BO;

public class OrderTracking
{
    /// <summary>
    /// מזהה ההזמנה
    /// </summary>
    public int OrderID { set; get; }

    /// <summary>
    /// מצב ההזמנה
    /// </summary>
    public OrderStatus Status { set; get; }

    /// <summary>
    /// תיאור התקדמות ההזמנה, צמדים של שלבים ותאריכים
    /// </summary>
    public List<Tuple<DateTime, string>?>? Tracking { set; get; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
