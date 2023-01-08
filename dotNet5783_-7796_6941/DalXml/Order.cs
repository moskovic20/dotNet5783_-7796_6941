using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class Order : IOrder
{
    public int Add(Do.Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Do.Order?> GetAll(Func<Do.Order?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Do.Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Do.Order item)
    {
        throw new NotImplementedException();
    }
}
