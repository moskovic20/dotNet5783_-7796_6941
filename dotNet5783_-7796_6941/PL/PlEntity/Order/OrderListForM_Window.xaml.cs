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
            OrderForList orderToD = (OrderForList)Orders_DateGrid.SelectedItem;
            try
            {
                var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה למחוק הזמנה זו?", "מחיקת הזמנה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (delete)
                {
                    case MessageBoxResult.Yes:
                        bl.BoOrder.DeleteOrder_forM(orderToD.OrderID);
                        allOrders.Remove(orderToD);
                        MessageBox.Show("!הספר נמחק בהצלחה");
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

        private void AddTheStation_Click(object sender, RoutedEventArgs e)
        {
            new AddOrderForM_Window().ShowDialog();
        }
    }
}
