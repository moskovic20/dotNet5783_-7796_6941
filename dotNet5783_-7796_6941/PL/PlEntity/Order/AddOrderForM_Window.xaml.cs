using BO;
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

namespace PL.PlEntity.Order
{
    /// <summary>
    /// Interaction logic for AddOrderForM_Window.xaml
    /// </summary>
    public partial class AddOrderForM_Window : Window
    {
        private Cart adminCart = new();

        public AddOrderForM_Window()
        {
            InitializeComponent();
            DataContext = adminCart;
        }

        private void AddProductsToO(object sender, RoutedEventArgs e)
        {
            new AddProductToNewO_forM().ShowDialog();
        }
    }
}
