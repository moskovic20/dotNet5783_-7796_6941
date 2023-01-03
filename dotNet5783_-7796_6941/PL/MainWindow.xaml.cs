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
using PL.PO;
using System.Net.Mail;
using PL.PlEntity.Order;

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

        //this.DataContext = pForShow;//?
        this.TvBox.ItemsSource = allBooksForShow;

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
}
