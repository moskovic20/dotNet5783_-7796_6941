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
using PL.PlEntity.Products;
using PL.PlEntity.Order;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Window
    {
        private IBl bl;

        public adminMenu(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void productInMenu_Click(object sender, RoutedEventArgs e)
        {
            this.adminFrame.Content = new productList(bl);
        }

        private void ordersInMenu_Click(object sender, RoutedEventArgs e)
        {
            this.adminFrame.Content = new ordersList(bl);
        }
    }
}
