using DalApi;
using Do;

namespace Dal;

public class DalProduct : IProduct
{
    //public static DataSource s_instance { get; } = new DataSource();

    public int Add(Product P)
    {
        Random random = new Random();
        int indexOfMyP = DataSource._Products.FindIndex(x => x.GetValueOrDefault().ID == P.ID);

        if (indexOfMyP == -1) //this product is not found in the data
        {
            while (!IdIsFound(P.ID)) //check if this id is not taken.
            {
                P.ID = random.Next(100000, 999999999);
            }

            DataSource._Products.Add(P);
            return P.ID;
        }

        if (DataSource._Products[indexOfMyP].GetValueOrDefault().IsDeleted == false)
            throw new Exception("The product you wish to add is already exists");

        DataSource._Products.Add(P);
        return P.ID;

    }

    public void Delete(int id)
    {
        int getIdOfProduct = DataSource._Products.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (getIdOfProduct > 0)
        {
            if (DataSource._Products[getIdOfProduct].GetValueOrDefault().IsDeleted == true)
                throw new Exception("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = DataSource._Products[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                DataSource._Products[getIdOfProduct] = (Product?)ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new Exception("the product doesn't exist");
    }

    public IEnumerable<Product> GetAll()
    {
        if (DataSource._Products.FirstOrDefault() == null)
            throw new Exception("there is not any product");

        IEnumerable<Product?> allProduct = DataSource._Products.FindAll(x => true);
        return (IEnumerable<Product>)allProduct;
    }

    public Product GetById(int id)
    {
        Product? ProductById = DataSource._Products.Find(x => x.GetValueOrDefault().ID == id
                                                          && x.GetValueOrDefault().IsDeleted == false);

        if (ProductById == null)
            throw new Exception("the product is not found");///ok?

        return (Product)ProductById;
    }

    public void Update(Product item)
    {
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new Exception("the product can't be update because he doesn't exist");
        }
        Delete(item.ID);
        Add(item);
    }

    private bool IdIsFound(int myID)
    {
        int indexOfSameId = DataSource._Products.FindIndex(x => x.GetValueOrDefault().ID == myID);

        if (indexOfSameId == -1)
            return true;

        else
            return false;
    }
}
