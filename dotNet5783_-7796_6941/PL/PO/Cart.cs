using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PL.PO;

internal class Cart : MainPo
{
    
    private string? _CustomerName;
    public string? CustomerName
    {
        get=> _CustomerName;
        set
        {
            _CustomerName = value;     
             OnPropertyChanged("CustomerName");
        }
    }


    private string? _CustomerEmail;
    public string? CustomerEmail
    {
        get => _CustomerEmail;
        set
        {
            _CustomerEmail = value;

            OnPropertyChanged("CustomerEmail");
            
        }
    }

    public string? _CustomerAddress;
    public string? CustomerAddress
    {
        get => _CustomerAddress;
        set
        {
            _CustomerAddress = value;
            OnPropertyChanged("CustomerAddress");
        }
    }


    private ObservableCollection<OrderItem>? _Items;
    public ObservableCollection<OrderItem>? Items
    {
        get
        {
            return _Items; }
        set
        {
            _Items = value;
            OnPropertyChanged("Items");
        }
    }


    private double? _TotalPrice;
    public double? TotalPrice
    {
        get => _TotalPrice;
        set
        {
            _TotalPrice = value;
            OnPropertyChanged("TotalPrice");
           
        }
    }
}
