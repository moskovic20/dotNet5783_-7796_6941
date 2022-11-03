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
    /// The name of this current book
    /// </summary>
    public string? nameOfBook { get; set; }
   
    /// <summary>
    /// for category of this book or other items 
    /// </summary>
    public Enum? Category { get; set; }

    /// <summary>
    /// price of this product
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// Number of books in stock
    /// </summary>
    public double? InStock{get; set;}

    public override string ToString() => $@"

        Product ID: {ID } 
        Book title: {nameOfBook}
        Category: {Category}
        Price: {Price}
        Number of books in stock: {InStock}

"
    ;
        
}

