using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    public int ProductID { get; set; }

    public double Price { get; set; }

    public CATEGORY Category { get; set; }

    public int InStock { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
