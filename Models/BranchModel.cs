using Google.Cloud.Firestore;

namespace CargoTransAPI.Models
{
    [FirestoreData]
    public class BranchModel
    {
        public string BranchId { get; set; }

        [FirestoreProperty("name")]
        public string Name { get; set; }

        [FirestoreProperty("city")]
        public string City { get; set; }

        [FirestoreProperty("address")]
        public string Address { get; set; }

        [FirestoreProperty("phone")]
        public string Phone { get; set; }

        [FirestoreProperty("email")]
        public string Email { get; set; }
    }
}