 namespace Do;

public struct OrderItems
{
    
    public int ID { get; set; }

    public int? IDOfOrdet { get; set; }
    public int? IDOfProduct { get; set; }
    
    public float? priceOfOneItem { get; set; }

    public int? amountOfItem { get; set; }
}

