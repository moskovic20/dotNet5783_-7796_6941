using BlApi;
using PL.PO;
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

namespace PL.Catalog;
/// <summary>
/// Interaction logic for Favorites.xaml
/// </summary>
public partial class Favorites : Page
{
    IBl bl;
    Frame myFrame;

    private Cart myCart;
    private ObservableCollection<ProductItem>? lovedBooks;

    public Favorites(IBl bl, Frame frame, ObservableCollection<ProductItem>? Loved, Cart myCart)
    {
        InitializeComponent();
        this.bl = bl;
        myFrame = frame;
        this.myCart = myCart;
        lovedBooks = Loved;
        DataContext = lovedBooks;
    }

    #region לחיצה על ספר-מעבר לעמוד פרטי הספר המלאים
    private void Catalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ProductItem p = (ProductItem)((ListView)sender).SelectedItem;
        this.myFrame.Content = new bookDetails(p.ID, bl, myCart);

    }
    #endregion


    #region לחיצה על הוסף לאהובים-מהקטלוג
    private void CancelFavoritedBook_Click(object sender, RoutedEventArgs e)
    {
        Button button = (sender as Button)!;
        PO.ProductItem p = (button.DataContext as PO.ProductItem)!;
        lovedBooks?.Remove(p);

    }
    #endregion

}



