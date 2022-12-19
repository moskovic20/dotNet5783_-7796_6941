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
    /// Interaction logic for AddProduct_forM_Window.xaml
    /// </summary>
    public partial class AddProduct_forM_Window : Window
    {
        private IBl bl;

        public AddProduct_forM_Window(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ui3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
