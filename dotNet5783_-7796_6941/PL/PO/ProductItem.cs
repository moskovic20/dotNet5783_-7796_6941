using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PL.PO;
public class ProductItem : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

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
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("NameOfBook"));

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));

        }
    }

    private string? _Summary;
    public string? Summary
    {
        get
        {
            return _Summary;
        }
        set
        {
            _Summary = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Summary"));

        }
    }

    private int? _AmountInCart; //filing good like i should (-_-) ;P :} i hate nullableeeeee!
    public int? AmountInCart
    {
        get
        {
            return _AmountInCart;
        }
        set
        {
            _AmountInCart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("AmountInCart"));

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("InStock"));

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));

        }
    }


}