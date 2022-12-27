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
using PL.PO;

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

        public UpdatProductForM_Window(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            productToUpdate = new();
            this.updateCateg_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));

            DataContext = productToUpdate;
            this.idComboBox.ItemsSource = bl.BoProduct.GetAllProductForList_forM();
            this.idComboBox.DisplayMemberPath = "ID";
            this.idComboBox.SelectedValuePath = "ID";
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

                productToUpdate = new PO.Product();
                this.DataContext = productToUpdate;
                idComboBox.SelectedItem = null;
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

        #region אירוע- שינוי הבחירה בתיבת מספר מזהה
        private void idComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idComboBox.SelectedItem is BO.ProductForList)
            {
                this.productToUpdate = ((BO.ProductForList)this.idComboBox.SelectedItem).copyProductForListToPoProduct().GetCopy();
                //AddP_categ_commbbox.SelectedValue = productToUpdate.Category;
                this.DataContext = productToUpdate;
                beforUpdate = productToUpdate.GetCopy();
            }
        }
        #endregion


    }
}
