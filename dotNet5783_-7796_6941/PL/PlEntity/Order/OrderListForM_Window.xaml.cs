using BlApi;
//using BO;
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
using PL.PO;
using System.Collections.Specialized;

namespace PL.PlEntity.Order
{
    /// <summary>
    /// Interaction logic for OrderListForM_Window.xaml
    /// </summary>
    public partial class OrderListForM_Window : Window
    {
        private IBl bl;
        private ObservableCollection<PO.OrderForList> allOrders = new();

        public OrderListForM_Window(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            allOrders=allOrders!.ToObserCollection_O();
            DataContext = allOrders;
        }

        #region אירוע- לחיצה על כפתור מחיקת הזמנה
        private void DeleteOrder_button(object sender, RoutedEventArgs e)
        {
            try
            {

                OrderForList orderToD = (OrderForList)Orders_DateGrid.SelectedItem;
                if (orderToD.Status == OrderStatus.Accepted)
                {
                    PL.PO.Order myOrder = bl.BoOrder.GetOrdertDetails(orderToD.OrderID).CopyBoOrderToPoOrder();
                    myOrder.Items = myOrder.Items ?? new();

                }




                var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה למחוק הזמנה זו?", "מחיקת הזמנה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (delete)
                {
                    case MessageBoxResult.Yes:

                        if(orderToD.Status == OrderStatus.Processing)
                        {
                            bl.BoOrder.DeleteOrder_forM(orderToD.OrderID);
                            allOrders.Remove(orderToD);
                        }
                        else
                        {

                        }
                        MessageBox.Show("!ההזמנה נמחקה בהצלחה");
                        break;
                    case MessageBoxResult.No:
                        Orders_DateGrid.SelectedItem = null;
                        break;
                }


                bl.BoOrder.DeleteOrder_forM(orderToD.OrderID);

            }
            catch
            {

            }
        }
        #endregion
    }
}
