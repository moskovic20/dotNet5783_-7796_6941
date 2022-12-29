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
using Do;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListForM_Window.xaml
/// </summary>
public partial class ProductListForM_Window : Window
{
    private IBl bl;
    private ObservableCollection<PO.ProductForList> allBooks;

    public ProductListForM_Window(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        var list = from p in bl.BoProduct.GetAllProductForList_forM()
                   select new PO.ProductForList
                   {
                       ID = p.ID,
                       NameOfBook = p.NameOfBook,
                       Price = p.Price,
                       Category = (PO.CATEGORY)p.Category
                   };

        allBooks = new ObservableCollection<PO.ProductForList>(list);
        DataContext = allBooks;
        allBooks.CollectionChanged += AllBooks_CollectionChanged;
    }

    private void AllBooks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                break;
            default:
                break;
        }
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
        Action<int> action = productId => allBooks.Add(bl.BoProduct.GetProductForList(productId).CopyBoPflToPoPfl());
        new AddProductForM_Window(bl, action).ShowDialog();
    }
    #endregion

    #region אירוע- לחיצה על כפתור עדכון ספר
    private void UpdatButton_Click(object sender, RoutedEventArgs e)
    {
        Action<int> updateAction = (ProductID)=>
        {
            PO.ProductForList p = bl.BoProduct.GetProductForList(ProductID).CopyBoPflToPoPfl();
            PO.ProductForList P_BeforUp = allBooks.FirstOrDefault(x=>x.ID==p.ID)!;
            P_BeforUp.Price = p.Price; //עדכנו שדה שדה,לבדוק אם יש דרך חכמה יותר
            P_BeforUp.Category = p.Category;
            P_BeforUp.NameOfBook = p.NameOfBook;
        };

        new UpdatProductForM_Window(bl,(ProductForList)Products_DateGrid.SelectedItem,updateAction).ShowDialog();
        
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