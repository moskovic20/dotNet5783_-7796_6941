using DalApi;
using Do;

namespace Dal;

internal class DalProduct : IProduct
{

    DataSource _DS = DataSource.Instance!;

    /// <summary>
    /// הפונקציה מקבלת מוצר ומוסיפה אותו למערכת
    /// </summary>
    /// <param name="P"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Add(Product P)
    {
        List<Product?> listOfThisID = _DS._Products.FindAll(x => x?.ID == P.ID);
        int indexOfExist = listOfThisID.FindIndex(x => x?.IsDeleted != true);

        if (indexOfExist == -1)//
        {
            _DS._Products.Add(P);
            return P.ID;
        }
        else
            throw new AlreadyExistException("מוצר זה כבר קיים במערכת");

    }

    /// <summary>
    /// הפונקציה מקבלת מספר מזהה של מוצר ומוחקת את המוצר
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Delete(int id)
    {
        int getIdOfProduct = _DS._Products.FindIndex(x => x?.ID == id && x?.IsDeleted != true);

        if (getIdOfProduct != -1)
        {
            if (_DS._Products[getIdOfProduct]?.IsDeleted == true)
                throw new DoesntExistException("המוצר כבר נחמק ");
            else
            {
                Product ChangingStatusOfProductIsdeleted = _DS._Products[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                ChangingStatusOfProductIsdeleted.ProductImagePath = null;
                _DS._Products[getIdOfProduct] = ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new DoesntExistException("המוצר לא נמצא");
    }

    /// <summary>
    /// הפונקציה מקבלת מספר מזהה של מוצר ומחזירה אמוצר זה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistException"></exception>
    public Product GetById(int id)
    {
        Product? ProductById = _DS._Products.FirstOrDefault(x => x?.ID == id && x?.IsDeleted == false);

        return ProductById ?? throw new DoesntExistException("המוצר לא נמצא");
    }

    /// <summary>
    /// הפונקציה מקבלת מוצר ומעדכת את פרטי המוצר לפי המוצר שהתקבל
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DoesntExistException"></exception>
    public void Update(Product item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new DoesntExistException("המוצר לא קיים");
        }
        Delete(item.ID);
        Add(item);
    }

    /// <summary>
    /// הפונקציה מחזירה את כל רשימת המוצרים לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    => from item in _DS._Products
       where item != null
       where item?.IsDeleted == false
       where (filter != null) ? filter(item) : true
       select item;

    /// <summary>
    ///  הפונקציה מחזירה את כל רשימת הפריטים (כולל אלו שנמחקו)-לפי פונקציית הסינון שמתקבלת
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAlldeleted(Func<Do.Product?, bool>? filter = null)
    => from item in _DS._Products
       where item != null
       where (filter != null) ? filter(item) : true
       where item?.IsDeleted == true
       select item;
}
