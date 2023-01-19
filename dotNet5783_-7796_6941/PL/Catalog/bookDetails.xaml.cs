using BlApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for bookDetails.xaml
    /// </summary>
    public partial class bookDetails : Page
    {
        IBl bl;
        Product myProduct;
        Cart myCart;

        public bookDetails(int ID,IBl bl,Cart Cart)
        {
            InitializeComponent();

            this.bl = bl;
            myCart = Cart;
            myProduct = bl.BoProduct.GetProductDetails_forM(ID).copyProductToPo();
            DataContext = myProduct;
            if (myProduct.InStock < 1)
            {
                gradeNumUpDown.Visibility = Visibility.Collapsed;
                addToCart.Visibility = Visibility.Collapsed;
                noInStock.Visibility = Visibility.Visible;
            }

            if (myProduct.productImagePath != null)
            {
                string path = myProduct.productImagePath!;
                string fullPath = System.IO.Path.Combine(Environment.CurrentDirectory, path);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new System.Uri(fullPath ?? throw new Exception("problem"));
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                productImage.Source = image;
            }//טיפול בתמונה
        }

        #region הוספת המוצר לסל הקניות
        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), myProduct.ID, (int?)gradeNumUpDown.Value?? throw new Exception()).putTo(myCart);
                MessageBox.Show("!הספר נוסף בהצלחה לסל הקניות");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
        #endregion


    }
}
