using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO;

internal class OrderItem
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _OrderID;
    public int OrderID
    {
        get
        {
            return _OrderID;
        }
        set
        {
            _OrderID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("OrderID"));
            }
        }
    }

    private int _ProductID;
    public int ProductID
    {
        get => _ProductID;
        set
        {
            _ProductID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductID"));
            }
        }
    }


    private string? _NameOfBook;
    public string? NameOfBook
    {
        get => _NameOfBook;
        set
        {
            _NameOfBook = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductID"));
            }
        }
    }

    private double? _PriceOfOneItem;
    public double? PriceOfOneItem
    {
        get => _PriceOfOneItem;
        set
        {
            _PriceOfOneItem = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PriceOfOneItem"));
            }
        }
    }


    private int _AmountOfItems;
    public int AmountOfItems
    {
        get => _AmountOfItems;
        set
        {
            _AmountOfItems = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("AmountOfItems"));
            }
        }
    }

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
