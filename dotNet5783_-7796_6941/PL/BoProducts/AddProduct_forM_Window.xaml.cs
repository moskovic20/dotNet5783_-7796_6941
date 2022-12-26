using BlApi;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BO;

namespace PL.BoProducts;

/// <summary>
/// Interaction logic for AddProduct_forM_Window.xaml
/// </summary>
public partial class AddProduct_forM_Window : Window
{
    private IBl bl;

    public AddProduct_forM_Window(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        // AddP_categ_commbbox.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));
    }

        private void AddP_ID_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...מספר מזהה";
            }
           
        }

        private void AddP_name_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...שם הספר";
            }
        }

        private void AddP_author_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...שם הסופר";
            }

        }

        private void AddP_price_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...מחיר";
            }
        }

        private void AddP_inStock_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...כמות במלאי";
            }
        }

        private void AddP_image_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...מיקום התמונה במחשב";
            }
        }

        private void AddP_summary_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.Text == "")
            {
                ((TextBox)sender).Foreground = Brushes.Gray;
                (sender as TextBox)!.Text = "...תקציר הספר";
            }
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //דרוש טיפול בקטגוריה-המרה לעברית וכו..
                //string selectedcmb = "";
                //var comboBoxItem = AddP_categ_commbbox.Items[AddP_categ_commbbox.SelectedIndex] as ComboBoxItem;
                //if (comboBoxItem != null)
                //{
                //    selectedcmb = comboBoxItem.Content.ToString()!;
                //}
                bl.BoProduct.AddProduct_forM(new Product
                {
                    ID = int.Parse(AddP_ID_textBox.Text),
                    NameOfBook = AddP_name_textBox.Text,
                    AuthorName = AddP_author_textBox.Text,
                    //Category = (BO.CATEGORY)AddP_categ_commbbox.SelectedIndex,
                    Price = AddP_price_textBox.Text == "...מחיר" ? null : int.Parse(AddP_price_textBox.Text),
                    InStock = AddP_inStock_textBox.Text == "...כמות במלאי" ? null : int.Parse(AddP_inStock_textBox.Text),
                    path = AddP_image_textBox.Text == "...מיקום התמונה במחשב" ? null : AddP_image_textBox.Text,
                    Summary = AddP_summary_textBox.Text == "תקציר" ? null : AddP_summary_textBox.Text
                });

                MessageBox.Show("!הספר נוסף בהצלחה");

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

        private void AddP_ID_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...מספר מזהה")
                (sender as TextBox)!.Text = "";

        }

        private void AddP_name_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...שם הספר")
                (sender as TextBox)!.Text = "";
        }

        private void AddP_author_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...שם הסופר")
                (sender as TextBox)!.Text = "";
        }

        private void AddP_price_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...מחיר")
                (sender as TextBox)!.Text = "";
        }

        private void AddP_inStock_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...כמות במלאי")
                (sender as TextBox)!.Text = "";
        }

        private void AddP_image_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...מיקום התמונה במחשב")
                (sender as TextBox)!.Text = "";
        }

        private void AddP_summary_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)!.Foreground = Brushes.Black;
            if ((sender as TextBox)?.Text == "...תקציר הספר")
                (sender as TextBox)!.Text = "";
        }

        private void addProductButtonToEnabled()
        {
            if (addProductButton == null)
                return;

                if (AddP_ID_textBox.Text == "...מספר מזהה" || AddP_name_textBox.Text == "...שם הספר"
                || AddP_author_textBox.Text == "...שם הסופר" || AddP_categ_commbbox.SelectedItem == null)
                addProductButton.IsEnabled =false;
            else
                addProductButton.IsEnabled = true;
        }

        private void AddP_categ_commbbox_LostFocus(object sender, RoutedEventArgs e)
        {
            addProductButtonToEnabled();
        }

        private void AddP_ID_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnabled();
        }

        private void AddP_name_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnabled();
        }

        private void AddP_author_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addProductButtonToEnabled();
        }
    
}
