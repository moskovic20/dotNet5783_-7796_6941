using BlApi;
using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Products
{

    /// <summary>
    /// Interaction logic for ProductListForM_Window.xaml
    /// </summary>
    public partial class ProductListForM_Window : Window
    {
        private IBl bl;
        private ObservableCollection<ProductForList> allBooks;

        public ProductListForM_Window(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            allBooks = new();
            allBooks = allBooks.ToObserCollection();
            DataContext = allBooks;

            // ProductListview.ItemsSource = bl.BoProduct.GetAllProductForList_forM();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));

        }


        private void categoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.CATEGORY? categ = cmbCategorySelector.SelectedItem as BO.CATEGORY?;

            if (categ == BO.CATEGORY.all)
                Products_DateGrid.ItemsSource = bl.BoProduct.GetListedProducts();
            else
                Products_DateGrid.ItemsSource = bl.BoProduct.GetListedProducts(BO.Filters.filterBYCategory, categ);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new AddProductForM_Window(bl, allBooks).ShowDialog();
        }

        private void UpdatButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdatProductForM_Window(bl, (ProductForList)Products_DateGrid.SelectedItem).ShowDialog();
            UpdatButton.IsEnabled = false;
            Products_DateGrid.SelectedItem=null;
        }

        private void Products_DateGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatButton.IsEnabled = true;
        }
    }
}

