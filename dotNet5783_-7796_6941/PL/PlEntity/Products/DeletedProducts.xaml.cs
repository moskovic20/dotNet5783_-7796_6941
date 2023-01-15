using BlApi;
using PL.PO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.PlEntity.Products
{
    /// <summary>
    /// Interaction logic for DeletedProducts.xaml
    /// </summary>
    public partial class DeletedProducts : Page
    {
        IBl bl;
        public DeletedProducts(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            DeletedProducts_DateGrid.ItemsSource = bl.BoProduct.GetAllDeletedProducts().Select(x => x.copyProductToPo());
        }
    }
}
