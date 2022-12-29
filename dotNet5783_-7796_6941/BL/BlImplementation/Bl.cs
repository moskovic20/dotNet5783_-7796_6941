using BlApi;
//using BO;

namespace BlImplementation;

sealed internal class Bl : IBl//from public to internul...ok?
{

    private static IBl? instance;
    private static readonly object key = new(); //Thread Safe

    internal static IBl Instance
    {
        get
        {
            if (instance == null) //Lazy Initialization
            {
                lock (key)
                {
                    if (instance == null)
                        instance = new Bl();
                }
            }

            return instance;
        }
    }

    internal Bl() { }

    public IOrder BoOrder { get; } = new BoOrder();

    public IProduct BoProduct { get; } = new BoProduct();

    public ICart Cart { get; } = new BoCart();
}
