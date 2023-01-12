using BO;
using DalApi;


namespace BlApi;

public interface IProduct
{
    IEnumerable<BO.ProductForList> GetAllProductForList_forM();
    IEnumerable<BO.ProductForList> GetAllProductForList_forC();
    IEnumerable<BO.ProductItem> GetAllProductItems_forC();
    int AddProduct_forM(BO.Product productToAdd);
    BO.Product GetProductDetails_forM(int id);
    public IEnumerable<ProductForList> GetProductsByName(string name);
    ProductItem GetProductDetails_forC(int id, Cart cart);
    void DeleteProductByID_forM(int id);
    void UpdateProductDetails_forM(BO.Product product);
    ProductForList GetProductForList(int productId);
    public IEnumerable<ProductForList> GetAllProductByNumber(int number);


}
