﻿using BlApi;
using PL.PO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Catalog
{
    /// <summary>
    /// Interaction logic for bookDetails.xaml
    /// </summary>
    public partial class bookDetails : Page
    {
        IBl bl;
        Product myProduct;
        Cart myCart;

        public bookDetails(int ID,IBl bl,Cart Cart)
        {
            InitializeComponent();

            this.bl = bl;
            myCart = Cart;
            myProduct = bl.BoProduct.GetProductDetails_forM(ID).copyProductToPo();
            DataContext = myProduct;
        }

        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            myCart = bl.BoCart.UpdateProductAmountInCart(myCart.CastingFromPoToBoCart(), myProduct.ID, (int)gradeNumUpDown.Value ).CastingFromBoToPoCart();
        }
    }
}
