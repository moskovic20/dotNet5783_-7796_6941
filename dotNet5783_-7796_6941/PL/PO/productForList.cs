using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL.PO
{
    public class ProductForList : INotifyPropertyChanged
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


        public PO.ProductForList GetCopy()
        {
            return (ProductForList)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, NameOfBook : {1}, Price: {2},Category:{3}",
              ID, NameOfBook, Price, Category.ToString());
        }
    }
}
