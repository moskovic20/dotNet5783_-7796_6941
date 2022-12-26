using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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

        private PO.CATEGORY _Category;
        public PO.CATEGORY Category
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

        
        public double? Price { get; set; }


        public int? InStock { get; set; }

        private string? _Path;
        public string? Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path= value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Path"));

            }
        }

        public PO.Product GetCopy()
        {
            return (Product)this.MemberwiseClone();
        }

        public static bool operator !=(Product p1, Product p2)
        {
            if (p1.ID != p2.ID || p1.NameOfBook != p2.NameOfBook || p1.AuthorName != p2.AuthorName ||
                p1.Category != p2.Category || p1.InStock != p2.InStock || p1.Path != p2.Path ||
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
