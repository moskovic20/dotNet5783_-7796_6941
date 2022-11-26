﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int OrderID { get; set; }

    public string? CustomerEmail { get; set;}

    public string? CustomerAdress { get; set;}

    public DateTime? OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime PaymentDate { get; set; }

    public DateTime ShipDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public OrderItem? Items { get; set; }

    public double TotalPrice { get; set; }
}
