using BlApi;
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

namespace PL.PlEntity.Order;

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
        allOrders = allOrders!.ToObserCollection_O();
        DataContext = allOrders;
    }

        #region אירוע- לחיצה על כפתור מחיקת הזמנה
        private void CancleOrder_button(object sender, RoutedEventArgs e)
        {
            try
            {
                
                OrderForList orderToD = (OrderForList)Orders_DateGrid.SelectedItem;

                if (orderToD.Status == OrderStatus.Processing)
                    throw new Exception("אי אפשר לבטל הזמנה שנשלחה");
                if (orderToD.Status == OrderStatus.Completed)
                    throw new Exception("אי אפשר לבטל הזמנה שהושלמה");

                var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה לבטל הזמנה זו?", "ביטול הזמנה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (delete)
                {
                    case MessageBoxResult.Yes:
                        bl.BoOrder.CancleOrder_forM(orderToD.OrderID);
                        allOrders.Remove(orderToD);

                        MessageBox.Show("!ההזמנה בוטלה בהצלחה");
                        break;
                    case MessageBoxResult.No:
                        Orders_DateGrid.SelectedItem = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                   MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
    #endregion


    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderForList ordToUp = (PO.OrderForList)Orders_DateGrid.SelectedItem;
        try
        {
            var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה לעדכן שליחת הזמנה זו?", "עדכון סטטוס", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (delete)
            {
                case MessageBoxResult.Yes:
                    bl.BoOrder.UpdateOrderShipping(ordToUp.OrderID);
                    MessageBox.Show("!ההזמנה יצאה לדרך");
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
        ordToUp.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;
    }

    private void UpdateDelivary_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderForList ordToUp = (PO.OrderForList)Orders_DateGrid.SelectedItem;
        try
        {
            var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה לעדכן ביצוע הזמנה זו?", "עדכון סטטוס", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (delete)
            {
                case MessageBoxResult.Yes:
                    bl.BoOrder.UpdateOrderDelivery(ordToUp.OrderID);
                    MessageBox.Show("!ההזמנה בוצעה בהצלחה");
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
        ordToUp.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;
    }

    #region אירוע- לחיצה על כפתור עדכון הזמנה
    private void UpdateOrder_Click(object sender, RoutedEventArgs e)
    {
        //Action<int> updateAction = (orderID) =>
        //{
        //    PO.Order o = bl.BoOrder.GetOrdertDetails(orderID).CopyBoOrderForListToPoOrder();
        //    PO.Order O_BeforUp = allOrders.FirstOrDefault(x => x.OrderID == o.orderID)!.CopyBoOrderForListToPoOrder();
        //     //עדכנו שדה שדה,לבדוק אם יש דרך חכמה יותר

        //};

        //new UpdatProductForM_Window(bl, (ProductForList)Products_DateGrid.SelectedItem, updateAction).ShowDialog();

    }

    #endregion

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{
    //    // PO.OrderForList or = bl.BoOrder.GetOrderForList(orderId).CopyPropTo(new PO.OrderForList());//צריך לבדוק אם הפונקציה עובדת
    //    Action<int> statusAction = (orderId) =>
    //    {
    //        //PO.ProductForList p = bl.BoProduct.GetProductForList(ProductID).CopyBoPflToPoPfl();
    //        //  PO.ProductForList P_BeforUp = allBooks.FirstOrDefault(x => x.orderID == p.orderID)!;
    //        PO.OrderForList or = bl.BoOrder.GetOrderForList(orderId).CopyPropTo(new PO.OrderForList());
    //        PO.OrderForList orBeforUp = allOrders.FirstOrDefault(x => x.OrderID == or.OrderID)!;
    //        orBeforUp.Status = or.Status;
    //    };
    //    //צריך לבדוק אם הפונקציה עובדת
    //    //new statusUpdatWindow(bl, (PO.OrderForList)Orders_DateGrid.SelectedItem, statusAction).ShowDialog();
    //}

    private void moveOrderToArchives(object sender, RoutedEventArgs e)
    {
        try
        {

            OrderForList orderToD = (OrderForList)Orders_DateGrid.SelectedItem;
            if (orderToD.Status != OrderStatus.Completed)
                throw new Exception("אי אפשר להעביר לארכיון הזמנה שאינה הושלמה ");

            var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה להעביר הזמנה זו לארכיון?", "העברה לארכיון", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (delete)
            {
                case MessageBoxResult.Yes:
                    bl.BoOrder.DeleteOrder_forM(orderToD.OrderID);
                    allOrders.Remove(orderToD);

                    MessageBox.Show("!ההזמנה הועברה בהצלחה");
                    break;
                case MessageBoxResult.No:
                    Orders_DateGrid.SelectedItem = null;
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
               MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }

    private void Orders_DateGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO.OrderForList or= (PO.OrderForList)Orders_DateGrid.SelectedItem;
        new OrderDetailsWindowForM_(bl,or).Show();
    }
}



