using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class Product : IProduct
{

    public int Add(Do.Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Do.Product?> GetAll(Func<Do.Product?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Do.Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Do.Product GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public void Update(Do.Product item)
    {
        throw new NotImplementedException();
    }
}
