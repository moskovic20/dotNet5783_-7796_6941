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
        //IBl bl;
     //    myProduct;

        public bookDetails(int ID,IBl bl)
        {
            InitializeComponent();

          //  myProduct = bl.BoProduct.GetProductDetails_forC()
        }
    }
}
