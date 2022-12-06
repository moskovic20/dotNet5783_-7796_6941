﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// מזהה ההזמנה
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// כתובת האימייל של הלקוח
    /// </summary>
    public string? Email { get; set;}

    /// <summary>
    /// כתובת הלקוח, אליה צריך לשלוח את ההזמנה
    /// </summary>
    public string? ShippingAddress { get; set;}

    /// <summary>
    /// מצב ההזמנה
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    ///תאריך ביצוע הזמנה
    /// </summary>
    public DateTime? PaymentDate { get; set; }
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
