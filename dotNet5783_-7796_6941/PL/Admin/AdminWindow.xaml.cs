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
using PL.PO;
using PL.Products;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        IBl bl;
        public AdminWindow(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void products_Click(object sender, RoutedEventArgs e)
        {
            new ProductListForM_Window(bl).ShowDialog();
        }
    }
}
