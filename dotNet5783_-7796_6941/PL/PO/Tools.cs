using BlApi;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Net.Mail;
using System.Net;
using System.Printing;
using System.Windows.Input;
using System.Text.RegularExpressions;
using DalApi;
using PL.Catalog;
using System.Windows.Controls;

namespace PL.PO;

static class Tools
{
    private static IBl bl = BlApi.Factory.GetBl();

    #region (העתקה כללית (לא באמת בשימוש נראה לי
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

    public static void limitInputToInt(this TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    public static void limitInputToDouble(this TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9.]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    #endregion

    //public static bool IsImageNeedCare(PO.Product before, PO.Product after)
    //{
    //    if (before.Category != after.Category || before.NameOfBook != after.NameOfBook
    //        || before.ProductImagePath != after.ProductImagePath)
    //        return true;
    //    return false;
    //}

    //________________________________________productTools__________________________________________________________

    #region convert BO.productForList to PO.productForList
    public static PO.ProductForList CopyBoPflToPoPfl(this BO.ProductForList p)
    {

        PO.ProductForList product = new()
        {
            ID = p.ID,
            NameOfBook = p.NameOfBook,
            Price = p.Price,
            Category = (PO.CATEGORY)p.Category,
            InStock = p.InStock
        };

        return product;
    }
    #endregion

    #region convert from PO.ProductForList to BO.Product 

    internal static PO.ProductForList CopyBoProductToPoPFL(this BO.Product prodPO)
    {
        PO.ProductForList copyProduct = new()
        {
            ID = prodPO.ID,
            NameOfBook = prodPO.NameOfBook,
            Price = prodPO.Price,
            InStock = prodPO.InStock,
            Category = (PO.CATEGORY)prodPO.Category!
        };
        return copyProduct;
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

    internal static PO.ProductItem CopyPflToPoProductItem(this BO.ProductForList pfl)
    {
        BO.Product productBO = bl.BoProduct.GetProductDetails_forM(pfl.ID);

        PO.ProductItem product = new PO.ProductItem()
        {
            ID = pfl.ID,
            NameOfBook = pfl.NameOfBook,
            Price = pfl.Price,
            Category = (PO.CATEGORY)pfl.Category,
            //Summary = productBO.Summary,
            AmountInCart = productBO.InStock,
            //InStock=  productBO.InStock ?? 
            InStock = false //false לא עובד אז סתם שמתי שקר כמו טירונית (-___-)
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

    //public static PO.CATEGORY HebrewToEnglishCategory(this PO.Hebrew_CATEGORY hebrewCategory)
    //{
    //    if (hebrewCategory == Hebrew_CATEGORY.מסתורין)
    //        return PO.CATEGORY.mystery;
    //    if (hebrewCategory == Hebrew_CATEGORY.פנטזיה)
    //        return PO.CATEGORY.fantasy;
    //    if (hebrewCategory == Hebrew_CATEGORY.היסטוריה)
    //        return PO.CATEGORY.history;
    //    if (hebrewCategory == Hebrew_CATEGORY.מדע)
    //        return PO.CATEGORY.scinence;
    //    if (hebrewCategory == Hebrew_CATEGORY.ילדים)
    //        return PO.CATEGORY.children;
    //    if (hebrewCategory == Hebrew_CATEGORY.רומן)
    //        return PO.CATEGORY.romans;
    //    if (hebrewCategory == Hebrew_CATEGORY.בישול_ואפייה)
    //        return PO.CATEGORY.cookingAndBaking;
    //    if (hebrewCategory == Hebrew_CATEGORY.פסיכולוגיה)
    //        return PO.CATEGORY.psychology;

    //    return PO.CATEGORY.kodesh;
    //}

    public static string EnglishToHebewStringCategory(this PO.CATEGORY myCategory)
    {
        if (myCategory == PO.CATEGORY.mystery)
            return "מסתורין";
        if (myCategory == PO.CATEGORY.fantasy)
            return "פנטזיה";
        if (myCategory == PO.CATEGORY.history)
            return "היסטוריה";
        if (myCategory == PO.CATEGORY.scinence)
            return "מדע";
        if (myCategory == PO.CATEGORY.children)
            return "ילדים";
        if (myCategory == PO.CATEGORY.romans)
            return "רומן";
        if (myCategory == PO.CATEGORY.cookingAndBaking)
            return "בישול ואפייה";
        if (myCategory == PO.CATEGORY.psychology)
            return "פסיכולוגיה";

        return "קודש";
    }

    #endregion

    internal static PO.ProductItem CopyProductItemFromBoToPo(this BO.ProductItem pI)
    {
        PO.ProductItem copyProduct = new()
        {
            ID = pI.ID,
            NameOfBook = pI.NameOfBook,
            Price = pI.Price,
            Category = (PO.CATEGORY)pI.Category,
            //Summary = pI.Summary,
            AmountInCart = pI.AmountInCart,
            InStock = pI.InStock,
            ProductImagePath = pI.ProductImagePath
        };
        return copyProduct;
    }


    public static List<ProductForList> GetAllProductInPO()
    {
        var list = from p in bl.BoProduct.GetAllProductForList_forM()
                   select p.CopyBoPflToPoPfl();

        return list.ToList();

    }


    // ___________________________________________orderTools__________________________________________________________

    public static List<OrderForList> GetAllOrdersInPO()
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

        return list.ToList();
    }

    #region convert fron BO.order to PO.order
    internal static PO.Order CopyBoOrderToPoOrder(this BO.Order boOrder)
    {
        PO.Order myNewOrder = new()
        {
            OrderID = boOrder.OrderID,
            CustomerName = boOrder.CustomerName,
            CustomerEmail = boOrder.CustomerEmail,
            CustomerAddress = boOrder.CustomerAddress,
            DateOrder = boOrder.DateOrder,
            Status = (PO.OrderStatus)boOrder.Status,
            PaymentDate = boOrder.PaymentDate,
            ShippingDate = boOrder.ShippingDate,
            DeliveryDate = boOrder.DeliveryDate,
            TotalPrice = boOrder.TotalPrice
        };

        if (boOrder.Items != null)
        {
            var list = from myOI in boOrder.Items
                       select new OrderItem()
                       {
                           OrderID = myOI.OrderID,
                           ProductID = myOI.ProductID,
                           NameOfBook = myOI.NameOfBook,
                           PriceOfOneItem = myOI.PriceOfOneItem,
                           AmountOfItems = myOI.AmountOfItems,
                           TotalPrice = myOI.TotalPrice
                       };

            myNewOrder.Items = new(list);
        }
        return myNewOrder;
    }
    #endregion


    // ___________________________________________CartTools__________________________________________________________


    public static BO.Cart CastingFromPoToBoCart(this PO.Cart cart)
    {
        BO.Cart newCart;

        if (cart.Items != null)
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
        else
        {
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
            Items = new(list.ToList()),
            TotalPrice = cart.TotalPrice
        };
        return newCart;
    }

    public static void putTo(this BO.Cart sorce, PO.Cart target)
    {
        var list = from myOI in sorce.Items
                   select new PO.OrderItem()
                   {
                       OrderID = myOI.OrderID,
                       ProductID = myOI.ProductID,
                       NameOfBook = myOI.NameOfBook,
                       PriceOfOneItem = myOI.PriceOfOneItem,
                       AmountOfItems = myOI.AmountOfItems,
                       TotalPrice = myOI.TotalPrice
                   };

        target.Items = new(list.ToList());
        target.TotalPrice = sorce.TotalPrice;
    }

    public static void reboot(this PO.Cart cart)
    {
        cart.Items = null;
        cart.CustomerName = null;
        cart.CustomerEmail = null;
        cart.CustomerAddress = null;
        cart.TotalPrice = null;
    }
}
