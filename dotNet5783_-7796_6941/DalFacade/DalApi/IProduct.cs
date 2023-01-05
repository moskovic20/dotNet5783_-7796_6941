using Do;

namespace DalApi;
public interface IProduct : ICrud<Product>
{
    public Product GetByName(string name);
}
