using System.ComponentModel;

namespace PL.PO;


//public class OrderForList : INotifyPropertyChanged
//{
//    public event PropertyChangedEventHandler? PropertyChanged;

//    private int _OrderID;
//    public int OrderID
//    {
//        get
//        {
//            return _OrderID;
//        }
//        set
//        {
//            _OrderID = value;
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs("OrderID"));

//        }
//    }

//    private string? _CustomerName;
//    public string? CustomerName
//    {
//        get
//        {
//            return _CustomerName;
//        }
//        set
//        {
//            _CustomerName = value;
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs("CustomerName"));

//        }
//    }

//    private OrderStatus _Status;
//    public OrderStatus Status
//    {
//        get
//        {
//            return _Status;
//        }
//        set
//        {
//            _Status = value;
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
//            }
//        }
//    }

//    private int _AmountOfItems;
//    public int AmountOfItems
//    {
//        get
//        {
//            return _AmountOfItems;
//        }
//        set
//        {
//            _AmountOfItems = value;
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs("AmountOfItems"));

//        }
//    }

//    private double _TotalPrice;
//    public double TotalPrice
//    {
//        get
//        {
//            return _TotalPrice;
//        }
//        set
//        {
//            _TotalPrice = value;
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
//            }
//        }
//    }
//}

