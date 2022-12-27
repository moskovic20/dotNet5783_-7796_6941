using BO;


namespace BlApi;

public interface IProduct
{
    IEnumerable<BO.ProductForList> GetAllProductForList_forM();
    IEnumerable<BO.ProductForList> GetAllProductForList_forC();
    int AddProduct_forM(BO.Product productToAdd);
    BO.Product GetProductDetails_forM(int id);
    ProductItem GetProductDetails_forC(int id,Cart cart);
    void DeleteProductByID_forM(int id);
    void UpdateProductDetails_forM(BO.Product product);

    public IEnumerable<BO.ProductForList?> GetListedProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);
    
    // IEnumerable<ProductForList> GetProductDetails_forM();
}
