using BlApi;

namespace BO;

public class Product
{
    /// <summary>
    /// מזהה המוצר
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    ///  שם המוצר
    /// </summary>
    public string? NameOfBook { get; set; }

    /// <summary>
    /// שם הסופר של הספר
    /// </summary>
    public string? AuthorName { get; set; }

    /// <summary>
    /// מחיר הספר
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// קטגוריית המוצר
    /// </summary>
    public BO.CATEGORY Category { get; set; }

    /// <summary>
    /// תקציר הספר
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Path for image to product
    /// </summary>
    public string? ProductImagePath { get; set; }

    /// <summary>
    /// כמות המלאי של המוצר
    /// </summary>
    public int? InStock { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
