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


sealed class DalXml : IDal  
{
    #region singelton

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
    //public static IDal Instance { get; } = new DalXml();
    private DalXml() {

        #region אתחול ראשוני למספר רץ
        //אתחול של המספרים הרצים לקובץ..configNumbers order = new()
        //{
        //    numberSaved = 100020,
        //    typeOfnumber = "Num For Order ID"
        //};
        //configNumbers orderItem = new()
        //{
        //    numberSaved = 100052,
        //    typeOfnumber = "Num For OrderItem ID"
        //};
        //List<configNumbers?> helpListCharge = new()
        // {
        //    order,
        //    orderItem
        // };
        //XMLTools.SaveListToXMLSerializer<configNumbers>(helpListCharge, "config");
        #endregion
    }

    #endregion


    #region ממשקים לשימוש
    public IOrder Order { get; } = new Dal.DalOrder();
    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
    #endregion
}
