using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Dal;

public struct configNumbers
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}

sealed class DalXml : IDal   //sealed?
{
    #region singelton

    //private static DalXml? instance;
    //private static readonly object key = new(); //Thread Safe
    //public static DalXml? Instance
    //{
    //    get
    //    {
    //        if (instance == null) //Lazy Initialization
    //        {
    //            lock (key)
    //            {
    //                if (instance == null)
    //                    instance = new DalXml();
    //            }
    //        }

    //        return instance;
    //    }

    //}
    //static DalXml() { }
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    #endregion

    #region ממשקים לשימוש
    public IOrder Order { get; } = new Dal.Order();
    public IProduct Product { get; } = new Dal.Product();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    #endregion

    //#region DS xml file
    //internal string OrderPath = @"Order.xml"; // path to name of file..
    //internal string OrderItemPath = @"OrderItem.xml";
    //internal string ProductPath = @"Product.xml";
    //internal string configPath = @"config.xml";
    //#endregion
    
}
