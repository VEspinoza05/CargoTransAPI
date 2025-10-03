using Google.Cloud.Firestore;

namespace CargoTransAPI.Models
{
    [FirestoreData]
    public class ShipmentModel
    {
        public string ShipmentId { get; set; } = string.Empty;

        [FirestoreProperty("shippingDate")]
        public DateTime ShippingDate { get; set; }

        [FirestoreProperty("originBranch")]
        public string OriginBranch { get; set; } = string.Empty;

        [FirestoreProperty("destinationBranch")]
        public string DestinationBranch { get; set; } = string.Empty;

        [FirestoreProperty("state")]
        public string State { get; set; } = string.Empty;

        [FirestoreProperty("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [FirestoreProperty("username")]
        public string UserName { get; set; } = string.Empty;
    }
}