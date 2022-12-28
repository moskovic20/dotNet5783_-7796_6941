using BlApi;
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
                ProductImagePath = prodPO.ProductImagePath,
                Category = (BO.CATEGORY)prodPO.Category!
            };
            return copyProduct;
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
                return PO.CATEGORY.childen;
            if (hebrewCategory == Hebrew_CATEGORY.רומן)
                return PO.CATEGORY.romans;
            if (hebrewCategory == Hebrew_CATEGORY.בישול_ואפייה)
                return PO.CATEGORY.cookingAndBaking;
            if (hebrewCategory == Hebrew_CATEGORY.פסיכולוגיה)
                return PO.CATEGORY.psychology;

            return PO.CATEGORY.Kodesh;
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
            if (myCategory == PO.CATEGORY.childen)
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
                ProductImagePath = productBO.ProductImagePath,
                InStock = productBO.InStock,
                Category = (PO.CATEGORY)pfl.Category

            };

            return product;
        }

        internal static PO.ProductForList copyProductForListToPo(this BO.ProductForList pfl)
        {
            PO.ProductForList myNewPFL = new PO.ProductForList()
            {
                NameOfBook = pfl.NameOfBook,
                ID = pfl.ID,
                Price = pfl.Price,
                Category = ((PO.CATEGORY)pfl.Category).EnglishToHebewCategory()
            };

            return myNewPFL;
        }

        internal static PO.ProductForList copyProductToPFL(this PO.Product p)
        {
            PO.ProductForList myNewPFL = new PO.ProductForList()
            {
                NameOfBook = p.NameOfBook,
                ID = p.ID,
                Price = p.Price,
                Category = ((PO.CATEGORY)p.Category!).EnglishToHebewCategory()
            };

            return myNewPFL;
        }

        internal static PO.Product copyProductToPo(this BO.Product pfl)
        {

            PO.Product product = new PO.Product()
            {
                ID = pfl.ID,
                NameOfBook = pfl.NameOfBook,
                AuthorName = pfl.AuthorName,
                Price = pfl.Price,
                Summary = pfl.Summary,
                ProductImagePath = pfl.ProductImagePath,
                InStock = pfl.InStock,
                Category = (PO.CATEGORY)pfl.Category

            };

            return product;
        }

        public static ObservableCollection<PO.ProductForList> ToObserCollection(this ObservableCollection<PO.ProductForList> allBooks)
        {
            foreach (BO.ProductForList Book in bl.BoProduct.GetAllProductForList_forM())
                allBooks.Add(Book.copyProductForListToPo());

            return allBooks;
        }
    }
}
