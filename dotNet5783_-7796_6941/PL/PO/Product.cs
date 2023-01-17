using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PL.PO
{

    public class Product : MainPo
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

        private string? _AuthorName;
        public string? AuthorName
        {
            get
            {
                return _AuthorName;
            }
            set
            {
                _AuthorName = value;
                OnPropertyChanged("AuthorName");
            }
        }

        private PO.CATEGORY? _Category;
        public PO.CATEGORY? Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                OnPropertyChanged("Category");

            }
        }

        private string? _Summary;
        public string? Summary
        {
            get { return _Summary; }
            set
            {
                _Summary = value;
                OnPropertyChanged("Summary");

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

        private int? _InStock;
        public int? InStock
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
                try
                {
                    
                     image = new BitmapImage(new System.Uri(/*Environment.CurrentDirectory +*/ productImagePath ?? throw new Exception()));
                       
                }
                catch (Exception)
                {
                    image = null;
                }
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

        public PO.Product GetCopy()
        {
            return (Product)this.MemberwiseClone();
        }

        public static bool operator !=(Product p1, Product p2)
        {
            if (p1?.ID != p2.ID || p1.NameOfBook != p2.NameOfBook || p1.AuthorName != p2.AuthorName ||
                p1.Category != p2.Category || p1.InStock != p2.InStock || p1.productImagePath != p2.productImagePath ||
                p1.Price != p2.Price || p1._Summary != p2._Summary)
                return true;
            return false;
        }
        public static bool operator ==(Product p1, Product p2)
        {
            if (p1 != p2)
                return false;
            return true;
        }
    }
}
