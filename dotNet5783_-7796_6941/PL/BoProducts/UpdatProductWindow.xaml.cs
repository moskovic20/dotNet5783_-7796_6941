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

namespace PL.BoProducts;

/// <summary>
/// Interaction logic for UpdatProductWindow.xaml
/// </summary>
public partial class UpdatProductWindow : Window
{
    private IBl bl;
    BO.Product product;
    private List<string> errorMessages;
    public UpdatProductWindow(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;
        //this.product = bl.BoProduct.GetProductDetails_forM();//???
        this.studentCampusComboBox.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));//
        this.studentGenderComboBox.ItemsSource = Enum.GetValues(typeof(BO.CATEGORY));//

        refreshData(); //this.studentIdComboBox.ItemsSource = bl.GetAllStudents();
        this.studentIdComboBox.DisplayMemberPath = "StudentId";
        this.studentIdComboBox.SelectedValuePath = "StudentId";

        errorMessages = new List<string>();
    }



    private void changeImageButton_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
        f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        if (f.ShowDialog() == true)
        {
            //this.studentImage.Source = new BitmapImage(new Uri(f.FileName));

        }
    }


    private void validation_Error(object sender, ValidationErrorEventArgs e)
    {
        if (e.Action == ValidationErrorEventAction.Added)
            errorMessages.Add(e.Error.Exception.Message);
        else
            errorMessages.Remove(e.Error.Exception.Message);

        this.addButton.IsEnabled = !errorMessages.Any();
    }

    private void updateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (errorMessages.Any()) //errorMessages.Count > 0 
            {
                string err = "Exception:";
                foreach (var item in errorMessages)
                    err += "\n" + item;

                MessageBox.Show(err);
                return;
            }
            else
            {
                bl.BoProduct.UpdateProductDetails_forM(product);
                this.DataContext = product = null;//?
                refreshData();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void studentIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (this.studentIdComboBox.SelectedItem is BO.Product)
        {
            this.product = ((BO.Product)this.studentIdComboBox.SelectedItem)/*.DeepClone()*/;
            this.DataContext = product;
        }
    }

    private void deleteDataGridButton_Click(object sender, RoutedEventArgs e)
    {
        BO.Product obj = this.StudentDataGrid.SelectedItem as BO.Product;
        if (obj != null)
        {
            MessageBox.Show($"delete Student: \n{obj}");
        }
    }


    private void refreshData()
    {
        try
        {
            this.studentIdComboBox.ItemsSource = bl.BoProduct.GetAllProductForList_forM();//GetListedProducts() ?
            this.StudentDataGrid.ItemsSource = bl.BoProduct.GetAllProductForList_forM();
        }
        catch
        {
            this.Close();
        }
    }

}