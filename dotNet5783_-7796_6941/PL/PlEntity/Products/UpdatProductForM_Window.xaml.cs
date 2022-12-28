using BlApi;
using PL.PO;
using System;
using System.Windows;

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


        public UpdatProductForM_Window(IBl bl, ProductForList pToUp)
        {
            InitializeComponent();
            this.bl = bl;
            productToUpdate = new();
            productToUpdate = bl.BoProduct.GetProductDetails_forM(pToUp.ID).copyProductToPo();
            this.updateCateg_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));

            DataContext = productToUpdate;
            beforUpdate = productToUpdate.GetCopy();
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
