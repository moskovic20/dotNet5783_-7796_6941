using BlApi;
using PL.PO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Linq;
using System.IO;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for UpdatProductForM_Window.xaml
    /// </summary>
    public partial class UpdatProductForM_Window : Window
    {
        private IBl bl;
        private PO.Product productToUpdate;
        private PO.Product? beforUpdate;
        Action<int> action;


        public UpdatProductForM_Window(IBl bl, ProductForList pToUp, Action<int> action)
        {
            InitializeComponent();
            this.bl = bl;
            this.action=action;

            productToUpdate = new();
            productToUpdate = bl.BoProduct.GetProductDetails_forM(pToUp.ID).copyProductToPo();
            DataContext = productToUpdate;
            beforUpdate = productToUpdate.GetCopy();

            this.updateCateg_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));
        }


        #region אירוע-לחיצה על כפתור עדכן
        private void updateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productToUpdate == beforUpdate!)
                    throw new Exception("לא ביצעת עדכון עבור אף שדה...");

                if (updateName_textBox.Text == "")
                    throw new Exception("...הכנס את שם הספר");
                if (updateAuthor_TextBox.Text == "")
                    throw new Exception("...הכנס את שם הסופר");
                if (updateCateg_commbbox.SelectedItem == null)
                    throw new Exception("הכנס את קטגוריית הספר");

                if(PO.Tools.IsImageNeedCare(beforUpdate!,productToUpdate))
                {

                    productImage.Source = null;
                    string sorce = beforUpdate?.productImagePath!;

                    string suffix = sorce.Split(@".").Last();
                    string target = Environment.CurrentDirectory + "\\images\\productImages\\" + productToUpdate.Category + "\\" + productToUpdate.NameOfBook + "." + suffix;
                   // File.Move(sorce, target);
                    productToUpdate.productImagePath = target;
                    productImage.Source = productToUpdate.Image;
                }

                bl.BoProduct.UpdateProductDetails_forM(productToUpdate.CopyProductToBO());
                action(productToUpdate.ID);
                MessageBox.Show("!הספר עודכן בהצלחה");

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            

        }
        #endregion
       
        #region אירוע-לחיצה על כפתור ביטול
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        private void updateImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                productToUpdate.ProductImagePath = op.FileName;
                productImage.Source = productToUpdate.Image;
            }
        }

        private void PreviewTextInputToInt(object sender, TextCompositionEventArgs e)
        {
            e.limitInputToInt();
        }

        private void PreviewTextInputToDouble(object sender, TextCompositionEventArgs e)
        {
            e.limitInputToDouble();
        }
    }
}
