using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace PL.PO
{

    public class Product : INotifyPropertyChanged
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
                    PropertyChanged(this, new PropertyChangedEventArgs("orderID"));

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
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("AuthorName"));

            }
        }

        private PO.CATEGORY? _Category;
        public PO.CATEGORY? Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(""));

            }
        }

        private string? _Summary;
        public string? Summary
        {
            get { return _Summary; }
            set
            {
                _Summary = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Summary"));

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
