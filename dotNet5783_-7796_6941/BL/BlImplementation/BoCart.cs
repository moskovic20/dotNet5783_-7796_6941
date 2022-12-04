using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class BoCart//: ICart
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        Do.Product myP = dal.Product.GetById(id)?? throw new ArgumentNullException("missing product");

        if (cart.Items == null)
            return cart;////לתקן!!!

        bool isExist=cart.Items.Exists(x => x != null && x.ID == id);
        if(isExist)
        {

        }
    }

}
