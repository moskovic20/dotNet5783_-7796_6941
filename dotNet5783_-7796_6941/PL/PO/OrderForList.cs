using System.ComponentModel;

namespace PL.PO;


public class OrderForList : MainPo
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

    private int _AmountOfItems;
    public int AmountOfItems
    {
        get
        {
            return _AmountOfItems;
        }
        set
        {
            _AmountOfItems = value;
            OnPropertyChanged("AmountOfItems");

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

