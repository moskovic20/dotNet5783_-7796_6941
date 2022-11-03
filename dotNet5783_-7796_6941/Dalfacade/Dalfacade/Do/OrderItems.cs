
﻿ namespace Do;
/// <summary>
/// structor for ditales of this item to relate the products to his ordet
/// </summary>
public struct OrderItems
{
    /// <summary>
    /// unique number to this item in this current order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// ID for the Order to connect all his product to the same costumer's order
    /// </summary>
    public int? IdOfOrder { get; set; }
    /// <summary>
    /// ID of the product in store
    /// </summary>
    public int? IdOfProduct { get; set; }
    /// <summary>
    /// price of this current item on store
    /// </summary>
    public double ? priceOfOneItem { get; set; }
    /// <summary>
    /// number of this book in the order
    /// </summary>

    public int? amountOfItem { get; set; }

    public override string ToString() => $@"
	    Ordered product ID : {ID}, 
	    Order ID (Customer and shipping details) : {IdOfOrder}
        Product ID : {IdOfProduct}
        Price of one item : {priceOfOneItem}
        the amount of items : {amountOfItem}
	";

}

