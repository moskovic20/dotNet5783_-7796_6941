using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do;
/// <summary>
/// structor for ditales of this item to relate the products to his ordet
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// unique number to this item in this current order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ID for the Order to connect all his product to the same costumer's order
    /// </summary>
    public int? IdOfOrder { get; set; }

    /// <summary>
    /// ID of the product in store
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// price of this current item on store
    /// </summary>
    public double? PriceOfOneItem { get; set; }

    /// <summary>
    /// number of this book in the order
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// Should this orderItem be considered deleted?
    /// </summary>
    public bool IsDeleted { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }


}

