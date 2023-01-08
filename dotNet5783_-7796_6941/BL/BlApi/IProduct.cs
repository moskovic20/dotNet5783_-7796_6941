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
    public BO.Product GetProductByName(string name);
    ProductItem GetProductDetails_forC(int id, Cart cart);
    void DeleteProductByID_forM(int id);
    void UpdateProductDetails_forM(BO.Product product);
    ProductForList GetProductForList(int productId);
    IEnumerable<BO.ProductForList?> GetListedProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);
   // public IEnumerable<ProductForList> GetPartOfProduct(Predicate<ProductForList> filter);

    // IEnumerable<ProductForList> GetProductDetails_forM();
}
