namespace Do;

/// <summary>
/// Structure for order that contains details about the customer
/// and the items in the order.
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique identifier for order
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the customer
    /// </summary>
    public string ? NameCustomer { get; set; }

    /// <summary>
    /// Email addrres of the customer
    /// </summary>
    public string ? Email { get; set; }

    /// <summary>
    /// The address to which the order should be sent
    /// </summary>
    public string ? ShippingAddress { get; set; }

    /// <summary>
    /// The date the order was created
    /// </summary>
    public string ? DateOrder { get; set; }

    /// <summary>
    /// The date the shipment was launched.
    /// התאריך שהמשלוח יצא לדרך
    /// </summary>
    public string ? ShippingDate { get; set; }

    /// <summary>
    /// The date the shipment arrived at its destination
    /// תאריך הגעת המשלוח ליעד
    /// </summary>
    public string ? DeliveryDate { get; set; }
}
