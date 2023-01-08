﻿using BlApi;
namespace BO;

public class ProductItem
{

    /// <summary>
    /// המזהה של המוצר   
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// שם המוצר-הספר
    /// </summary>
    public string? NameOfBook { get; set; }

    /// <summary>
    /// מחיר המוצר
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// הקטגוריה אליו משתייך המוצר
    /// </summary>
    public CATEGORY Category { get; set; }

    /// <summary>
    /// תקציר הספר
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// כמות המוצר בסל הקניות של הלקוח
    /// </summary>
    public int? AmountInCart { get; set; }

    /// <summary>
    /// האם המוצר במלאי
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// Path for image to product
    /// </summary>
    public string? ProductImagePath { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
