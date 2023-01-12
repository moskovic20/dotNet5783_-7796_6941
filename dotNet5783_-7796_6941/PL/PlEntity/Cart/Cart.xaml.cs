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
using PL.Catalog;

namespace PL.PlEntity.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Page
{
    private IBl bl;
    private PO.Cart myCart;
    Frame myFrame;
    public Cart(IBl bl , PO.Cart cart,Frame frame)
    {
        InitializeComponent();
        this.bl = bl;
        this.myFrame = frame;
        this.myCart = cart;

        this.DataContext = myCart;
        this.OrderItems_DateGrid.DataContext = myCart.Items;
    }

    #region יצירת הזמנה 
    private void MakeOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        { 
            if (NameC.Text == "")
                throw new Exception("הכנס שם לקוח");
            if (MailC.Text == "")
                throw new Exception("הכנס כתובת מייל");
            if (AdressC.Text == "")
                throw new Exception("הכנס כתובת לשילוח");

            int OrderId=bl.BoCart.MakeOrder(myCart.CastingFromPoToBoCart());
            
            MessageBox.Show("מספר הזמנתך הוא "+ OrderId + "!ההזמנה נקלטה במערכת");

            myCart.reboot();
            myFrame.Content = new catalog(bl, myCart, this.myFrame);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

    }
    #endregion

    #region הגדלת כמות פריטים
    private void incresNum_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PO.OrderItem toChangeP = (PO.OrderItem)OrderItems_DateGrid.SelectedItem;
            bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, toChangeP.AmountOfItems + 1).putTo(myCart);
            this.DataContext = myCart;
            this.OrderItems_DateGrid.DataContext = myCart.Items;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
    #endregion

    #region הקטנת כמות פריטים
    private void reduceNum_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PO.OrderItem toChangeP = (PO.OrderItem)OrderItems_DateGrid.SelectedItem;
            bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, toChangeP.AmountOfItems -1).putTo(myCart);
            this.DataContext = myCart;
            this.OrderItems_DateGrid.DataContext = myCart.Items;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
    #endregion

    #region מחיקת ספר
    private void deleteOrderItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PO.OrderItem toChangeP = (PO.OrderItem)OrderItems_DateGrid.SelectedItem;
            bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, 0).putTo(myCart);
            this.DataContext = myCart;
            this.OrderItems_DateGrid.DataContext = myCart.Items;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
    #endregion

    private void CancleOrder_Click(object sender, RoutedEventArgs e)
    {
        myCart.Items = null;
        myCart.TotalPrice = null;
        this.OrderItems_DateGrid.DataContext = myCart.Items;
    }
}
