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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using PL.PO;


namespace PL.PlEntity.Order
{
    /// <summary>
    /// Interaction logic for ordersList.xaml
    /// </summary>
    public partial class ordersList : Page
    {

        private IBl bl;
        private ObservableCollection<PO.OrderForList> allOrders = new();
        private ObservableCollection<OrderForList> orderSearch = new();

        public ordersList(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            allOrders = allOrders!.ToObserCollection_O();
            DataContext = allOrders;
        }

        #region אירוע- לחיצה על כפתור ביטול הזמנה
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
                        OrderForList temp = allOrders.First(x => x.OrderID == orderToD.OrderID);
                        allOrders.Remove(temp);
                        if (DataContext == orderSearch)
                        {
                            OrderForList ofl = orderSearch.First(x => x.OrderID == orderToD.OrderID);
                            orderSearch.Remove(ofl);
                        }
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

        #region עדכון שילוח והגעה של ההזמנה
        private void UpdateShip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.OrderForList ordToUp = (OrderForList)Orders_DateGrid.SelectedItem;
                if (ordToUp.Status != OrderStatus.Accepted)
                    throw new Exception("כבר הוזן תאריך שילוח להזמנה זו");

                var update = MessageBox.Show("האם אתה בטוח שאתה רוצה לעדכן שליחת הזמנה זו?", "עדכון סטטוס", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (update)
                {
                    case MessageBoxResult.Yes:
                        bl.BoOrder.UpdateOrderShipping(ordToUp.OrderID);
                        MessageBox.Show("!ההזמנה יצאה לדרך");
                        break;
                    case MessageBoxResult.No:
                        break;
                }

                ordToUp.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;

                if (DataContext == orderSearch)
                {
                    OrderForList myO = allOrders.First(x => x.OrderID == ordToUp.OrderID);
                    myO.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }

        }

        private void UpdateDelivary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.OrderForList ordToUp = (PO.OrderForList)Orders_DateGrid.SelectedItem;

                if (ordToUp.Status == OrderStatus.Accepted)
                    throw new Exception("ההזמנה עוד לא יצאה לדרך ולכן לא ניתן לעדכן הגעה");

                if (ordToUp.Status == OrderStatus.Completed)
                    throw new Exception("כבר הוזן תאריך הגעה של ההזמנה");

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

                ordToUp.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;
                if (DataContext == orderSearch)
                {
                    OrderForList myO = allOrders.First(x => x.OrderID == ordToUp.OrderID);
                    myO.Status = (PO.OrderStatus)bl.BoOrder.GetOrdertDetails(ordToUp.OrderID).Status;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
        #endregion

        #region מחיקת הזמנה שהושלמה מהמערכת
        private void moveOrderToArchives(object sender, RoutedEventArgs e)
        {
            try
            {

                OrderForList orderToD =(OrderForList)((DataGrid)sender).SelectedItem;
                if (orderToD.Status != OrderStatus.Completed)
                    throw new Exception("אי אפשר להעביר לארכיון הזמנה שאינה הושלמה ");

                var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה להעביר הזמנה זו לארכיון?", "העברה לארכיון", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (delete)
                {
                    case MessageBoxResult.Yes:
                        bl.BoOrder.DeleteOrder_forM(orderToD.OrderID);
                        OrderForList temp = allOrders.First(x => x.OrderID == orderToD.OrderID);
                        allOrders.Remove(temp);
                        if (DataContext == orderSearch)
                        {
                            OrderForList ofl = orderSearch.First(x => x.OrderID == orderToD.OrderID);
                            orderSearch.Remove(ofl);
                        }
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
        #endregion

        #region לחיצה על הזמנה לצפייה בפרטי הזמנה
        private void Orders_DateGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.OrderForList or =(OrderForList)((DataGrid)sender).SelectedItem;
            new OrderDetailsWindowForM_(bl, or.OrderID).Show();
        }
        #endregion

        #region קבץ לפי סטטוס
        private void GroupByStatus_Click(object sender, RoutedEventArgs e)
        {


            if ((string)GroupByStatus.Content == "קבץ לפי סטטוס")
            {
                GroupByStatus.Content = "בטל קיבוץ";
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_DateGrid.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
                view.GroupDescriptions.Add(groupDescription);
            }
            else
            {
                GroupByStatus.Content = "קבץ לפי סטטוס";
                remoteGroup();
            }



        }

        private void remoteGroup()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(allOrders);
            view.GroupDescriptions.Clear();
        }
        #endregion

        #region חיפוש הזמנה לפי מספר הזמנה\שם לקוח
        private void nameClaientOrID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                string myString = nameClaientOrID.Text;


                if (myString == "" || myString == null)
                {
                    DataContext = allOrders;
                    GroupByStatus.IsEnabled = true;
                }
                else
                {
                    OrderForList myOrder = new();

                    int id;

                    bool isID = int.TryParse(myString, out id);
                    if (isID)
                    {
                        var list = from o in bl.BoOrder.GetAllOrdersByNumber(id)
                                   select new OrderForList
                                   {
                                       CustomerName = o.CustomerName,
                                       OrderID = o.OrderID,
                                       AmountOfItems = o.AmountOfItems,
                                       Status = (PO.OrderStatus)o.Status,
                                       TotalPrice = o.TotalPrice,
                                   };
                        remoteGroup();
                        orderSearch.Clear();
                        orderSearch = new(list);
                    }
                    else
                    {
                        var list = from o in bl.BoOrder.getAllOrderOfClaient(myString)
                                   select new OrderForList
                                   {
                                       CustomerName = o.CustomerName,
                                       OrderID = o.OrderID,
                                       AmountOfItems = o.AmountOfItems,
                                       Status = (PO.OrderStatus)o.Status,
                                       TotalPrice = o.TotalPrice,
                                   };
                        remoteGroup();
                        orderSearch.Clear();
                        orderSearch = new(list);

                    }

                    DataContext = orderSearch;
                    GroupByStatus.Content = "קבץ לפי קטגוריה";
                    GroupByStatus.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                  MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }

        }
        #endregion

       
    }
}
