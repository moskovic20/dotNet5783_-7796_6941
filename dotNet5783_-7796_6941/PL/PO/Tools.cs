using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace PL.PO
{
    static class Tools
    {
        private static IBl bl = BlApi.Factory.GetBl();

        #region convert from PO.Product to BO.Product
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
                path = prodPO.Path,
                Category = (BO.CATEGORY)prodPO.Category!
            };
            return copyProduct;
        }
        #endregion

        #region Converting the category from English to Hebrew and vice versa.

        //public static PO.CATEGORY HebrewToEnglishCategory(this PO.Hebrew_CATEGORY? hebrewCategory)
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
        //        return PO.CATEGORY.childen;
        //    if (hebrewCategory == Hebrew_CATEGORY.רומן)
        //        return PO.CATEGORY.romans;
        //    if (hebrewCategory == Hebrew_CATEGORY.בישול_ואפייה)
        //        return PO.CATEGORY.cookingAndBaking;
        //    if (hebrewCategory == Hebrew_CATEGORY.פסיכולוגיה)
        //        return PO.CATEGORY.psychology;

        //    return PO.CATEGORY.Kodesh;
        //}

        //public static Hebrew_CATEGORY EnglishToHebewCategory(this PO.CATEGORY myCategory)
        //{
        //    if (myCategory == PO.CATEGORY.mystery)
        //        return Hebrew_CATEGORY.מסתורין;
        //    if (myCategory == PO.CATEGORY.fantasy)
        //        return Hebrew_CATEGORY.פנטזיה;
        //    if (myCategory == PO.CATEGORY.history)
        //        return Hebrew_CATEGORY.היסטוריה;
        //    if (myCategory == PO.CATEGORY.scinence)
        //        return Hebrew_CATEGORY.מדע;
        //    if (myCategory == PO.CATEGORY.childen)
        //        return Hebrew_CATEGORY.ילדים;
        //    if (myCategory == PO.CATEGORY.romans)
        //        return Hebrew_CATEGORY.רומן;
        //    if (myCategory == PO.CATEGORY.cookingAndBaking)
        //        return Hebrew_CATEGORY.בישול_ואפייה;
        //    if (myCategory == PO.CATEGORY.psychology)
        //        return Hebrew_CATEGORY.פסיכולוגיה;

        //    return Hebrew_CATEGORY.קודש;
        //}

        #endregion

        internal static PO.Product copyProductForListToPoProduct(this BO.ProductForList pfl)
        {
            BO.Product productBO = bl.BoProduct.GetProductDetails_forM(pfl.ID);

            PO.Product product = new PO.Product()
            {
                ID = pfl.ID,
                NameOfBook = pfl.NameOfBook,
                AuthorName = productBO.AuthorName,
                Price = pfl.Price,
                Summary = productBO.Summary,
                Path = productBO.path,
                InStock = productBO.InStock,
                Category = (PO.CATEGORY)pfl.Category
 
           };

            return product;
        }

        internal static PO.productForList copyProductForListToPo(this BO.ProductForList pfl)
        {
            PO.productForList myNewPFL = new PO.productForList()
            {
                NameOfBook = pfl.NameOfBook,
                ID = pfl.ID,
                Price = pfl.Price,
                Category = (PO.CATEGORY)pfl.Category
            };

            return myNewPFL;
        }

        public static ObservableCollection<PO.productForList> ToObserCollection(this ObservableCollection<PO.productForList> allBooks)
        {
            foreach (BO.ProductForList Book in bl.BoProduct.GetProductDetails_forM())
                       allBooks.Add(Book.copyProductForListToPo());
           
            return allBooks;
        }
    }
}
