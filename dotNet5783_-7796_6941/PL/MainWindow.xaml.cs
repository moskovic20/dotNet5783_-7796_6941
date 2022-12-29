//using BlApi;
//using BlImplementation;
//using PL.BoProducts;
//using PL.Products;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace PL;

///// <summary>
///// Interaction logic for MainWindow.xaml
///// </summary>
//public partial class MainWindow : Window
//{
//    private IBl _bl =  BlApi.Factory.GetBl();

//    public MainWindow()
//    {
//        InitializeComponent();
//    }


//    private void _go_to_product_manager_Click(object sender, RoutedEventArgs e)
//    {
//        new ProductListForM_Window(_bl).Show();
//    }

//}

using BlApi;
using PL.Admin;
using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PL;

/// <summary>
/// Interaction logic for WindowForMain.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;
    //private ObservableCollection<Product> products;
    private Product pForShow;
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

        allBooksForShow = new(bl.BoProduct.GetAllProductForList_forC().Select(p => p.copyPflToPoProduct()));

        this.DataContext = pForShow;//?
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

    }

    private void Admin_Click(object sender, RoutedEventArgs e)
    {
        new AdminPassword(bl).ShowDialog();
    }
}
