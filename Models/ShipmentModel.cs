public class ShipmentModel
{
    public string ShipmentId { get; set; }
    public DateTime ShippingDate { get; set; }
    public string OriginBranch { get; set; }
    public string DestinyBranch { get; set; }
    public ShippingStateEnum State { get; set; }
    public string CustomerName { get; set; }
    public string UserId { get; set; }
}