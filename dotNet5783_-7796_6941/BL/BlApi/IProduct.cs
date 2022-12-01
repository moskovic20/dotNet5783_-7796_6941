using BO;
using Do;

namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList> GetAllProductForList();
    BO.Product GetProductByID(int id);
    void AddProduct(BO.Product product);
    void GetProductDetails_forM(int id);
    ProductItem GetProductDetails_forC(int id,Cart cart);
    void UpdateProductDetails(BO.Product product);

}
