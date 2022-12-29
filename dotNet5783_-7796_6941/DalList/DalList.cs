using DalApi;

namespace Dal;

sealed internal class DalList : IDal
{

    private static DalList? instance;

    private static readonly object key = new(); //Thread Safe

    public static DalList? Instance
    {
        get
        {
            if (instance == null) //Lazy Initialization
            {
                lock (key)
                {
                    if (instance == null)
                        instance = new DalList();
                }
            }

            return instance;
        }
    }

    static DalList() { }
    private DalList() { }
    public IOrder Order { get; } = new DalOrder();
    public IProduct Product { get; } = new DalProduct();
    public IOrderItem OrderItem { get; } = new DalOrderItem();

}
