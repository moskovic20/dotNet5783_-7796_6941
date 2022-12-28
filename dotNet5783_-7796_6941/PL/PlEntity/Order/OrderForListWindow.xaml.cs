using BlApi;
using PL.PO;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderForLstWindow.xaml
    /// </summary>
    public partial class OrderForLstWindow : Window
    {
        private IBl bl;
        private ObservableCollection<OrderForList> allOrders;
        public OrderForLstWindow(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;

        }



    }
}
