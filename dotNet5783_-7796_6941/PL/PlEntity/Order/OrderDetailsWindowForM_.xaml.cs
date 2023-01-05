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

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for OrderDetailsWindowForM_.xaml
/// </summary>
public partial class OrderDetailsWindowForM_ : Window
{
    IBl bl;
    PO.Order orderToShow;
    
    public OrderDetailsWindowForM_(IBl bl, OrderForList order)
    {
        InitializeComponent();
        this.bl = bl;
        this.orderToShow = bl.BoOrder.GetOrdertDetails(order.OrderID).CopyBoOrderToPoOrder();
        this.DataContext = orderToShow;
        this.OrderItems_DateGrid.DataContext = orderToShow.Items;


    }

}
