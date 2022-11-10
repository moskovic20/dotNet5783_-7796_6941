using DalApi;
using Do;

namespace Dal;

public class DalProduct : IProduct
{
    //public static DataSource s_instance { get; } = new DataSource();

    public int Add(Product item)
    {
        if(true)/////////
        {

        }
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        int getIdOfProduct;
        getIdOfProduct= DataSource._Product.FindIndex(x => x.GetValueOrDefault().ID == id);

        if (getIdOfProduct > 0)
        {
            if (DataSource._Product[getIdOfProduct].GetValueOrDefault().IsDeleted == true)
                throw new Exception("the product doesn't exist");
            else
            {
                Product ChangingStatusOfProductIsdeleted = DataSource._Product[getIdOfProduct].GetValueOrDefault();
                ChangingStatusOfProductIsdeleted.IsDeleted = true;
                DataSource._Product[getIdOfProduct] = (Product?) ChangingStatusOfProductIsdeleted;
            }
        }
        else
            throw new Exception("the product doesn't exist");
    }

    public IEnumerable<Product> GetAll()
    {
        IEnumerable<Product?> allProduct = DataSource._Product.FindAll(x => true);
            return (IEnumerable<Product>)allProduct;
    }

    public Product GetById(int id)
    {
        Product? ProductById = DataSource._Product.Find(x=> x.GetValueOrDefault().ID==id);
        if(ProductById==null)
            throw new Exception( "the product is not found");///ok?
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
}
