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

                if (AddP_ID_textBox.Text == "0")
                    throw new Exception("...הכנס מספר מזהה ");
                if (AddP_name_textBox.Text == "")
                    throw new Exception("...הכנס את שם הספר");
                if (AddP_author_textBox.Text == "")
                    throw new Exception("...הכנס את שם הסופר");
                if (AddP_categ_commbbox.SelectedItem == null)
                    throw new Exception("הכנס את קטגוריית הספר");
                   

                int newID=bl.BoProduct.AddProduct_forM(product.CopyProductToBO());

                if(newID!=product.ID)
                    MessageBox.Show("!הספר בוצע בהצלחה"+"\n:שים לב- עקב כפילות של מספר המוצר מעתה מספר המוצר יהיה"+newID, "מזהה כפול", MessageBoxButton.OK, MessageBoxImage.Information);
                
                MessageBox.Show("!הספר נוסף בהצלחה");

                product = new PO.Product();
                this.DataContext = product;
                AddP_categ_commbbox.SelectedItem = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message +"\n"+ex.InnerException?.Message,"שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
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


    }
}
