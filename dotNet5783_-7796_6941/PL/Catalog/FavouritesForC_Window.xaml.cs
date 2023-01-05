using BlApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for FavouritesForC_Window.xaml
    /// </summary>
    public partial class FavouritesForC_Window : Window
    {
        private IBl bl;
        private ObservableCollection<Product> Favourites;
        public FavouritesForC_Window(IBl bl, ObservableCollection<Product> favourites)
        {
            InitializeComponent();
            this.bl = bl;
            this.Favourites = favourites; 
        }
    }
}
