using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.PO
{
    static class Tools
    {
        private static IBl bl = BlApi.Factory.GetBl();

        #region convert from PO.Product to BO.Product
        internal static BO.Product CopyProductToBO(this PO.Product prodPO)
        {
            BO.Product copyProduct = new ()
            { 
                ID = prodPO.ID,
                NameOfBook=prodPO.NameOfBook,
                AuthorName=prodPO.AuthorName,
                Summary=prodPO.Summary,
                Price=prodPO.Price,
                InStock=prodPO.InStock,
                path=prodPO.Path,
                Category=(BO.CATEGORY)prodPO.Category,
            };
            return copyProduct;
        }
        #endregion

        #region convert from Category to string (Hebrew) and from string(Hebrew) to Category.

        public static PO.CATEGORY stringToCategory(string hebrewCategory)
        {
            if (hebrewCategory == "מסתורין")
                return PO.CATEGORY.mystery;
            if (hebrewCategory == "פנטזיה")
                return PO.CATEGORY.fantasy;
            if (hebrewCategory == "היסטוריה")
                return PO.CATEGORY.history;
            if (hebrewCategory == "מדע")
                return PO.CATEGORY.scinence;
            if (hebrewCategory == "ילדים")
                return PO.CATEGORY.childen;
            if (hebrewCategory == "רומן")
                return PO.CATEGORY.romans;
            if (hebrewCategory == "בישול ואפייה")
                return PO.CATEGORY.cookingAndBaking;
            if (hebrewCategory == "פסיכולוגיה")
                return PO.CATEGORY.psychology;
            if (hebrewCategory == "קודש")
                return PO.CATEGORY.Kodesh;

            throw new Exception();
        }

        public static string CategoryToString(this PO.CATEGORY myCategory)
        {
            if (myCategory == PO.CATEGORY.mystery)
                return "מסתורין";
            if (myCategory == PO.CATEGORY.fantasy)
                return "פנטזיה";
            if (myCategory == PO.CATEGORY.history)
                return "היסטוריה";
            if (myCategory == PO.CATEGORY.scinence)
                return "מדע";
            if (myCategory == PO.CATEGORY.childen)
                return "ילדים";
            if (myCategory == PO.CATEGORY.romans)
                return "רומן";
            if (myCategory == PO.CATEGORY.cookingAndBaking)
                return "בישול ואפייה";
            if (myCategory == PO.CATEGORY.psychology)
                return "פסיכולוגיה";
            if (myCategory == PO.CATEGORY.Kodesh)
                return "קודש";

            throw new Exception();
        }

        #endregion

        internal static PO.Product copyProductForListToPO(this BO.ProductForList pfl)
        {
            BO.Product productBO = bl.BoProduct.GetProductDetails_forM(pfl.ID);

           PO.Product product = new PO.Product()
           {
              ID= pfl.ID,
              NameOfBook= pfl.NameOfBook,
              AuthorName=productBO.AuthorName,
              Price=pfl.Price,
              Summary=productBO.Summary,
              Path=productBO.path,
              InStock=productBO.InStock,
              Category=(PO.CATEGORY)pfl.Category

           };

            return product;
        }
    }
}
