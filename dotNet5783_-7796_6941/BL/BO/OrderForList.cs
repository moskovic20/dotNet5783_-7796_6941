using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO;

public class OrderForList
{
    public int OrderID { get; set; }

    public string? CustomerName { get; set; }

    public OrderStatus Status { get; set; }

    public int AmountOfItems { get; set; }

    public  double TotalPrice { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
