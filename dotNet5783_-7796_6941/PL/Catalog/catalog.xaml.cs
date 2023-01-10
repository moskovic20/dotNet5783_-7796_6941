using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//using BO;

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for catalog.xaml
    /// </summary>
    public partial class catalog : Page
    {
        IBl bl;
        Frame myFrame;
        Cart myCart;
        private ObservableCollection<ProductItem>? allBooksForShow;

        public catalog(IBl bl, Cart cart, Frame frame)
        {
            InitializeComponent();
            this.bl = bl;
            myFrame = frame;
            myCart = cart;

            allBooksForShow = new(bl.BoProduct.GetAllProductItems_forC().Select(p => p.CopyProductItemFromBoToPo()));
            DataContext = allBooksForShow;

        }

        private void Catalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ProductItem p = (ProductItem)Catalog.SelectedItem;
            this.myFrame.Content = new bookDetails(p.ID, bl,myCart);

        }

        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (sender as Button)!;
                PO.ProductItem p = (button.DataContext as PO.ProductItem)!;
                myCart=bl.BoCart.AddProductToCart(myCart.CastingFromPoToBoCart(), p.ID).CastingFromBoToPoCart(); //הוספת המוצר לשכבה מתחת 
                MessageBox.Show("!הספר נוסף בהצלחה לסל הקניות");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
    }
}
