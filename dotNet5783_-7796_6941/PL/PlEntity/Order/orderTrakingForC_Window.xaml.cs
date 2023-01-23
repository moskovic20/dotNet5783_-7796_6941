using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.XPath;
using BlApi;
using PL.PO;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for orderTrakingForC_Window.xaml
/// </summary>
public partial class orderTrakingForC_Window : Window
{
    private IBl bl;
    PO.Order myOrder = new();
    int id;

    public orderTrakingForC_Window(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;
        searchRec.Visibility = Visibility.Visible;
        search.Visibility = Visibility.Visible;
        //orderID.Visibility = Visibility.Visible;
        orderID.Text = null;
        //numO.Visibility = Visibility.Visible;
    }

    public orderTrakingForC_Window(IBl bl, int id)
    {
        InitializeComponent();
        this.bl = bl;
        this.id = id;
        searchRec.Visibility= Visibility.Hidden;
        search.Visibility = Visibility.Hidden;
        //orderID.Visibility= Visibility.Visible;
        orderID.Text = id.ToString();
        //numO.Visibility= Visibility.Visible;
        healpTraking(id);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bool isFine = int.TryParse(orderID.Text, out id);
            if (!isFine)
                throw new Exception("לא יכול להמיר מספר זה");

            healpTraking(id);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                   MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }


    }

    private void healpTraking(int id)
    {
        try
        {
            myOrder = bl.BoOrder.GetOrdertDetails(id).CopyBoOrderToPoOrder();
            DataContext = myOrder;

            orderDate_label.Visibility = Visibility.Visible;
            shippingDate_label.Visibility = Visibility.Visible;
            deliveryDate_label.Visibility = Visibility.Visible;

            orderDate.Visibility = Visibility.Visible;
            ShippingDate.Visibility = Visibility.Visible;
            DeliveryDate.Visibility = Visibility.Visible;

            orderDetails.Visibility = Visibility.Visible;

            circle1.Visibility = Visibility.Visible;

            if (myOrder.ShippingDate != null)
            {
                circle2.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle2.Stroke = System.Windows.Media.Brushes.Yellow;
                circle2.Visibility = Visibility.Visible;
            }
            else
            {
                circle2.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFC2C3C9")!;
                circle2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle2.Visibility = Visibility.Visible;
            }
            if (myOrder.DeliveryDate != null)
            {
                circle3.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle3.Stroke = System.Windows.Media.Brushes.Yellow;
                circle3.Visibility = Visibility.Visible;
            }
            else
            {
                circle3.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFC2C3C9")!;
                circle3.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle3.Visibility = Visibility.Visible;
            }

        }
        catch (BO.GetDetails_Exception)
        {
            errorOrderID.Visibility = Visibility.Visible;

            if (orderDate.Visibility == Visibility)
            {
                orderDetails.Visibility = Visibility.Hidden;

                orderDate_label.Visibility = Visibility.Hidden;
                shippingDate_label.Visibility = Visibility.Hidden;
                deliveryDate_label.Visibility = Visibility.Hidden;

                orderDate.Visibility = Visibility.Hidden;
                ShippingDate.Visibility = Visibility.Hidden;
                DeliveryDate.Visibility = Visibility.Hidden;

                circle1.Visibility = Visibility.Hidden;
                circle2.Visibility = Visibility.Hidden;
                circle3.Visibility = Visibility.Hidden;
            }

        }
    }

    private void orderID_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
     
    }

    private void orderID_GotFocus(object sender, RoutedEventArgs e)
    {
        errorOrderID.Visibility = Visibility.Hidden;
    }

    private void orderDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderDetailsWindowForM_(bl, id).Show();
    }
}
