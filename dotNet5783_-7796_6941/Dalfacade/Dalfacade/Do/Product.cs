namespace Do;
/// <summary>
/// structor for all kinds of products in our book store
/// </summary>
public struct Product
{
    /// <summary>
    /// uniqe name of the current book
    /// </summary>
    public string? nameOfProduct { get; set; }

    /// <summary>
    /// ID of this product
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// for category of this book or other items 
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// price of this product
    /// </summary>
    public float? Price { get; set; }

    /// <summary>
    /// amount of this product on shop
    /// </summary>
    public int? Amount{get; set;}
}
