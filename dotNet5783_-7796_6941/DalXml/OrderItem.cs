using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class OrderItem : IOrderItem
{
    public int Add(Do.OrderItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Do.OrderItem?> GetAll(Func<Do.OrderItem?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Do.OrderItem GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Do.OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Do.OrderItem?> GetListByOrderID(int OrderID)
    {
        throw new NotImplementedException();
    }

    public void Update(Do.OrderItem item)
    {
        throw new NotImplementedException();
    }
}
