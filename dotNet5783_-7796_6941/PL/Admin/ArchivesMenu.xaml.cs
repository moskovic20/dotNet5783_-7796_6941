using BlApi;
using PL.PlEntity.Order;
using PL.PlEntity.Products;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for ArchivesMenu.xaml
    /// </summary>
    public partial class ArchivesMenu : Page
    {
        IBl bl;
       

        public ArchivesMenu(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
           
        }

        private void orderItemsArchives_Click(object sender, RoutedEventArgs e)
        {
            archivesMenu.Content = new DeletedOrderItems(bl);
        }

        private void productsArchives_Click(object sender, RoutedEventArgs e)
        {
            archivesMenu.Content = new DeletedProducts(bl);
        }

        private void ordersArchives_Click(object sender, RoutedEventArgs e)
        {
            archivesMenu.Content = new DeletedOrders(bl);
        }
    }
}
