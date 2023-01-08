using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL.PO
{
    public class ProductForList : MainPo
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

        private int? _InStock;
        public int? InStock
        {
            get => _InStock;
            set
            {
                _InStock = value;
                OnPropertyChanged("InStock");
            }
        }


        public PO.ProductForList GetCopy()
        {
            return (ProductForList)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("OrderID: {0}, NameOfBook : {1}, Price: {2},Category:{3}",
              ID, NameOfBook, Price, Category.ToString());
        }
    }
}
