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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.PO;

namespace PL.PlEntity.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Page
{
    private IBl bl;
    private PO.Cart myCart;
    public Cart(IBl bl , PO.Cart newCart)
    {
        InitializeComponent();
        this.bl = bl;
        this.myCart = newCart;
        this.DataContext = myCart;
        this.OrderItems_DateGrid.DataContext = myCart.Items;
    }
}
