using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PL.PO;
public class ProductItem : MainPo
{

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
            OnPropertyChanged("ID");
        }
    }

    private string? _NameOfBook;
    public string? NameOfBook
    {
        get
        {
            return _NameOfBook;
        }
        set
        {
            _NameOfBook = value;
            OnPropertyChanged("NameOfBook");
        }
    }

    private double? _Price;
    public double? Price
    {
        get
        {
            return _Price;
        }
        set
        {
            _Price = value;
            OnPropertyChanged("Price");
        }
    }

    private CATEGORY _Category;
    public CATEGORY Category
    {
        get
        {
            return _Category;
        }
        set
        {
            _Category = value;
            OnPropertyChanged("Category");
        }
    }

    //private string? _Summary;
    //public string? Summary
    //{
    //    get
    //    {
    //        return _Summary;
    //    }
    //    set
    //    {
    //        _Summary = value;
    //        OnPropertyChanged("Summary");
    //    }
    //}

    private int? _AmountInCart;
    public int? AmountInCart
    {
        get
        {
            return _AmountInCart;
        }
        set
        {
            _AmountInCart = value;
            OnPropertyChanged("AmountInCart");
        }
    }

    private bool _InStock;
    public bool InStock
    {
        get
        {
            return _InStock;
        }
        set
        {
            _InStock = value;
            OnPropertyChanged("InStock");
        }
    }

    public string? productImagePath;
    public string? ProductImagePath
    {
        get => productImagePath;
        set
        {
            productImagePath = value;

            //try { image = new BitmapImage(new System.Uri(Environment.CurrentDirectory + productImagePath)); }
            try { image = new BitmapImage(new System.Uri(productImagePath ?? throw new Exception("problem"))); }
            catch { image = null; }
        }
    }

    private BitmapImage? image;
    public BitmapImage? Image
    {
        get
        {
            return image;
        }
        set
        {
            image = value;
            OnPropertyChanged("Image");
        }
    }


}