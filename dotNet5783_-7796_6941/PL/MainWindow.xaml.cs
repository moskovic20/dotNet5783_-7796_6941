using BlApi;
using BlImplementation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl _bl =  BlApi.Factory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _go_to_product_list__Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(_bl).Show();
        }

        private void _go_to_product_admin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
