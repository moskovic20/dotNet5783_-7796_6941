

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
    public int ID { get; set; }

    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? NameCustomer { get; set; }

    /// <summary>
    /// Email addrres of the customer
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The address to which the order should be sent
    /// </summary>
    public string? ShippingAddress { get; set; }

    /// <summary>
    /// The date the order was created
    /// </summary>
    public DateTime? DateOrder { get; set; }

    /// <summary>
    /// The date the shipment was launched.
    /// התאריך שהמשלוח יצא לדרך
    /// </summary>
    public DateTime? ShippingDate { get; set; }

    /// <summary>
    /// The date the shipment arrived at its destination
    /// תאריך הגעת המשלוח ליעד
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Has the order been deleted?
    /// האם ההזמנה נמחקה ממאגר הרשימות
    /// </summary>
    public bool? IsDeleted { get; set; }


    public override string ToString() => $@"
	Order ID : {ID}, 
	Name Customer : {NameCustomer}
    Customer email : {Email}
    Send the order to: {ShippingAddress}
    The order was made on : {DateOrder}
    The shipment started on: {ShippingDate}
    The shipment reached its destination on : {DeliveryDate}
	";

}
