using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.BoProducts
{
    /// <summary>
    /// Interaction logic for ProductListWindow_forM.xaml
    /// </summary>
    public partial class ProductListWindow_forM : Window
    {
        private IBl bl;
        
        public ProductListWindow_forM(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            //addButton.AddHandler();

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
            new AddProduct_forM_Window(bl).Show();
        }

        private void UpdatButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdatProductWindow(bl).Show();
        }
    }
}
