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


namespace PL;

/// <summary>
/// Interaction logic for WindowForMain.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;
    //private ObservableCollection<Product> products;
    //private Product pForShow;
    private ObservableCollection<Product> allBooksForShow;

    private ObservableCollection<Product> lovedBooks;

    private PL.PO.Cart myCart;
    public MainWindow()
    {
        InitializeComponent();
        this.bl = BlApi.Factory.GetBl();

        //this.cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));
        //this.TvBox.ItemsSource = new BookData[]
        //{
        //    new BookData{Title="Movie 1", ImageData=LoadImage("booksForBeakRound.jpg.")},
        //    new BookData{Title="Movie 2", ImageData=LoadImage("booksForBeakRound.jpg")},
        //    new BookData{Title="Movie 3", ImageData=LoadImage("booksForBeakRound.jpg")},
        //    new BookData{Title="Movie 4", ImageData=LoadImage("booksForBeakRound.jpg")},
        //    new BookData{Title="Movie 5", ImageData=LoadImage("booksForBeakRound.jpg")},
        //    new BookData{Title="Movie 6", ImageData=LoadImage("booksForBeakRound.jpg")}
        //};

        allBooksForShow = new(bl.BoProduct.GetAllProductForList_forC().Select(p => p.CopyPflToPoProduct()));
        myCart = new PL.PO.Cart(
                    //CustomerName = null,
                    //CustomerEmail = null,
                    //CustomerAddress = null,
                    //Items = new List<PO.OrderItem>(),
                    //TotalPrice = null
            );
        //this.DataContext = pForShow;//?
        this.Catalog.ItemsSource = allBooksForShow;
        // Catalog.FontStyle = Heebo;
        lovedBooks = new();
    }

    // for this code image needs to be a project resource
    private BitmapImage LoadImage(string filename)
    {
        return new BitmapImage(new Uri(@"Image\" + filename, UriKind.RelativeOrAbsolute));/*/*@"Image/"/"pack://application:,,,/Image/" + filename)*/
    }

    private void conectUs_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Cart_Click(object sender, RoutedEventArgs e)
    {

    }

    private void trakingOrder_Click(object sender, RoutedEventArgs e)
    {
        new orderTrakingForC_Window(bl).Show();
    }

    private void Admin_Click(object sender, RoutedEventArgs e)
    {
        new AdminPassword(bl).ShowDialog();
    }

    private void addToCard_Click(object sender, RoutedEventArgs e)
    {
        PO.Product p= (PO.Product)Catalog.SelectedItem;
        //.ProductItem pI= (BO.ProductItem)Catalog.SelectedItem; //המסך יודע להמיר משו שלא דיפנדנסי?
        ToCart(p.ID);

    }
    private void ToCart(int pID)
    {
        try
        {
            // bl.BoCart.AddProductToCart(myCart.CastingFromPoToBoCart(), p.ID);//הוספת המוצר לשכבה מתחת
            myCart = bl.BoCart.AddProductToCart(myCart.CastingFromPoToBoCart(), pID).CastingFromBoToPoCart();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }

    /// <summary>
    /// שמירת ספרים כספרים אהובים
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addToLoved_Click(object sender, RoutedEventArgs e)
    {
        PO.Product p = (PO.Product)Catalog.SelectedItem;
        lovedBooks.Add(p);
    }

    private void seePrefered_Click(object sender, RoutedEventArgs e) //window for marked as loved books, with optian to add to cart
    {
        Action<int> CartAction = productId => ToCart(productId); 
        new FavouritesForC_Window(bl, lovedBooks, CartAction).ShowDialog();
      
    }
}
