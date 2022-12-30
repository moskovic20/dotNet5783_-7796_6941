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
using PL.Products;
using BO;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for OrderListForM_Window.xaml
/// </summary>
public partial class OrderListForM_Window : Window
{
    private IBl bl;
    private ObservableCollection<PO.OrderForList> allOrders;

    public OrderListForM_Window(IBl bl)
    {
        InitializeComponent();

        this.bl = bl;
        var orderForLists = from order in bl.BoOrder.GetAllOrderForList()
                            select new PO.OrderForList()
                            {
                                OrderID = order.OrderID,
                                CustomerName = order.CustomerName,
                                Status = (PO.OrderStatus)order.Status,
                                AmountOfItems = order.AmountOfItems,
                                TotalPrice = order.TotalPrice
                            };
        allOrders = new(orderForLists);
        DataContext = allOrders; //כדי שיקושר למסך נראה לי
    }

    private void AddOrder_Click(object sender, RoutedEventArgs e)
    {
 
        Action<int> AddAction = (orderId)=>/*allOrders.Add(bl.BoOrde)*/
        {
            PO.Order or = bl.BoOrder.GetOrdertDetails(orderId).CopyPropTo(new PO.Order());//צריך לבדוק אם הפונקציה עובדת
            // כדי להכניס לאובזרבר צריך המרה מטיפוס ליסט ללא ליסט
            //allOrders.Add(or);
        };
        new AddOrderWindow(bl, AddAction).ShowDialog();

    }

    #region אירוע- לחיצה על כפתור עדכון הזמנה
    private void UpdateOrder_Click(object sender, RoutedEventArgs e)
    {
        //Action<int> updateAction = (orderID) =>
        //{
        //    PO.Order o = bl.BoOrder.GetOrdertDetails(orderID).CopyBoOrderForListToPoOrder();
        //    PO.Order O_BeforUp = allOrders.FirstOrDefault(x => x.OrderID == o.ID)!.CopyBoOrderForListToPoOrder();
        //     //עדכנו שדה שדה,לבדוק אם יש דרך חכמה יותר
            
        //};

        //new UpdatProductForM_Window(bl, (ProductForList)Products_DateGrid.SelectedItem, updateAction).ShowDialog();

    }

    #endregion

    private void Products_DateGrid_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        new OrderDetailsWindow(bl, (sender as DataGrid).SelectedItem as PO.OrderForList).ShowDialog();

    }
}
