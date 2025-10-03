namespace CargoTransAPI.DTOs
{
    public class NewShipmentDTO
    {
        public DateTime ShippingDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string DestinationBranch { get; set; } = string.Empty;
    }
}