using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PL.PO;

internal class Cart
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _CustomerName;
    public string? CustomerName
    {
        get=> _CustomerName;
        set
        {
            _CustomerName = value;
            if (PropertyChanged != null)
            {
                PropertyChanged!(this, new PropertyChangedEventArgs("CustomerName"));
            }
        }
    }


    private string? _CustomerEmail;
    public string? CustomerEmail
    {
        get => _CustomerEmail;
        set
        {
            _CustomerEmail = value;
            if(PropertyChanged != null)
            {
                PropertyChanged!(this, new PropertyChangedEventArgs("CustomerEmail"));
            }
        }
    }

    public string? _CustomerAddress;
    public string? CustomerAddress
    {
        get => _CustomerAddress;
        set
        {
            _CustomerAddress = value;
            if (PropertyChanged != null)
            {
                PropertyChanged!(this, new PropertyChangedEventArgs("CustomerAddress"));
            }
        }
    }

    
    public List<OrderItem>? Items { get; set; }
    //public ObservableCollection<OrderItem>? Items
    //{
    //    get
    //    { return Items; }
    //    set
    //    {
    //        _Items = value;
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs("Items"));
    //        }
    //    }
    //}


    private double? _TotalPrice;
    public double? TotalPrice
    {
        get => _TotalPrice;
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
