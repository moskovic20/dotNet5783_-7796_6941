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
using System.Windows.Shapes;

namespace PL.PlEntity.Cart;


/// <summary>
/// Interaction logic for CartForC_Window.xaml
/// </summary>
public partial class CartForC_Window : Window
{
    private IBl bl;
    private PO.Cart myCart;
    public CartForC_Window(IBl bl, PO.Cart newCart)
    {
        InitializeComponent();
        this.bl = bl;
        this.myCart = newCart;
        this.DataContext = myCart;
        this.OrderItems_DateGrid.DataContext = myCart.Items;
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}


