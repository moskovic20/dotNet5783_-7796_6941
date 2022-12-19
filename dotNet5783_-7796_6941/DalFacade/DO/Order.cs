using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Do;

/// <summary>
/// Structure for order that contains details about the customer
/// and the items in the order.
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique identifier for order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? CustomerName { get; set; } //ערך מתאפס- בגלל הקומפיילק

    /// <summary>
    /// Email addrres of the customer
    /// </summary>
    public string? CustomerEmail { get; set; } //כנ"ל

    /// <summary>
    /// The address to which the order should be sent
    /// </summary>
    public string? ShippingAddress { get; set; }

    /// <summary>
    /// The date the order was created
    /// תאריך ביצוע ההזמנה
    /// </summary>
    public DateTime DateOrder { get; set; }

    /// <summary>
    /// The date the shipment was launched.
    /// התאריך שהמשלוח יצא לדרך
    /// </summary>
    public DateTime? ShippingDate { get; set; }

    /// <summary>
    /// The date the shipment arrived at its destination
    /// תאריך הגעת המשלוח ליעד
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Has the order been deleted?
    /// האם ההזמנה נמחקה ממאגר הרשימות
    /// </summary>
    public bool IsDeleted { get; set; }


    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
