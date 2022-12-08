using BO;
using Do;

namespace BlApi;

public interface IProduct
{
    IEnumerable<BO.ProductForList> GetAllProductForList();
    void AddProduct_forM(BO.Product product);
    BO.Product GetProductDetails_forM(int id);
    ProductItem GetProductDetails_forC(int id,Cart cart);
    void DeleteProductByID_forM(int id);
    void UpdateProductDetails_forM(BO.Product product);

}
