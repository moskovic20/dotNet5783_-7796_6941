using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public DO.CATEGORY? Category { get; set; }

    /// <summary>
    /// כמות המלאי של המוצר
    /// </summary>
    public int? InStock { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
