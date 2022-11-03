namespace Do;
public struct OrderItems
{
    public int ID { get; set; }

    public int? IdOfOrder { get; set; }

    public int? IdOfProduct { get; set; }

    public float? priceOfOneItem { get; set; }

    public int? amountOfItem { get; set; }


    public override string ToString() => $@"
	   Ordered product ID : {ID}, 
	   Order ID (Customer and shipping details) : {IdOfOrder}
       Product ID : {IdOfProduct}
       Price of one item : {priceOfOneItem}
       the amount of items : {amountOfItem}
	";

}

