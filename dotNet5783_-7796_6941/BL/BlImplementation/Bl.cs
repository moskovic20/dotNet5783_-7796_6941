using BlApi;
using BO;

namespace BlImplementation;

sealed public class Bl : IBl
{

    public static IBl Instance { get; } = new Bl();

    private Bl() { }

    public IOrder BoOrder { set; get; } = new BoOrder();

    public IProduct BoProduct { set; get; } = new BoProduct();

    public ICart Cart { set; get; } = new BoCart();
}
