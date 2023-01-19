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
using System.Net;

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


