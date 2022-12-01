using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{

    public int ID { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public BL_CATEGORY? Category { get; set; }

    /// <summary>
    /// The amount of the product in the customer's shopping cart.
    /// כמות המוצר בסל הקניות של הלקוח
    /// </summary>
    public int Amount { get; set; }

    public bool InStock { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
