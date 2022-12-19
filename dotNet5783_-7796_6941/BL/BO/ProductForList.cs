using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
