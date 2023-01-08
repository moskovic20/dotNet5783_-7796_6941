using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalXml : IDal
{
    private static DalXml? instance;

    private static readonly object key = new(); //Thread Safe

    public static DalXml? Instance
    {
        get
        {
            if (instance == null) //Lazy Initialization
            {
                lock (key)
                {
                    if (instance == null)
                        instance = new DalXml();
                }
            }

            return instance;
        }
    }

    static DalXml() { }
    private DalXml() { }

    public IOrder Order { get; } = new Dal.Order();
    public IProduct Product { get; } = new Dal.Product();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}
