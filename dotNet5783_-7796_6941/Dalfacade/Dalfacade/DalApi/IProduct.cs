﻿using Do;

namespace DalApi;
public interface IProduct : ICrud<Product>
{
    int Add(Product item);
    Product GetById(int id);
    void Update(Product item);
    void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable <Product> GetAll();
}
