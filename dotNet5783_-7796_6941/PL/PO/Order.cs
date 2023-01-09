using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PL.PO;

public class Order : MainPo
{
 
    private int _ID;
    public int OrderID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
            OnPropertyChanged("OrderID");
        }
    }

    private string? _CustomerName;
    public string? CustomerName
    {
        get
        {
            return _CustomerName;
        }
        set
        {
            _CustomerName = value;
            OnPropertyChanged("CustomerName");
        }
    }

    private string? _CustomerEmail;
    public string? CustomerEmail
    {
        get
        {
            return _CustomerEmail;
        }
        set
        {
            _CustomerEmail = value;
            OnPropertyChanged("CustomerEmail");
        }
    }

    private string? _ShippingAddress;
    public string? ShippingAddress
    {
        get
        {
            return _ShippingAddress;
        }
        set
        {
            _ShippingAddress = value;
            OnPropertyChanged("ShippingAddress");
        }
    }

    private DateTime _DateOrder;
    public DateTime DateOrder
    {
        get
        {
            return _DateOrder;
        }
        set
        {
            _DateOrder = value;
            OnPropertyChanged("DateOrder");
        }
    }

    private OrderStatus _Status;
    public OrderStatus Status
    {
        get
        {
            return _Status;
        }
        set
        {
            _Status = value;
            OnPropertyChanged("Status");
        }
    }

    private DateTime _PaymentDate;
    public DateTime PaymentDate
    {
        get
        {
            return _PaymentDate;
        }
        set
        {
            _PaymentDate = value;
            OnPropertyChanged("PaymentDate");
        }
    }

    private DateTime? _ShippingDate;
    public DateTime? ShippingDate
    {
        get
        {
            return _ShippingDate;
        }
        set
        {
            _ShippingDate = value;
            OnPropertyChanged("ShippingDate");
        }
    }

    private DateTime? _DeliveryDate;
    public DateTime? DeliveryDate
    {
        get
        {
            return _DeliveryDate;
        }
        set
        {
            _DeliveryDate = value;
            OnPropertyChanged("DeliveryDate");
        }
    }


    private ObservableCollection<OrderItem>? _Items;
    public ObservableCollection<OrderItem>? Items
    {
        get
        { 
        return _Items; 
        }
        set
        {
            _Items = value;
            OnPropertyChanged("Items");
        }
    }

    private double _TotalPrice;
    public double TotalPrice
    {
        get
        {
            return _TotalPrice;
        }
        set
        {
            _TotalPrice = value;
            OnPropertyChanged("TotalPrice");
        }
    }

}

