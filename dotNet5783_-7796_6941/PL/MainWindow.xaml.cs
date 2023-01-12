using BlApi;
using PL.Admin;
using PL.PO;
using PL.PlEntity.Cart;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Net.Mail;
using PL.PlEntity.Order;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using PL.Catalog;
using MaterialDesignThemes.Wpf;
using System.Windows.Documents;
using System.Collections;
using Microsoft.VisualBasic;
using PL.PlEntity.Products;

namespace PL;

/// <summary>
/// Interaction logic for WindowForMain.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;

    //private ObservableCollection<ProductItem>? BooksForShow;
    private PL.PO.Cart myCart;

    public MainWindow()
    {
        InitializeComponent();
        bl = BlApi.Factory.GetBl();
        myCart = new();
        myFrame.Content = new catalog(bl, myCart, this.myFrame);

    }

    #region טעינת תמונות | יש מה לעבוד עוד
    // for this code image needs to be a project resource
    private BitmapImage LoadImage(string filename)
    {
        return new BitmapImage(new Uri(@"Image\" + filename, UriKind.RelativeOrAbsolute));/*/*@"Image/"/"pack://application:,,,/Image/" + filename)*/
    }
    #endregion

    #region כפתור צור קשר | חסר לנו פונקציית מייל 
    private void conectUs_Click(object sender, RoutedEventArgs e)
    {

    }
    #endregion

    #region התחברות מנהל למערכת
    private void connectToSystem_Click(object sender, RoutedEventArgs e)
    {
        this.myFrame.Content = new Admin_Password(bl);
       // this.Close();
    }
    #endregion

    #region כפתור מסע משלוח
    private void trakingOrder_Click(object sender, RoutedEventArgs e)
    {
        new orderTrakingForC_Window(bl).Show();
    }
    #endregion

    #region עמוד ההזמנה
    private void GoToCart_Click(object sender, RoutedEventArgs e)
    {
        this.myFrame.Content = new PlEntity.Cart.Cart(bl,myCart,myFrame);
    }
    #endregion

    #region כפתור-מעבר לקטלוג
    private void goToCatalog_Click(object sender, RoutedEventArgs e)
    {
        this.myFrame.Content = new catalog(bl, myCart, this.myFrame);
    }
    #endregion
}

#region אופציה לספרים אהובים..
/*
 אם נוסיף אופציה לאהובים..
private ObservableCollection<Product> lovedBooks;
lovedBooks = new();

 /// <summary>
    /// שמירת ספרים כספרים אהובים
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
  private void addToLoved_Click(object sender, RoutedEventArgs e)
    {
        PO.ProductItem p = (PO.ProductItem)Catalog.SelectedItem;
        lovedBooks.Add(p);
    }

    private void seePrefered_Click(object sender, RoutedEventArgs e) //window for marked as loved books, with optian to add to cart
    {
        Action<int> CartAction = productId => ToCart(productId); 
        new FavouritesForC_Window(bl, lovedBooks, CartAction).ShowDialog();
      
    }

 */
#endregion

