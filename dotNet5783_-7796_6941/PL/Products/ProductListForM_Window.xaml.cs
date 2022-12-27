using BlApi;
using PL.BoProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using PL.PO;

namespace PL.Products
{

    /// <summary>
    /// Interaction logic for ProductListForM_Window.xaml
    /// </summary>
    public partial class ProductListForM_Window : Window
    {
        private IBl bl;
       // private ObservableCollection<productForList> allBooks;

        public ProductListForM_Window(IBl bl)
        {
            InitializeComponent();
          
            this.bl = bl;
            //allBooks = new();
            //allBooks.ToObserCollection();
            //DataContext =allBooks;

            ProductListview.ItemsSource = bl.BoProduct.GetAllProductForList_forM();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));

        }

        private void categoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.CATEGORY? categ = cmbCategorySelector.SelectedItem as BO.CATEGORY?;

            if (categ == BO.CATEGORY.all)
                ProductListview.ItemsSource = bl.BoProduct.GetListedProducts();
            else
                ProductListview.ItemsSource = bl.BoProduct.GetListedProducts(BO.Filters.filterBYCategory, categ);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new AddProductForM_Window(bl).Show();
            //DataContext = allBooks;
        }

        private void UpdatButton_Click(object sender, RoutedEventArgs e)
        {
            //new UpdatProductWindow(bl).Show();
            new UpdatProductForM_Window(bl).Show();
        }
    }
}

