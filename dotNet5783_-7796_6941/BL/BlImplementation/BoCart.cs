using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class BoCart : ICart
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");
    
    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        try
        {
            cart.Items = cart.Items ?? new();

            Do.Product product = dal.Product.GetById(productID);//הבאת פרטי מוצר

            BO.OrderItem item = cart.Items.FirstOrDefault(x => x?.ProductID == productID)
            ?? new()
            {
                ID = 0,
                ProductID = productID,
                NameOfBook = product.NameOfBook,
                PriceOfOneItem = product.Price ?? throw new BO.Adding_Exception("cant add this product to the cart because No price has been entered for it yet"),
                AmountOfItems = 0,
                TotalPrice = 0,
            };

            if (product.InStock! >= 0)
                throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:" + item.NameOfBook);

            item.AmountOfItems++;
            item.TotalPrice += item.PriceOfOneItem;

            cart.Items.Add(item);
            return cart;
        }
        catch (Do.DoesntExistException ex) { throw new BO.Adding_Exception("cant add this product to the cart", ex); }
        catch (BO.InvalidValue_Exception ex) { throw new BO.Adding_Exception("cant add this product to the cart", ex); }
        catch (Exception ex) { throw new BO.Adding_Exception("cant add this product to the cart", ex); }


    }

    public BO.Cart UpdateProductAmountInCart(BO.Cart cart, int productID, int NewAmount)
    {
        try
        {
            int difference = 0;
            cart.Items = cart.Items ?? new();

            Do.Product product = dal.Product.GetById(productID);//הבאת פרטי מוצר

            BO.OrderItem item = cart.Items.Find(x => x.ProductID == productID) ?? throw new Exception("אין מוצר לעדכן");

            if (NewAmount == 0)
            {
                cart.Items.Remove(item);
                cart.TotalPrice -= item.TotalPrice;
            }

            else if (NewAmount < item.AmountOfItems)
            {
                difference = item.AmountOfItems - NewAmount;

                item.AmountOfItems = NewAmount;
                item.TotalPrice -= difference * item.PriceOfOneItem;
                cart.TotalPrice -= difference * item.PriceOfOneItem;
                return cart;
            }

            else
            {
                difference = NewAmount - item.AmountOfItems;

                if (product.InStock < NewAmount)
                    throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:"+item.NameOfBook);

                item.AmountOfItems = NewAmount;
                item.TotalPrice += difference * item.PriceOfOneItem;
                cart.TotalPrice += difference * item.PriceOfOneItem;
            }

            return cart;
        }
        catch(BO.InvalidValue_Exception ex) { throw new BO.Update_Exception("can't update the amount of this product",ex); }
        catch (Do.DoesntExistException ex) { throw new BO.Update_Exception("can't update the amount of this product", ex); }
    }

    public int MakeOrder(BO.Cart cart)
    {
        try
        {
            cart = cart ?? throw new ArgumentNullException("");//לתקן
            cart.Items = cart.Items ?? throw new Exception("אין מוצרים בסל הקניות");//לתקן

            if (cart.CustomerName == null)
                throw new BO.InvalidValue_Exception("No value entered for the field: Customer Name");

            if (cart.CustomerAddress == null)
                throw new BO.InvalidValue_Exception("No value entered for the field: Customer Address");

            if (cart.CustomerEmail != null)
            {
                if (!cart.CustomerEmail.IsValidEmail())
                    throw new BO.InvalidValue_Exception("Invalid email address");
            }

            cart.Items.TrueForAll(x => ValidationChecks(x));

            Do.Order newOrder = new(); //יצירת הזמנה חדשה
            cart.CopyPropertiesTo(newOrder);
            newOrder.DateOrder = DateTime.Now;

            int IdOfONewOrder = dal.Order.Add(newOrder);

            foreach (BO.OrderItem item in cart.Items)
            {
                Do.Product product = dal.Product.GetById(item.ProductID);

                Do.OrderItem orderItem = new()
                {
                    IdOfOrder = IdOfONewOrder,
                    IdOfProduct = item.ProductID,
                    AmountOfItem = item.AmountOfItems,
                    PriceOfOneItem = item.PriceOfOneItem,
                };

                dal.OrderItem.Add(orderItem);

                Do.Product newProduct = new();//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
                product.CopyPropertiesTo(newProduct);
                newProduct.InStock -= item.AmountOfItems;

                dal.Product.Update(newProduct);//מעדכנים את הכמות של המוצר ברשימה
            }
            return IdOfONewOrder;
        }
        catch (BO.InvalidValue_Exception ex) { throw new BO.MakeOrder_Exception("cant create this cart",ex); }
        catch (Do.AlreadyExistException ex) { throw new BO.MakeOrder_Exception("cant create this cart", ex); }
        catch (Do.DoesntExistException ex) { throw new BO.MakeOrder_Exception("cant create this cart", ex); }
        catch(Exception ex) { throw new BO.MakeOrder_Exception("cant create this cart", ex); }
    }

    private bool ValidationChecks(BO.OrderItem item)
    {
        Do.Product product = dal.Product.GetById(item.ProductID);

        if (item.AmountOfItems < 1)
            throw new BO.InvalidValue_Exception("the amount of the book:"+ item.NameOfBook+" is negative");

        if (product.InStock < item.AmountOfItems)
            throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:"+item.NameOfBook);

        return true; ;
    }

}
