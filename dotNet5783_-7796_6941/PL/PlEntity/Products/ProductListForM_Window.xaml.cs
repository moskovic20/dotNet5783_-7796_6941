using BlApi;
//using PL.BoProducts;
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
using System.Collections.ObjectModel;
using PL.PO;
using BO;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListForM_Window.xaml
/// </summary>
public partial class ProductListForM_Window : Window
{
    private IBl bl;
    private ObservableCollection<ProductForList> allBooks;

    public ProductListForM_Window(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        allBooks = new();
        allBooks = allBooks.ToObserCollection_P();
        DataContext = allBooks;

    }


    //private void categoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    BO.CATEGORY? categ = cmbCategorySelector.SelectedItem as BO.CATEGORY?;

    //    if (categ == BO.CATEGORY.all)
    //        Products_DateGrid.ItemsSource = bl.BoProduct.GetListedProducts();
    //    else
    //        Products_DateGrid.ItemsSource = bl.BoProduct.GetListedProducts(BO.Filters.filterBYCategory, categ);
    //}

    #region אירוע-לחציה על כפתור הוסף ספר
    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        new AddProductForM_Window(bl, allBooks).ShowDialog();
        allBooks = allBooks.ToObserCollection_P();
    }
    #endregion

    #region אירוע- לחיצה על כפתור עדכון ספר
    private void UpdatButton_Click(object sender, RoutedEventArgs e)
    {
        new UpdatProductForM_Window(bl,(ProductForList)Products_DateGrid.SelectedItem).ShowDialog();
        allBooks= allBooks.ToObserCollection_P();
    }
    #endregion

    #region אירוע- לחציה על כפתור מחק ספר
    private void deleteProductButton_Click(object sender, RoutedEventArgs e)
    {
        ProductForList pToD = (ProductForList)Products_DateGrid.SelectedItem;
        try
        {
            var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה למחוק ספר זה?", "מחיקת ספר", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (delete)
            {
                case MessageBoxResult.Yes:
                    bl.BoProduct.DeleteProductByID_forM(pToD.ID);
                    allBooks.Remove(pToD);
                    MessageBox.Show("!הספר נמחק בהצלחה");
                    break;
                case MessageBoxResult.No:
                    Products_DateGrid.SelectedItem = null;
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
               MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

    }
    #endregion
}