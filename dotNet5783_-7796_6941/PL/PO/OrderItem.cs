using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO;

public class OrderItem : MainPo
{

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
            OnPropertyChanged("OrderID");
        }
    }

    private int _ProductID;
    public int ProductID
    {
        get => _ProductID;
        set
        {
            _ProductID = value;
            OnPropertyChanged("ProductID");
        }
    }

    private string? _NameOfBook;
    public string? NameOfBook
    {
        get => _NameOfBook;
        set
        {
            _NameOfBook = value;
            OnPropertyChanged("NameOfBook");
        }
    }

    private double? _PriceOfOneItem;
    public double? PriceOfOneItem
    {
        get => _PriceOfOneItem;
        set
        {
            _PriceOfOneItem = value;
            OnPropertyChanged("PriceOfOneItem");
        }
    }

    private int _AmountOfItems;
    public int AmountOfItems
    {
        get => _AmountOfItems;
        set
        {
            _AmountOfItems = value;
            OnPropertyChanged("AmountOfItems");
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
