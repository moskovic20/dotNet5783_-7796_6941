using DalApi;
using Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    private static DalList? instance;
    private static readonly object key = new();

    public static DalList GetInstance()
    {
        if (instance == null)
        {
            lock (key)
            {
                if (instance == null)
                    instance = new DalList();
            }
        }

        return instance;
    }

   // public static IDal Instance { get; } = new DalList();

    private DalList() { }
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();

}
