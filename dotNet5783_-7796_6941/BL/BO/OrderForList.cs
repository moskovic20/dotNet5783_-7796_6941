using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int OrderID { get; set; }

    public string? CuustomerName { get; set; }

    public OrderStatus Status { get; set; }

    public int AmountOfItems { get; set; }

    double TotalPrice { get; set; }
}
