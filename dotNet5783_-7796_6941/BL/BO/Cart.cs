using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// סל הקניות של הלקוח
/// </summary>
public class Cart
{
    /// <summary>
    /// שם הלקוח
    /// </summary>
    public string? CustomerName { get; set;}

    /// <summary>
    /// כתובת האימייל של הלקוח
    /// </summary>
    public string? CustomerEmail { get; set;}

    /// <summary>
    /// הכתובת של הקונה
    /// </summary>
    public int? CustomerAddress { get; set;}

    /// <summary>
    /// רשימת הפריטים בהזמנה
    /// </summary>
    public List<OrderItem?>? Items { get; set;}

    /// <summary>
    /// המחיר הכולל של ההזמנה
    /// </summary>
    public double?  TotalPrice { get; set;}

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
