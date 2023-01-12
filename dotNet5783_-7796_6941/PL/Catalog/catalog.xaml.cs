using BlApi;
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
using System.Text.RegularExpressions;
using PL.Products;
//using BO;

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for catalog.xaml
    /// </summary>
    public partial class catalog : Page
    {
        IBl bl;
        Frame myFrame;
        Cart myCart;

        private ObservableCollection<ProductItem>? BooksForShow;
        private ObservableCollection<ProductItem>? allBooks;
        private ObservableCollection<ProductItem>? oneGroupOfBooks;
        IOrderedEnumerable<IGrouping<string, ProductItem>>? allGroups;

        public catalog(IBl bl, Cart cart, Frame frame)
        {
            InitializeComponent();
            this.bl = bl;
            myFrame = frame;
            myCart = cart;
            BooksForShow = new(bl.BoProduct.GetAllProductItems_forC().Select(p => p.CopyProductItemFromBoToPo()));
            allBooks = new(BooksForShow);
            DataContext = BooksForShow;


        }

        private void Catalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ProductItem p = (ProductItem)((ListView)sender).SelectedItem;
            this.myFrame.Content = new bookDetails(p.ID, bl,myCart);

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Button button = (sender as Button)!;
                PO.ProductItem p = (button.DataContext as PO.ProductItem)!;
                bl.BoCart.AddProductToCart(myCart.CastingFromPoToBoCart(), p.ID).putTo(myCart);//הוספת המוצר לשכבה מתחת 
                MessageBox.Show("!הספר נוסף בהצלחה לסל הקניות");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void SortByPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((ComboBox)sender).SelectedIndex == 0)
            {
                var list = BooksForShow?.OrderBy(ProductItem => ProductItem.Price);
                BooksForShow = new(list!);
                DataContext = BooksForShow;
            }

            if (((ComboBox)sender).SelectedIndex == 1)
            {
                var list = BooksForShow?.OrderByDescending(ProductItem => ProductItem.Price);
                BooksForShow = new(list!);
                DataContext = BooksForShow;
            }

            if (((ComboBox)sender).SelectedIndex == 2)
            {
                BooksForShow = new(oneGroupOfBooks!);
                DataContext = BooksForShow;
            }
        }

        private void CategoryFilter_DropDownOpened(object sender, EventArgs e)
        {

            if (CategoryFilter.Items.Count == 0)
            {
                var groupsList = from book in allBooks
                                 group book by book.Category.EnglishToHebewStringCategory() into newGroup
                                 orderby newGroup.Key
                                 select newGroup;

                allGroups = groupsList;

                foreach (var group in groupsList)
                    CategoryFilter.Items.Add(group.Key);
       
                CategoryFilter.Items.Add("");

            }
        }

        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory =(string)((ComboBox)sender).SelectedItem;

            if (selectedCategory != null)
            {
                nameOfBook.Text = null;

                if (selectedCategory == "")
                {
                    BooksForShow = new(allBooks!);
                    DataContext = BooksForShow;
                   
                }
                else
                {
                    foreach (var group in allGroups!)
                    {
                        if (group.Key == selectedCategory)
                        {
                            BooksForShow = new(group.TakeWhile(x => true));
                        }

                    }
                    SortByPraic.SelectedItem = null;
                }

                oneGroupOfBooks = new(BooksForShow!);
                DataContext = BooksForShow;
            }

        }

        private void nameOfBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            string book = nameOfBook.Text;

                if (book != "")
                {
                    var list = from b in bl.BoProduct.GetProductsByName(book)
                               select b.CopyPropTo(new ProductItem());

                    BooksForShow = new(list);
                    DataContext = BooksForShow;
                    CategoryFilter.SelectedItem = null;
                }
                else
                {
                    BooksForShow = new(allBooks!);
                    DataContext = BooksForShow;
                }
        }
    }
}
