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
using System.IO;

//using Do;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListForM_Window.xaml
/// </summary>
public partial class ProductListForM_Window : Window
{
    private IBl bl;
    private ObservableCollection<PO.ProductForList> allBooks = new();
    private ObservableCollection<ProductForList> productSearch = new();

    public ProductListForM_Window(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        allBooks = new ObservableCollection<PO.ProductForList>(PO.Tools.GetAllProductInPO());
        DataContext = allBooks;
    }

    //private void AllBooks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //{
    //    switch (e.Action)
    //    {
    //        case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
    //            break;
    //        case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
    //            break;
    //        case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
    //            break;
    //        case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
    //            break;
    //        case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
    //            break;
    //        default:
    //            break;
    //    }
    //}

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
        Action<int> updateAction = (ProductID) =>
        {
            PO.ProductForList p = bl.BoProduct.GetProductForList(ProductID).CopyBoPflToPoPfl();
            PO.ProductForList P_BeforUp = allBooks.FirstOrDefault(x => x.ID == p.ID)!;
            P_BeforUp.Price = p.Price; //עדכנו שדה שדה,לבדוק אם יש דרך חכמה יותר
            P_BeforUp.Category = p.Category;
            P_BeforUp.NameOfBook = p.NameOfBook;
            P_BeforUp.InStock = p.InStock;

            if(DataContext== productSearch)
            {
                PO.ProductForList P_BeforUp2 = productSearch[0]!;
                P_BeforUp2.Price = p.Price; //עדכנו שדה שדה,לבדוק אם יש דרך חכמה יותר
                P_BeforUp2.Category = p.Category;
                P_BeforUp2.NameOfBook = p.NameOfBook;
                P_BeforUp2.InStock = p.InStock;
            }

        };

        new UpdatProductForM_Window(bl, (ProductForList)Products_DateGrid.SelectedItem, updateAction).ShowDialog();

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
                    //File.Delete(bl.BoProduct.GetProductDetails_forM(pToD.OrderID).ProductImagePath!);
                    bl.BoProduct.DeleteProductByID_forM(pToD.ID);
                    ProductForList temp=allBooks.First(x=>x.ID==pToD.ID);
                    allBooks.Remove(temp);
                    if (DataContext == productSearch)
                        productSearch.Clear();
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

    #region קיבוץ לפי קטגוריה
    private void GroupByCategory_Click(object sender, RoutedEventArgs e)
    {
        if ((string)GroupByCategory.Content == "קבץ לפי קטגוריה")
        {
            GroupByCategory.Content = "בטל קיבוץ";
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_DateGrid.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);
        }
        else
        {
            GroupByCategory.Content = "קבץ לפי קטגוריה";
            remoteGroup();
        }

    }

    private void remoteGroup()
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(allBooks);
        view.GroupDescriptions.Clear();
    }
    #endregion

    #region חיפוש מוצר לפי מספר/שם המוצר
    private void search_Click(object sender, RoutedEventArgs e)
    {
        try
        {

            string myString = nameOrID.Text;

            if (iconSearch.Kind== MaterialDesignThemes.Wpf.PackIconKind.Close)
            {
                nameOrID.Text = "";
                iconSearch.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
                DataContext = allBooks;
                GroupByCategory.IsEnabled = true;
            }
            else
            { 
                ProductForList myProduct = new();

                int id;

                bool isID = int.TryParse(myString, out id);
                if (isID)
                {
                    myProduct = bl.BoProduct.GetProductDetails_forM(id).CopyBoProductToPoPFL();
                    remoteGroup();
                    productSearch.Clear();
                    productSearch.Add(myProduct);
                }
                else
                {
                    myProduct = bl.BoProduct.GetProductByName(myString).CopyBoProductToPoPFL();
                    remoteGroup();
                    productSearch.Clear();
                    productSearch.Add(myProduct);
                }

                iconSearch.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                DataContext = productSearch;
                GroupByCategory.Content = "קבץ לפי קטגוריה";
                GroupByCategory.IsEnabled = false;
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