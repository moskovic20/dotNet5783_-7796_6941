using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; }

    public string? Email { get; set;}

    /// <summary>
    /// כתובת הלקוח, אליה צריך לשלוח את ההזמנה
    /// </summary>
    public string? ShippingAddress { get; set;}

    /// <summary>
    /// תאריך יצירת הזמנה
    /// </summary>
    public DateTime? DateOrder { get; set; }

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
    public DateTime ShippingDate { get; set; }

    /// <summary>
    /// תאריך אספקה
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    public List<OrderItem?>? Items { get; set; }

    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
