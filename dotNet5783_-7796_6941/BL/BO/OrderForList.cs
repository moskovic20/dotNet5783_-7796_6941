using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO;

public class OrderForList
{
    /// <summary>
    /// מספר מזהה של ההזמנה
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// שם הלקוח
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// מצב ההזמנה
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// מספר פריטים בהזמנה
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// מחיר סופי של ההזמה
    /// </summary>
    public  double TotalPrice { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
