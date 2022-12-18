using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO;

public class ProductForList
{
    public int ID { get; set; }

    public string NameOfBook { get; set; }

    public double? Price { get; set; }

    public CATEGORY Category { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
