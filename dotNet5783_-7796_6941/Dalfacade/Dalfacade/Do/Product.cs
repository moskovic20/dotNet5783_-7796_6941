namespace Do;

/// <summary>
/// structor for all kinds of structors in our book stor
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique identifier for item
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? nameOfBook { get; set; }
   
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
