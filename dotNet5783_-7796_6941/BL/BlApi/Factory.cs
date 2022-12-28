using BlImplementation;

namespace BlApi;

public static class Factory
{

    public static IBl GetBl() => Bl.Instance;

}
