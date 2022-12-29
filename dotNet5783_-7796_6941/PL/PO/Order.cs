using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PL.PO;

public class Order : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;//יטפל לנו בשינויים למסך ומהמסך

    private int _ID;
    public int ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerEmail"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ShippingAddress"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("DateOrder"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PaymentDate"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ShippingDate"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("DeliveryDate"));
            }
        }
    }


    private List<OrderItem?>? _Items;
    public List<OrderItem?>? Items
    {
        get
        {
            return _Items;
        }
        set
        {
            _Items = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }
    }


}

