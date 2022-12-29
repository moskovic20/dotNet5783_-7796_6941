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
using PL.PO;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

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


    }
}
