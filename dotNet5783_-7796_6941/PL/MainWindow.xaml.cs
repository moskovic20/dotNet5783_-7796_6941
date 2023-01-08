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
using Microsoft.VisualBasic;

namespace PL;

/// <summary>
/// Interaction logic for WindowForMain.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;
    BO.Cart myCart = new();
    private ObservableCollection<Product> allBooksForShow;

    public MainWindow()
    {
        InitializeComponent();
        this.bl = BlApi.Factory.GetBl();

        allBooksForShow = new(bl.BoProduct.GetAllProductForList_forC().Select(p => p.CopyPflToPoProduct()));
        this.Catalog.ItemsSource = allBooksForShow;
    }

    // for this code image needs to be a project resource
    private BitmapImage LoadImage(string filename)
    {
        return new BitmapImage(new Uri(@"Image\" + filename, UriKind.RelativeOrAbsolute));//@"Image/"/"pack://application:,,,/Image/" + filename)*/
    }

    private void connectToSystem_Click(object sender, RoutedEventArgs e)
    {
        new adminPassword(bl).Show();
        this.Close();
    }

    private void goToOrderTracking_Click(object sender, RoutedEventArgs e)
    {
        new orderTrakingForC_Window(bl).Show();
    }

    private void addToCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Button button = (sender as Button)!;
            PO.Product p = (button.DataContext as PO.Product)!;
            bl.BoCart.AddProductToCart(myCart, p.ID);
            MessageBox.Show("!הספר נוסף בהצלחה לסל הקניות");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }
}



