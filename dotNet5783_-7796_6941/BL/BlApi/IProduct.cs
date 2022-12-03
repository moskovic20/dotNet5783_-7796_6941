using BO;
using Do;

namespace BlApi;

public interface IProduct
{
    IEnumerable<BO.ProductForList> GetAllProductForList();
    void AddProduct(BO.Product product);
    BO.Product GetProductDetails_forM(int id);
    ProductItem GetProductDetails_forC(int id,Cart cart);
    void DeleteProductByID(int id);
    void UpdateProductDetails(BO.Product product);

}
