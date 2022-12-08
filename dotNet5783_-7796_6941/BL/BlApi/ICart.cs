

namespace BlApi;

public interface ICart
{
    BO.Cart AddProductToCart(BO.Cart cart, int IdOfProduct);

    BO.Cart UpdateProductAmountInCart(BO.Cart cart, int IDOfProduct, int NewSumOfProduct);

    public int MakeOrder(BO.Cart cart);
}
