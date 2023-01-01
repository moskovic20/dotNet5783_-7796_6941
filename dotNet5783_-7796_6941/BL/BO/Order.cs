using BlApi;
namespace BO;

public class Order
{
    /// <summary>
    /// מזהה ההזמנה
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? CustomerName { get; set; } //ערך מתאפס- בגלל הקומפיילקר

    /// <summary>
    /// כתובת האימייל של הלקוח
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// כתובת הלקוח, אליה צריך לשלוח את ההזמנה
    /// </summary>
    public string? ShippingAddress { get; set; }

    /// <summary>
    /// תאריך יצירת הזמנה
    /// </summary>
    public DateTime DateOrder { get; set; }

    /// <summary>
    /// מצב הזמנה
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    ///תאריך ביצוע(תשלום) הזמנה
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// תאריך שילוח
    /// </summary>
    public DateTime? ShippingDate { get; set; }

    /// <summary>
    /// תאריך אספקה
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// רשימת הפריטים בהזמנה
    /// </summary>
    public List<OrderItem?>? Items { get; set; }

    /// <summary>
    /// המחיר לתשלום של כל ההזמנה
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
