using BlApi;

namespace BlImplementation;

internal class BoCart : ICart
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");
    //DalApi.IDal? dal = DalApi.Factory.Get();לפי דן!


    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        try
        {

            cart.Items = cart.Items ?? new();

            Do.Product product = dal.Product.GetById(productID);//הבאת פרטי מוצר

            BO.OrderItem item = cart.Items.FirstOrDefault(x => x?.ProductID == productID) //אם המוצר קיים בסל הקניות אז הוא מקבל אותו אם לא יוצר חדש
            ?? new()
            {
                ID = 0,
                ProductID = productID,
                NameOfBook = product.NameOfBook,
                PriceOfOneItem = product.Price ?? throw new BO.Adding_Exception("cant add this product to the cart because No price has been entered for it yet"),//חריגה שלא אמורה לקרות
                AmountOfItems = 0,
                TotalPrice = 0,
            };

            if (item.AmountOfItems >= product.InStock)
                throw new BO.InvalidValue_Exception("The desired quantity for the book is not in stock:" + item.NameOfBook);

            if (item.AmountOfItems == 0) cart.Items.Add(item);//הוספה ראשונה של מוצר זה לסל הקניות

            item.AmountOfItems++;
            item.TotalPrice += item.PriceOfOneItem;

            cart.TotalPrice = cart.TotalPrice == null ? 0 : cart.TotalPrice;
            cart.TotalPrice += item.PriceOfOneItem;

            return cart;
        }
        catch (Do.DoesntExistException ex) { throw new BO.Adding_Exception("cant add this product to the cart", ex); }
    }

    public BO.Cart UpdateProductAmountInCart(BO.Cart cart, int productID, int NewAmount)
    {
        if (NewAmount < 0) throw new BO.InvalidValue_Exception("can't update negetive amount");

        try
        {
            int difference = 0;
            cart.Items = cart.Items ?? new();

            Do.Product product = dal.Product.GetById(productID);//הבאת פרטי מוצר

            BO.OrderItem item = cart.Items.Find(x => x.ProductID == productID) ?? throw new Exception("There is no product to update - an exception that should not happen");

            if (NewAmount == 0)//הסרת מוצר זה מסל הקניות
            {
                cart.Items.Remove(item);
                cart.TotalPrice -= item.TotalPrice;
            }

            else if (NewAmount < item.AmountOfItems)//הקטנת כמות מוצר בסל הקניות
            {
                difference = item.AmountOfItems - NewAmount;

                item.AmountOfItems = NewAmount;
                item.TotalPrice -= difference * item.PriceOfOneItem;
                cart.TotalPrice -= difference * item.PriceOfOneItem;
                return cart;
            }

            else //הגדלת כמות מוצר בסל הקניות
            {
                difference = NewAmount - item.AmountOfItems;

                if (product.InStock < NewAmount)
                    throw new BO.InvalidValue_Exception("The desired quantity for the book isnt in stock: " + item.NameOfBook);

                item.AmountOfItems = NewAmount;
                item.TotalPrice += difference * item.PriceOfOneItem;
                cart.TotalPrice += difference * item.PriceOfOneItem;
            }

            return cart;
        }
        catch (BO.InvalidValue_Exception ex) { throw new BO.Update_Exception("can't update the amount of this product", ex); }
        catch (Do.DoesntExistException ex) { throw new BO.Update_Exception("can't update the amount of this product", ex); }
    }

    public int MakeOrder(BO.Cart cart)
    {
        try
        {
            cart = cart ?? throw new ArgumentNullException("There is no shopping basket - an exception that should not happen");
            cart.Items = cart.Items ?? throw new ArgumentNullException("There are no items in the shopping basket - an exception that should not happen");

            if (cart.CustomerName == null)
                throw new BO.InvalidValue_Exception("cant make this order without customer Name");

            if (cart.CustomerAddress == null)
                throw new BO.InvalidValue_Exception("cant make this order without Ccustomer Address");

            if (cart.CustomerEmail == null) throw new BO.InvalidValue_Exception("cant make this order without email");
            else if (!cart.CustomerEmail.IsValidEmail())
                throw new BO.InvalidValue_Exception("Invalid email address");

            cart.Items.TrueForAll(x => x.ValidationChecks());//בדיקה שכל כמויות הפריטים בסל הקניות חיוביים וכן שיש מספיק במלאי 

            Do.Order newOrder = new(); //יצירת הזמנה חדשה
            newOrder = cart.CopyPropToStruct(newOrder);
            newOrder.DateOrder = DateTime.Now;

            int IdOfNewOrder = dal.Order.Add(newOrder);

          //אמור לעבוד במקום foreach.. var afterCastingAndUpdat = cart.Items.Select(oi => oi.ListFromBoToDo(IdOfNewOrder));//casting orderItems from Bo to Do

            foreach (BO.OrderItem item in cart.Items)//הכנסת המוצרים בסל למוצרים בהזמנה ועדכון פרטי המוצרים
            {
                Do.Product product = dal.Product.GetById(item.ProductID); //הבאת פרטי מוצר

                Do.OrderItem orderItem = new();
                orderItem = item.CopyPropToStruct(orderItem);
                orderItem.OrderID = IdOfNewOrder;

                dal.OrderItem.Add(orderItem);

                Do.Product newProduct = new();//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
                newProduct = product.CopyPropToStruct(newProduct);
                newProduct.InStock -= item.AmountOfItems;

                dal.Product.Update(newProduct);//מעדכנים את הכמות של המוצר ברשימה
            }
            return IdOfNewOrder;
        }
        catch (Do.AlreadyExistException ex) { throw new BO.MakeOrder_Exception("cant create this cart", ex); }
        catch (Do.DoesntExistException ex) { throw new BO.MakeOrder_Exception("cant create this cart", ex); }
    }
}


