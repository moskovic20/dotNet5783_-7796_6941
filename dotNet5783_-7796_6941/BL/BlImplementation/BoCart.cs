using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using DalApi;
using BO;
using System.Runtime.InteropServices;

namespace BlImplementation;

internal class BoCart: ICart
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    Cart AddProductToCart(Cart cart, int IDOfProduct) { return new Cart(); }
    Cart UpdateProductAmountInCart(Cart cart, int IDOfProduct, ImportedFromTypeLibAttribute NewSumOfProduct) { return new Cart(); }

    void MakingTheOrderFromCart(Cart cart) { }

}
