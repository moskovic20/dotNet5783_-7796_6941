using Do;

namespace DalApi;
public interface IProduct : ICrud<Product>
{
    new int Add(Product item);
    new Product GetById(int id);
    new void Update(Product item);
    new void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    new IEnumerable <Product> GetAll();
}
