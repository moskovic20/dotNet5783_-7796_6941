namespace BlApi;

public interface IBl
{
    public ICart Cart { get; }
    public IOrder BoOrder { get; }
    public IProduct BoProduct { get; }
}
