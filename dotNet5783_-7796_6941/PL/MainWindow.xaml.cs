//using BlApi;
//using BlImplementation;
//using PL.BoProducts;
//using PL.Products;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BlApi;
using PL.Admin;
using PL.PO;
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

namespace PL;

/// <summary>
/// Interaction logic for WindowForMain.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;

    private ObservableCollection<ProductItem> allBooksForShow;
    private PL.PO.Cart myCart;

    public MainWindow()
    {
        InitializeComponent();
        this.bl = BlApi.Factory.GetBl();

        //allBooksForShow = new(bl.BoProduct.GetAllProductForList_forC().Select(p => p.CopyPflToPoProduct()));
        allBooksForShow = new(bl.BoProduct.GetAllProductItems_forC().Select(p => p.CopyProductItemFromBoToPo()));
        myCart = new PL.PO.Cart();
        this.Catalog.ItemsSource = allBooksForShow;
        // Catalog.FontStyle = Heebo;
        
        this.DataContext = Catalog;
    }

    #region טעינת תמונות ---------יש מה לעבוד עוד-----------
    // for this code image needs to be a project resource
    private BitmapImage LoadImage(string filename)
    {
        return new BitmapImage(new Uri(@"Image\" + filename, UriKind.RelativeOrAbsolute));/*/*@"Image/"/"pack://application:,,,/Image/" + filename)*/
    }
    #endregion

    #region כפתור צור קשר
    private void conectUs_Click(object sender, RoutedEventArgs e)
    {

    }
    #endregion

    private void connectToSystem_Click(object sender, RoutedEventArgs e)
    {
        new adminPassword(bl).Show();
        this.Close();
    }
    #endregion

    #region כפתור מסע משלוח
    private void trakingOrder_Click(object sender, RoutedEventArgs e)
    {
        new orderTrakingForC_Window(bl).Show();
    }
    #endregion

    private void addToCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Button button = (sender as Button)!;
            PO.Product p = (button.DataContext as PO.Product)!;
            bl.BoCart.AddProductToCart(myCart.CastingFromPoToBoCart(), pID).CastingFromBoToPoCart(); //הוספת המוצר לשכבה מתחת 
            MessageBox.Show("!הספר נוסף בהצלחה לסל הקניות");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
}

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
