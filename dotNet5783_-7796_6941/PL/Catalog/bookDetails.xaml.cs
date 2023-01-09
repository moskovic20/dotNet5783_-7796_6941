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

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for bookDetails.xaml
    /// </summary>
    public partial class bookDetails : Page
    {
        IBl bl;
        Product myProduct;

        public bookDetails(int ID,IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            myProduct = bl.BoProduct.GetProductDetails_forM(ID).copyProductToPo();
            DataContext = myProduct;
        }


    }
}
