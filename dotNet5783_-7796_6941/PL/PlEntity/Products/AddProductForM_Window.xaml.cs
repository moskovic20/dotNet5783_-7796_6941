using BlApi;
using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using Microsoft.Win32;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Products;

/// <summary>
/// Interaction logic for AddProductForM_Window.xaml
/// </summary>
public partial class AddProductForM_Window : Window
{
    private IBl bl;


    public PO.Product productToAdd
    {
        get { return (PO.Product)GetValue(productToAddProperty); }
        set { SetValue(productToAddProperty, value); }
    }

    // Using a DependencyProperty as the backing store for productToAdd.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productToAddProperty =
        DependencyProperty.Register("productToAdd", typeof(PO.Product), typeof(AddProductForM_Window));

    Action<int> action;


    public AddProductForM_Window(IBl bl, Action<int> action)
    {
        InitializeComponent();
        this.bl = bl;
        this.action = action;

        productToAdd = new PO.Product();
        //this.DataContext = productToAdd;

        this.AddP_categ_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));
    }

    #region אירוע-לחיצה על כפתור הוסף
    private void addProductButton_Click(object sender, RoutedEventArgs e)
    {
        string fullPath = productToAdd.productImagePath!;

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
            if (priceText.Text == "" || inStock_TextBox.Text == "")
            {
                var mbResult = MessageBox.Show("ללא הכנסת ערך עבור מחיר מוצר או עבור כמות מוצר- הלקוח לא יכול לראות ולקנות מוצר זה.\nברצונך להמשיך את ההוספה?", "שים לב", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                switch (mbResult)
                {
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        return;
                        //break;
                }

            }
        
            string sorce, suffix, target;

            if (!string.IsNullOrWhiteSpace(productToAdd.productImagePath))
            {

                 sorce = productToAdd.productImagePath;
                 suffix = "." + sorce.Split(@".").Last();
                 target = @"..\PL\ProductImages\"+productToAdd.NameOfBook+suffix;
                //productToAdd.ProductImagePath = target;
            }
            else //תמונת ברירת מחדל
            { 
                 sorce = @"..\PL\ProductImages\Default.jpeg";
                 target = @"..\PL\ProductImages\" + productToAdd.NameOfBook+ ".jpeg";
               // productToAdd.ProductImagePath = target;
                productImage.Source = null;
            }

            productToAdd.ProductImagePath = target;
            int newID = bl.BoProduct.AddProduct_forM(productToAdd.CopyProductToBO());
            productToAdd.ProductImagePath = fullPath;
            File.Copy(sorce, target);
            action(newID);
            MessageBox.Show("!הספר נוסף בהצלחה");
            //this.Close();

            //productImage.Source = null;
            productToAdd = new PO.Product();
            AddP_categ_commbbox.SelectedItem = null;

        }
        catch(BO.Adding_Exception ex)
        {
            productToAdd.ProductImagePath = fullPath;
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
               MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

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
    private void PreviewTextInputToDouble(object sender, TextCompositionEventArgs e)
    {
       e.limitInputToDouble();
    }

    private void PreviewTextInputToInt(object sender, TextCompositionEventArgs e)
    {
        e.limitInputToInt();
    }

    #endregion

    #region בחירת תמונה עבר ספר
    private void choosePicture(object sender, RoutedEventArgs e)
    {

        OpenFileDialog op = new OpenFileDialog();
        op.Title = "Select a picture";
        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
          "Portable Network Graphic (*.png)|*.png";
        if (op.ShowDialog() == true)
        {
            productToAdd.ProductImagePath = op.FileName;
        }

    }
    #endregion

    #region הסרת תמונה
    private void RemoteImage_Click(object sender, RoutedEventArgs e)
    {
        productImage.Source = null;
        productToAdd.productImagePath = null;
    }
    #endregion
}
