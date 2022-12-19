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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.BoProducts
{
    /// <summary>
    /// Interaction logic for ProductListWindow_forC.xaml
    /// </summary>
    public partial class ProductListWindow_forC : Window
    {
        private IBl bl;

        public ProductListWindow_forC(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;

            ProductListview.ItemsSource = bl.BoProduct.GetAllProductForList_forC();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));
        }

        //private void cmbCategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // לוודא שלא מגיעים ערכים ללא מחיר
        //    //BO.CATEGORY? categ = cmbCategorySelector.SelectedItem as BO.CATEGORY?;

        //    //if (categ == BO.CATEGORY.all)
        //    //    ProductListview.ItemsSource = bl.BoProduct.GetListedProducts();
        //    //else
        //    //    ProductListview.ItemsSource = bl.BoProduct.GetListedProducts(BO.Filters.filterBYCategory, categ);
        //}

    }
}
