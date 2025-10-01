using Google.Cloud.Firestore;

namespace CargoTransAPI.Models
{
    [FirestoreData]
    public class ShipmentModel
    {
        public string ShipmentId { get; set; } = string.Empty;

        [FirestoreProperty("shippingDate")]
        public DateTime ShippingDate { get; set; }

        [FirestoreProperty("originBranchId")]
        public string OriginBranchId { get; set; } = string.Empty;

        [FirestoreProperty("destinationBranchId")]
        public string DestinationBranchId { get; set; } = string.Empty;

        [FirestoreProperty("state")]
        public string State { get; set; } = string.Empty;

        [FirestoreProperty("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [FirestoreProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        public UserModel? UserData { get; set; }
    }
}