namespace BlApi;

public interface IBl
{
    public ICart BoCart { get; }
    public IOrder BoOrder { get; }
    public IProduct BoProduct { get; }
}
