using BlApi;
using BO;

namespace BlImplementation;

internal class BoProduct : IProduct
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    /// <summary>
    /// הפונקציה מחזירה את כל רשימת הפריטים בחנות למנהל
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.GetAllForList_Exception"></exception>
    public IEnumerable<BO.ProductForList> GetAllProductForList_forM()
    {
        var products = dal.Product.GetAll();

        if (products.Count() == 0)
            throw new BO.GetAllForList_Exception("There are no products");

        return products.CopyListTo<Do.Product?, BO.ProductForList>();
    }

    /// <summary>
    /// הפונקציה מחזירה את רשמת המוצרים בחנות ללקוח- רק מוצרים שהוזן עבורם מחיר
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.GetAllForList_Exception"></exception>
    public IEnumerable<BO.ProductForList> GetAllProductForList_forC()
    {//לסנן את מחיר
        var products = dal.Product.GetAll((Do.Product? p) => { return p?.Price == null ? false : true; }); //סינון המוצרים שלא הוזן עבורם מחיר

        if (products.Count() == 0)
            throw new BO.GetAllForList_Exception("There are no products");

        return products.CopyListTo<Do.Product?, BO.ProductForList>();
    }

    public BO.Product GetProductDetails_forM(int id)
    {
        if (id < 0) throw new BO.InvalidValue_Exception("can't get this product because OrderID is negetive");

        try
        {
            Do.Product? myP = dal.Product.GetById(id); //הבאת המוצר מבשכבת הנתונים
            BO.Product BoMyP = new();
            BoMyP = myP.CopyPropTo(BoMyP);//העתקת הנתונים החופפים לישות הנתונים של מוצר בשכבת הלוגיקה
            return BoMyP;
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("cant give details of this product", ex);
        }
    }

    public BO.ProductItem GetProductDetails_forC(int id, BO.Cart cart)
    {
        if (id < 0) throw new BO.InvalidValue_Exception("can't get this product because OrderID is negetive");

        try
        {
            Do.Product myP = dal.Product.GetById(id);// הבאת המוצר הרצוי משכבת הנתונים
            if (myP.Price == null) throw new BO.InvalidValue_Exception("cant give this product because no price has been entered for it yet");//חריגה שלא אמורה לקרות, כרגע היא בשביל הבדיקה-כי לא ניתן ללקוח לבחור מוצרים שלא הוזן עבורם מחיר

            BO.ProductItem pForClient = new();
            pForClient = myP.CopyPropTo(pForClient);

            pForClient.InStock = (myP.InStock > 0) ? true : false;// מילוי השדה- האם קיים במלאי

            if (cart.Items == null)
            {
                pForClient.AmountInCart = 0;
            }
            else
            {
                BO.OrderItem? temp = cart.Items.FirstOrDefault(x => x.ProductID == id);//חיפוש הפריט בסל הקניות
                pForClient.AmountInCart = temp?.AmountOfItems ?? 0;//הכנסת כמות הפריטים
            }

            return pForClient;

        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.GetDetails_Exception("Can't get this product", ex);
        }
    }

    public ProductForList GetProductForList(int productId)
    {
        return dal.Product.GetById(productId).CopyPropTo(new ProductForList());
    }

    public int AddProduct_forM(BO.Product productToAdd)
    {
        int id = 0;

        try
        {
            if (productToAdd == null)
                throw new ArgumentNullException("missing product to add");

            if (productToAdd.ID < 0)
                throw new BO.Adding_Exception("מזהה הספר לא יכול להיות שלילי");

            if (productToAdd.ID < 100000)
                throw new BO.Adding_Exception("מזהה הספר חייב להיות בעל 6 ספרות לפחות");

            if (productToAdd.NameOfBook == null)
                throw new BO.Adding_Exception("חובה להזין את שם הספר");

            if (productToAdd.AuthorName == null)
                throw new BO.Adding_Exception("חובה להזין את שם הסופר");

            if (productToAdd.Price < 0)
                throw new BO.Adding_Exception("מחיר אינו יכול להיות שלילי");

            if (productToAdd.InStock < 0)
                throw new BO.Adding_Exception("כמות במלאי אינה יכולה להיות שלילית");

            Do.Product myNewP = new();
            myNewP = productToAdd.CopyPropToStruct(myNewP);//העברה לישות לוגית מוצר מסוג Do

            id = dal.Product.Add(myNewP);
            return id;
        }
        catch (Do.AlreadyExistException ex)
        {
            throw new BO.Adding_Exception("Can't add this product", ex);
        }
    }

    public void DeleteProductByID_forM(int id)
    {
        try
        {
            dal.Product.Delete(id);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.Deleted_Exception("Can't deleted this product", ex);
        }
    }

    public void UpdateProductDetails_forM(BO.Product productToUp)
    {
        if (productToUp == null)
            throw new ArgumentNullException("missing product");

        if (productToUp.ID < 100000)
            throw new BO.Update_Exception("cant gets Negative OrderID");//מספר שלילי

        if (productToUp.NameOfBook == "")
            throw new BO.Update_Exception("Name of book is missing");

        if (productToUp.Price < 0)
            throw new BO.Update_Exception("cant gets Negative price");

        if (productToUp.InStock < 0)
            throw new BO.Update_Exception("cant gets Negative amount");

        Do.Product DoProductToUp = new();
        DoProductToUp = productToUp.CopyPropToStruct(DoProductToUp);//העברה לישות נתונים- "מוצר" מסוג שכבת הנתונים

        try
        {
            dal.Product.Update(DoProductToUp);
        }
        catch (Do.DoesntExistException ex)
        {
            throw new BO.Update_Exception("Can't update product", ex);
        }
    }


    //______________________________________________________פונקציה של נורית עבור סינון בתצוגה

    public IEnumerable<BO.ProductForList?> GetListedProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null)

    {

        IEnumerable<Do.Product?> doProductList =

        enumFilter switch

        {

            BO.Filters.filterBYCategory =>

                //DO.Category categ = filterValue != null ? (DO.Category)filterValue : DO.Category.all;

                dal!.Product.GetAll(dp => dp?.Category == (filterValue != null ? (DO.CATEGORY)filterValue : DO.CATEGORY.all)),

            BO.Filters.filterBYName =>

                dal!.Product.GetAll(dp => dp?.NameOfBook == (string?)filterValue),

            BO.Filters.filterBYBiggerThanPrice =>

                dal!.Product.GetAll(dp => dp?.Price >= (double?)filterValue),

            BO.Filters.filterBYSmallerThanPrice =>

                dal!.Product.GetAll(dp => dp?.Price <= (double?)filterValue),

            BO.Filters.None =>

                dal!.Product.GetAll(),

            _ => dal!.Product.GetAll(),

        };

        return (from Do.Product doProduct in doProductList
                select doProduct.CopyPropTo(new BO.ProductForList())).ToList();
    }


}
