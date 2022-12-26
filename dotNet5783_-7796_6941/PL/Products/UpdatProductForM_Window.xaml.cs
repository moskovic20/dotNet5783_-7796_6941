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
            this.AddP_categ_commbbox.ItemsSource = Enum.GetValues(typeof(PO.Hebrew_CATEGORY));

            DataContext =productToUpdate;
            this.idComboBox.ItemsSource = bl.BoProduct.GetAllProductForList_forM();
            this.idComboBox.DisplayMemberPath = "ID";
            this.idComboBox.SelectedValuePath = "ID";
        }

        private void idComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idComboBox.SelectedItem is BO.ProductForList)
            {
                this.productToUpdate = ((BO.ProductForList)this.idComboBox.SelectedItem).copyProductForListToPO().GetCopy();
                //AddP_categ_commbbox.SelectedValue = productToUpdate.Category;
                this.DataContext = productToUpdate;
                beforUpdate = productToUpdate.GetCopy();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? selectedcmb = "";
                var comboBoxItem =idComboBox.SelectedItem;
                if (comboBoxItem != null)
                {
                    selectedcmb = comboBoxItem.ToString();
                }

                bl.BoProduct.UpdateProductDetails_forM(productToUpdate.CopyProductToBO());
                MessageBox.Show("!הספר עודכן בהצלחה");

                productToUpdate = new PO.Product();
                this.DataContext = productToUpdate;
                idComboBox.SelectedItem = null;
            }
            catch (BO.Update_Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch
            {
                MessageBox.Show("תקלה בעדכון הקו", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }

        }

        #region כשהטקסט משתנה-לבדוק האם ניתן לפתוח את כפתור- עדכן ספר

        private void updateProductButtonToEnable()
        {
            if (updateProductButton == null)
                return;

           if(productToUpdate!=beforUpdate!)
                updateProductButton.IsEnabled = true;
           else
            updateProductButton.IsEnabled = false;
        }


        
        #endregion
    }
}
