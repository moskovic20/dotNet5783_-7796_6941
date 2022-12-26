using BlApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for AddProductForM_Window.xaml
    /// </summary>
    public partial class AddProductForM_Window : Window
    {
        private IBl bl;
        PO.Product product;

        public AddProductForM_Window(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;

            product = new PO.Product();
            this.DataContext = product;

            this.AddP_categ_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));
        }

        #region אירוע-לחיצה על כפתור הוסף
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? selectedcmb = "";
                var comboBoxItem = AddP_categ_commbbox.SelectedItem;
                if (comboBoxItem != null)
                {
                    selectedcmb = comboBoxItem.ToString();
                }

                bl.BoProduct.AddProduct_forM(product.CopyProductToBO());
                MessageBox.Show("!הספר נוסף בהצלחה");

                product = new PO.Product();
                this.DataContext = product;
                AddP_categ_commbbox.SelectedItem = null;

            }
            catch (BO.Adding_Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch
            {
                MessageBox.Show("תקלה בהוספת הקו", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
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

        //private void AddP_name_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...שם הספר";
        //    }
        //}

        //private void AddP_author_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...שם הסופר";
        //    }

        //}

        //private void AddP_price_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...מחיר";
        //    }
        //}

        //private void AddP_inStock_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...כמות במלאי";
        //    }
        //}

        //private void AddP_image_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...מיקום התמונה במחשב";
        //    }
        //}

        //private void AddP_summary_textBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as TextBox)?.Text == "")
        //    {
        //        ((TextBox)sender).Foreground = Brushes.Gray;
        //        (sender as TextBox)!.Text = "...תקציר הספר";
        //    }
        //}

        //private void AddP_name_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...שם הספר")
        //        (sender as TextBox)!.Text = "";
        //}

        //private void AddP_author_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...שם הסופר")
        //        (sender as TextBox)!.Text = "";
        //}

        //private void AddP_price_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...מחיר")
        //        (sender as TextBox)!.Text = "";
        //}

        //private void AddP_inStock_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...כמות במלאי")
        //        (sender as TextBox)!.Text = "";
        //}

        //private void AddP_image_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...מיקום התמונה במחשב")
        //        (sender as TextBox)!.Text = "";
        //}

        //private void AddP_summary_textBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox)!.Foreground = Brushes.Black;
        //    if ((sender as TextBox)?.Text == "...תקציר הספר")
        //        (sender as TextBox)!.Text = "";
        //}
        #endregion

        #region כשהטקסט משתנה-לבדוק האם ניתן לפתוח את כפתור- הוסף ספר

        private void addProductButtonToEnable()
        {
            if (addProductButton == null)
                return;

            if (AddP_ID_textBox.Text == "0" || AddP_name_textBox.Text == ""
            || AddP_author_textBox.Text == "" || AddP_categ_commbbox.SelectedItem == null)
                addProductButton.IsEnabled = false;
            else
                addProductButton.IsEnabled = true;
        }

        private void AddP_categ_commbbox_LostFocus(object sender, RoutedEventArgs e)
        {
            addProductButtonToEnable();
        }

        private void AddP_ID_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnable();
        }

        private void AddP_name_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnable();
        }

        private void AddP_author_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnable();
        }
        #endregion

        #region הגבלות על הקלט
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion


    }
}
