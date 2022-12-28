using BlApi;
using PL.Order;
using PL.Products;
using System.Windows;

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

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            new OrderForLstWindow(bl).Show();
        }
    }
}
