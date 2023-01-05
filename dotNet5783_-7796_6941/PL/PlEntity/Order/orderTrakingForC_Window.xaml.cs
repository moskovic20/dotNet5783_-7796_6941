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
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bool isFine = int.TryParse(orderID.Text, out id);
            if (!isFine)
                throw new Exception("לא יכול להמיר מספר זה");

            myOrder = bl.BoOrder.GetOrdertDetails(id).CopyBoOrderToPoOrder();
            DataContext = myOrder;

            orderDate_label.Visibility = Visibility;
            shippingDate_label.Visibility = Visibility;
            deliveryDate_label.Visibility = Visibility;

            orderDate.Visibility = Visibility;
            ShippingDate.Visibility = Visibility;
            DeliveryDate.Visibility = Visibility;

            orderDetails.Visibility = Visibility;

            circle1.Visibility = Visibility;

            if (myOrder.ShippingDate != null)
            {
                circle2.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle2.Stroke = System.Windows.Media.Brushes.Yellow;
                circle2.Visibility = Visibility;
            }
            else
            {
                circle2.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFC2C3C9")!;
                circle2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle2.Visibility = Visibility;
            }
            if (myOrder.DeliveryDate != null)
            {
                circle3.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle3.Stroke = System.Windows.Media.Brushes.Yellow;
                circle3.Visibility = Visibility;
            }
            else
            {
                circle3.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFC2C3C9")!;
                circle3.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF009BA6")!;
                circle3.Visibility = Visibility;
            }

        }
        catch (BO.GetDetails_Exception )
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                   MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
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
