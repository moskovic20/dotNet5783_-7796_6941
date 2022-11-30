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
    public static IDal Instance { get; } = new DalList();
    private IOrder Order => new DalOrder();
    private IProduct Product => new DalProduct();
    private IOrderItem OrderItem => new DalOrderItem();


}
