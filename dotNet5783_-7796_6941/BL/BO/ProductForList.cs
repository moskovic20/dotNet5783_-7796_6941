using BlApi;
namespace BO;

public class ProductForList
{
    /// <summary>
    /// מזהה מוצר
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// שם המוצר
    /// </summary>
    public string? NameOfBook { get; set; }

    /// <summary>
    /// מחיר מוצר
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// קטגוריית המוצר- סוג הג'אנר
    /// </summary>
    public CATEGORY Category { get; set; }

    /// <summary>
    /// כמות המלאי של המוצר
    /// </summary>
    public int? InStock { get; set; }

    /// <summary>
    /// Path for image to product
    /// </summary>
    public string? ProductImagePath { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
