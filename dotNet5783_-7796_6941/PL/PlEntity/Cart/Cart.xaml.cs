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

namespace PL.PlEntity.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Page
{
    private IBl bl;
    private PO.Cart myCart;
    public Cart(IBl bl , PO.Cart newCart)
    {
        InitializeComponent();
        this.bl = bl;
        this.myCart = newCart;
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

            myCart = new();
            this.DataContext = myCart;//מקווה שככה אמורים
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
            //OrderForList orderToD = (OrderForList)Orders_DateGrid.SelectedItem;
            //PO.OrderItem toChangeP = (PO.OrderItem)sender;
            PO.OrderItem toChangeP = (PO.OrderItem)OrderItems_DateGrid.SelectedItem;
            myCart = bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, toChangeP.AmountOfItems + 1).CastingFromBoToPoCart();
            this.DataContext = myCart;//צריך?
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
            //PO.OrderItem toChangeP = (PO.OrderItem)sender;
            PO.OrderItem toChangeP = (PO.OrderItem)OrderItems_DateGrid.SelectedItem;
            myCart =bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, toChangeP.AmountOfItems - 1).CastingFromBoToPoCart();
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
            PO.OrderItem toChangeP = (PO.OrderItem)sender;
            bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), toChangeP.ProductID, 0);
            this.DataContext = myCart;//צריך?
            this.OrderItems_DateGrid.DataContext = myCart.Items;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
    #endregion
}
