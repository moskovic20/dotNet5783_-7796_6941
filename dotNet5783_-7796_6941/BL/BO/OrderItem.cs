using BlApi;
namespace BO;


public class OrderItem
{
    /// <summary>
    /// מזהה פריט ההזמנה
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// מזהה מוצר
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// שם המוצר
    /// </summary>
    public string? NameOfBook { get; set; }

    /// <summary>
    ///מחיר של יחידת מוצר 1 
    /// </summary>
    public double? PriceOfOneItem { get; set; }

    /// <summary>
    /// כמות הפריטים של מוצר זה בהזמנה
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// מחיר כולל של הפריט
    /// </summary>
    public double? TotalPrice { get; set; }


    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
