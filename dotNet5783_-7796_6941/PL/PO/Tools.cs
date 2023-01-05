using BlApi;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Net.Mail;
using System.Net;
using System.Printing;
using Do;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace PL.PO
{
    static class Tools
    {
        private static IBl bl = BlApi.Factory.GetBl();


        public static Target CopyPropTo<Source, Target>(this Source source, Target target)
        {
            Dictionary<string, PropertyInfo> propertyInfoTarget = target!.GetType().GetProperties()
                .ToDictionary(key => key.Name, value => value);

            IEnumerable<PropertyInfo> propertyInfoSource = source!.GetType().GetProperties();

            foreach (var item in propertyInfoSource)
            {
                if (propertyInfoTarget.ContainsKey(item.Name) && (item.PropertyType == typeof(string) || !item.PropertyType.IsClass))
                {
                    Type typeSource = Nullable.GetUnderlyingType(item.PropertyType)!;
                    Type typeTarget = Nullable.GetUnderlyingType(propertyInfoTarget[item.Name].PropertyType)!;

                    object value = item.GetValue(source)!;

                    if (value is not null)
                    {
                        if (propertyInfoTarget[item.Name].PropertyType == item.PropertyType || item.PropertyType.IsEnum)
                            propertyInfoTarget[item.Name].SetValue(target, value);

                        else if (typeSource is not null && typeTarget is not null)
                            value = Enum.ToObject(typeTarget, value);
                    }
                }
            }


            return target;
        }

        public static IEnumerable<Target> CopyListTo<Source, Target>(this IEnumerable<Source> sources) where Target : new()
   => from source in sources
      select source.CopyPropTo(new Target());

        //public static string CategoryToString(this CATEGORY? categ)
        //{
        //    if (categ == CATEGORY.children)
        //        return "children";
        //    if (categ == CATEGORY.cookingAndBaking)
        //        return "cookingAndBaking";
        //    if (categ == CATEGORY.fantasy)
        //        return "fantasy";
        //    if (categ == CATEGORY.history)
        //        return "history";
        //    if (categ == CATEGORY.kodesh)
        //        return "kodesh";
        //    if (categ == CATEGORY.mystery)
        //        return "mystery";
        //    if (categ == CATEGORY.psychology)
        //        return "psychology";
        //    if (categ == CATEGORY.romans)
        //        return "romans";
        //    return "scinence";

        //}

        //________________________________________productTools__________________________________________________________

        #region convert BO.productForList to PO.productForList
        internal static PO.ProductForList CopyBoPflToPoPfl(this BO.ProductForList p)
        {

            PO.ProductForList product = new ()
            {
                ID = p.ID,
                NameOfBook = p.NameOfBook,
                Price = p.Price,
                Category = (PO.CATEGORY)p.Category

            };

            return product;
        }
        #endregion

        #region convert from PO.Product to BO.Product and vice versa

        internal static BO.Product CopyProductToBO(this PO.Product prodPO)
        {
            BO.Product copyProduct = new()
            {
                ID = prodPO.ID,
                NameOfBook = prodPO.NameOfBook,
                AuthorName = prodPO.AuthorName,
                Summary = prodPO.Summary,
                Price = prodPO.Price,
                InStock = prodPO.InStock,
                ProductImagePath = prodPO.ProductImagePath,
                Category = (BO.CATEGORY)prodPO.Category!
            };
            return copyProduct;
        }

        internal static PO.Product copyProductToPo(this BO.Product p)
        {

            PO.Product product = new PO.Product()
            {
                ID = p.ID,
                NameOfBook = p.NameOfBook,
                AuthorName = p.AuthorName,
                Price = p.Price,
                Summary = p.Summary,
                ProductImagePath = p.ProductImagePath,
                InStock = p.InStock,
                Category = (PO.CATEGORY)p.Category

            };

            return product;
        }

        #endregion

        #region convert from BO.ProductForList to PO.Product and vice versa

        internal static PO.Product CopyPflToPoProduct(this BO.ProductForList pfl)
        {
            BO.Product productBO = bl.BoProduct.GetProductDetails_forM(pfl.ID);

            PO.Product product = new PO.Product()
            {
                ID = pfl.ID,
                NameOfBook = pfl.NameOfBook,
                AuthorName = productBO.AuthorName,
                Price = pfl.Price,
                Summary = productBO.Summary,
                ProductImagePath = productBO.ProductImagePath,
                InStock = productBO.InStock,
                Category = (PO.CATEGORY)pfl.Category

            };

            return product;
        }

        internal static BO.ProductForList CopyProductToBoPFL(this PO.Product p)
        {
            BO.ProductForList myNewPFL = new BO.ProductForList()
            {
                NameOfBook = p.NameOfBook,
                ID = p.ID,
                Price = p.Price,
                Category = (BO.CATEGORY)p.Category!
            };

            return myNewPFL;
        }

        #endregion

        #region Converting the category from English to Hebrew and vice versa.

        public static PO.CATEGORY HebrewToEnglishCategory(this PO.Hebrew_CATEGORY? hebrewCategory)
        {
            if (hebrewCategory == Hebrew_CATEGORY.מסתורין)
                return PO.CATEGORY.mystery;
            if (hebrewCategory == Hebrew_CATEGORY.פנטזיה)
                return PO.CATEGORY.fantasy;
            if (hebrewCategory == Hebrew_CATEGORY.היסטוריה)
                return PO.CATEGORY.history;
            if (hebrewCategory == Hebrew_CATEGORY.מדע)
                return PO.CATEGORY.scinence;
            if (hebrewCategory == Hebrew_CATEGORY.ילדים)
                return PO.CATEGORY.children;
            if (hebrewCategory == Hebrew_CATEGORY.רומן)
                return PO.CATEGORY.romans;
            if (hebrewCategory == Hebrew_CATEGORY.בישול_ואפייה)
                return PO.CATEGORY.cookingAndBaking;
            if (hebrewCategory == Hebrew_CATEGORY.פסיכולוגיה)
                return PO.CATEGORY.psychology;

            return PO.CATEGORY.kodesh;
        }

        public static Hebrew_CATEGORY EnglishToHebewCategory(this PO.CATEGORY myCategory)
        {
            if (myCategory == PO.CATEGORY.mystery)
                return Hebrew_CATEGORY.מסתורין;
            if (myCategory == PO.CATEGORY.fantasy)
                return Hebrew_CATEGORY.פנטזיה;
            if (myCategory == PO.CATEGORY.history)
                return Hebrew_CATEGORY.היסטוריה;
            if (myCategory == PO.CATEGORY.scinence)
                return Hebrew_CATEGORY.מדע;
            if (myCategory == PO.CATEGORY.children)
                return Hebrew_CATEGORY.ילדים;
            if (myCategory == PO.CATEGORY.romans)
                return Hebrew_CATEGORY.רומן;
            if (myCategory == PO.CATEGORY.cookingAndBaking)
                return Hebrew_CATEGORY.בישול_ואפייה;
            if (myCategory == PO.CATEGORY.psychology)
                return Hebrew_CATEGORY.פסיכולוגיה;

            return Hebrew_CATEGORY.קודש;
        }

        #endregion

        #region convert fron BO.order to PO.order
        internal static PO.Order CopyBoOrderToPoOrder(this BO.Order boOrder)
        {
            PO.Order myNewOrder = new()
            {
                ID = boOrder.ID,
                CustomerName= boOrder.CustomerName,
                CustomerEmail =boOrder.CustomerEmail,
                ShippingAddress=boOrder.ShippingAddress,
                DateOrder=boOrder.DateOrder,
                Status=(PO.OrderStatus)boOrder.Status,
                PaymentDate=boOrder.PaymentDate,
                ShippingDate=boOrder.ShippingDate,
                DeliveryDate=boOrder.DeliveryDate,
                TotalPrice=boOrder.TotalPrice
            };

            var list = from myOI in boOrder.Items
                       select new OrderItem()
                       {
                           OrderID=myOI.OrderID,
                           ProductID=myOI.ProductID,
                           NameOfBook=myOI.NameOfBook,
                           PriceOfOneItem=myOI.PriceOfOneItem,
                           AmountOfItems=myOI.AmountOfItems,
                           TotalPrice=myOI.TotalPrice
                       };

            myNewOrder.Items = new(list);
            return myNewOrder;
        }
        #endregion

        public static ObservableCollection<ProductForList> ToObserCollection_P(this ObservableCollection<ProductForList> allBooks)
        {
            var list = from p in bl.BoProduct.GetAllProductForList_forM()
                       select new PO.ProductForList
                       {
                           ID = p.ID,
                           NameOfBook = p.NameOfBook,
                           Price = p.Price,
                           Category = (PO.CATEGORY)p.Category
                       };

            allBooks = new ObservableCollection<PO.ProductForList>(list);
            return allBooks;
        }


       // ___________________________________________orderTools__________________________________________________________

        public static ObservableCollection<OrderForList> ToObserCollection_O(this ObservableCollection<OrderForList> allOrders)
        {
            var list = from O in bl.BoOrder.GetAllOrderForList()
                       select new PO.OrderForList
                       {
                           OrderID = O.OrderID,
                           CustomerName = O.CustomerName,
                           Status = (PO.OrderStatus)O.Status,
                           AmountOfItems = O.AmountOfItems,
                           TotalPrice = O.TotalPrice
                       };

            allOrders = new ObservableCollection<PO.OrderForList>(list);

            return allOrders;
        }

        public static BO.Cart CastingFromPoToBoCart(this PO.Cart cart)
        {
            BO.Cart newCart;

            if (cart.Items !=null)
            {
                var list = from myOI in cart.Items
                            select new BO.OrderItem()
                            {
                                OrderID = myOI.OrderID,
                                ProductID = myOI.ProductID,
                                NameOfBook = myOI.NameOfBook,
                                PriceOfOneItem = myOI.PriceOfOneItem,
                                AmountOfItems = myOI.AmountOfItems,
                                TotalPrice = myOI.TotalPrice
                            };
                newCart = new BO.Cart()
                {
                    CustomerName = cart.CustomerName,
                    CustomerEmail = cart.CustomerEmail,
                    CustomerAddress = cart.CustomerAddress,
                    Items = list.ToList(),
                    TotalPrice = cart.TotalPrice

                };
            }
            else {
                newCart = new BO.Cart()
                {
                    CustomerName = cart.CustomerName,
                    CustomerEmail = cart.CustomerEmail,
                    CustomerAddress = cart.CustomerAddress,

                    TotalPrice = cart.TotalPrice

                };
            }



            return newCart;
        }

        public static PO.Cart CastingFromBoToPoCart(this BO.Cart cart)
        {
            var list = from myOI in cart.Items
                       select new PO.OrderItem()
                       {
                           OrderID = myOI.OrderID,
                           ProductID = myOI.ProductID,
                           NameOfBook = myOI.NameOfBook,
                           PriceOfOneItem = myOI.PriceOfOneItem,
                           AmountOfItems = myOI.AmountOfItems,
                           TotalPrice = myOI.TotalPrice
                       };

            PO.Cart newCart = new PO.Cart()
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress,
                Items = list.ToList(),
                TotalPrice = cart.TotalPrice

            };

            return newCart;
        }
    }
}
