using BlApi;
using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BO;
using System.Windows.Data;
using Microsoft.Win32;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for AddProductForM_Window.xaml
    /// </summary>
    public partial class AddProductForM_Window : Window
    {
        private IBl bl;
        PO.Product productToAdd;
        Action<int> action;


        public AddProductForM_Window(IBl bl, Action<int> action)
        {
            InitializeComponent();
            this.bl = bl;
            this.action = action;

            productToAdd = new PO.Product();
            this.DataContext = productToAdd;

            this.AddP_categ_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));
        }

        #region אירוע-לחיצה על כפתור הוסף
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (AddP_ID_textBox.Text == "0")
                    throw new Exception("...הכנס מספר מזהה ");
                if (AddP_name_textBox.Text == "")
                    throw new Exception("...הכנס את שם הספר");
                if (AddP_author_textBox.Text == "")
                    throw new Exception("...הכנס את שם הסופר");
                if (AddP_categ_commbbox.SelectedItem == null)
                    throw new Exception("הכנס את קטגוריית הספר");

                //if (!string.IsNullOrWhiteSpace(productToAdd.productImagePath))
                //{
                //    string[] s = productToAdd.productImagePath.Split("\\");
                //    File.Move(productToAdd.productImagePath,)
                //}
                int newID = bl.BoProduct.AddProduct_forM(productToAdd.CopyProductToBO());
                action(newID);
                MessageBox.Show("!הספר נוסף בהצלחה");

                productToAdd = new PO.Product();
                this.DataContext = productToAdd;
                AddP_categ_commbbox.SelectedItem = null;

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

        #region הפיכת שדה מספר מזהה לריק לאחר לחיצה עליו ובחזרה ביציאה

        private void AddP_ID_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "0")
                (sender as TextBox)!.Text = "";

        }


        private void AddP_ID_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "0";
            }

        }


        #endregion

        #region הגבלות על הקלט
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                productImage.Source = new BitmapImage(new Uri(op.FileName));
            }

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            ////openFileDialog.Filter
            ////openFileDialog.InitialDirectory
            //bool check = openFileDialog.ShowDialog() ?? false;
            //if (check)
            //}
            //    productToAdd.ProductImagePath = openFileDialog.FileName;
            //}

        }
    }
}
