using BlApi;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.PO;

namespace PL.PlEntity.Products;

/// <summary>
/// Interaction logic for productList.xaml
/// </summary>
public partial class productList : Page
{
    private IBl bl;
    private ObservableCollection<PO.ProductForList> allBooks = new();
    private ObservableCollection<ProductForList> productSearch = new();

    public productList(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        allBooks = new ObservableCollection<PO.ProductForList>(PO.Tools.GetAllProductInPO());
        DataContext = allBooks;
    }


    #region אירוע-לחיצה על כפתור הוסף ספר
    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        Action<int> action = productID =>
        {
            ProductForList pToAdd = bl.BoProduct.GetProductForList(productID).CopyBoPflToPoPfl();
            allBooks.Add(pToAdd);
            if(DataContext==productSearch)
            {
                int myInt = 0;
                int.TryParse(nameOrID.Text,out myInt);
                if (pToAdd.NameOfBook.Contains(nameOrID.Text) || pToAdd.ID == myInt)
                    productSearch.Add(pToAdd);
            }
        };
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

            if (DataContext == productSearch)
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

    #region אירוע- לחיצה על כפתור מחק ספר
    private void deleteProductButton_Click(object sender, RoutedEventArgs e)
    {
        ProductForList pToD = (ProductForList)Products_DateGrid.SelectedItem;
        try
        {
            var delete = MessageBox.Show("האם אתה בטוח שאתה רוצה למחוק ספר זה?", "מחיקת ספר", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (delete)
            {
                case MessageBoxResult.Yes:

                    //File.Delete(bl.BoProduct.GetProductDetails_forM(pToD.ID).ProductImagePath!);

                    bl.BoProduct.DeleteProductByID_forM(pToD.ID);
                    ProductForList temp = allBooks.First(x => x.ID == pToD.ID);
                    allBooks.Remove(temp);

                    if (DataContext == productSearch)
                    {
                        ProductForList pfl = productSearch.First(x => x.ID == pToD.ID);
                        productSearch.Remove(pfl);
                    }

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
    private void nameOrID_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {

            string myString = nameOrID.Text;


            if (myString==""|| myString == null)
            {
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
                    var list = from p in bl.BoProduct.GetAllProductByNumber(id)
                               select p.CopyBoPflToPoPfl();

                    remoteGroup();
                    productSearch.Clear();
                    productSearch = new(list);
                }
                else
                {

                    var list = from p in bl.BoProduct.GetProductsByName(myString)
                               select p.CopyBoPflToPoPfl();

                    remoteGroup();
                    productSearch.Clear();
                    productSearch = new(list);
                }

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
