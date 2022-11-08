using DalApi;
using Do;

namespace Dal;

internal class DalProduct : IProduct
{
    internal static DataSource s_instance { get; } = new DataSource();

    public int Add(Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product item)
    {
        throw new NotImplementedException();
    }
}
