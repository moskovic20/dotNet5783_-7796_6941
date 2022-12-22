using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    ///  Unique identifier for item
    /// </summary>
    public string? NameOfBook { get; set; }

    /// <summary>
    /// The name of the author of the book
    /// </summary>
    public string? AuthorName { get; set; }

    /// <summary>
    /// for category of this book or other items 
    /// </summary>
    public CATEGORY Category { get; set; }

    /// <summary>
    /// book summary
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// price of this product
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// Number of books in stock
    /// </summary>
    public int? InStock { get; set; }

    /// <summary>
    /// for correct delitions in DalList classes
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Path for image to product
    /// </summary>
    public string? path { get; set; }


    public override string ToString()
    {
        return this.ToStringProperty();
    }


}