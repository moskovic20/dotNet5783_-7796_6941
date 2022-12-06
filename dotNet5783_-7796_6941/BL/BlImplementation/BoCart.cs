using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;


namespace BlImplementation;

internal class BoCart//: ICart
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        if (cart.Items == null) //הוספה ראשונה של מוצר לסל הכניות
            cart.Items = new();

        Do.Product myP = dal.Product.GetById(id);//הבאת פרטי מוצר

        BO.OrderItem? myOI = cart.Items.Find(x => x?.ID == id);//בודק האם המוצר כבר נמצא בסל הקניות


        if (myP.InStock! > 0)
            throw new BO.AddProductToCartProblemException("המוצר לא במלאי");


        if (myOI != null)//  המוצר כבר נמצא בסל הקניות
        {
            myOI.Amount++;
            myOI.TotalPrice += myOI.Price;
            cart.TotalPrice += myOI.Price;
            cart.Items.RemoveAll(x => x?.ID == id);
            cart.Items.Add(myOI);

        }
        else //המוצר לא נמצא בסל הקניות מלכתחילה
        {
            myOI = new();
            myP.CopyPropertiesTo(myOI);
            myOI.Price = myP.Price ?? throw new AddProductToCartProblemException("אין מחיר");
            myOI.TotalPrice = myOI.Price;
            cart.TotalPrice = myOI.Price;
            cart.Items.Add(myOI);
        }

        return cart;
    }

    BO.Cart UpdateProductAmountInCart(BO.Cart cart, int id, int NewAmount)
    {
        int difference = 0;

        if (cart.Items == null) //הוספה ראשונה של מוצר לסל הכניות
            cart.Items = new();

        Do.Product myP = dal.Product.GetById(id);//הבאת פרטי מוצר

        BO.OrderItem? myOI = cart.Items.Find(x => x?.ID == id);//מביא את המוצר מסל הקניות

        if (NewAmount < myOI?.Amount)
        {
            difference = myOI.Amount - NewAmount;

            myOI.Amount = NewAmount;
            myOI.TotalPrice -= difference * myOI.Price;
            cart.TotalPrice -= difference * myOI.TotalPrice;
        }
        else if (NewAmount > myOI?.Amount)
        {
            difference = NewAmount-myOI.Amount;

            if (myP.InStock < NewAmount)
                throw new AddProductToCartProblemException("אין במלאי");

            myOI.Amount = NewAmount;
            myOI.TotalPrice += difference * myOI.Price;
            cart.TotalPrice += difference * myOI.TotalPrice;
        }

        cart.Items.RemoveAll(x => x?.ID == id);

        if (difference != 0)
            cart.Items.Add(myOI);

        return cart;
    }


}
