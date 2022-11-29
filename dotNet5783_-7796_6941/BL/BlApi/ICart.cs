using BO;
using System.Runtime.InteropServices;

namespace BlApi;

public interface ICart
{
    Cart AddProductToCart(Cart cart, int IDOfProduct);//List Cart?
    Cart UpdateProductAmountInCart(Cart cart, int IDOfProduct, ImportedFromTypeLibAttribute NewSumOfProduct); 

    void MakingTheOrderFromCart(Cart cart);



}
