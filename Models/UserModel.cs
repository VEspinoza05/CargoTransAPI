using Google.Cloud.Firestore;

namespace CargoTransAPI.Models
{
    [FirestoreData]
    public class UserModel
    {
        public string UserId { get; set; }

        [FirestoreProperty("name")]
        public string Name { get; set; }

        [FirestoreProperty("email")]
        public string Email { get; set; }

        [FirestoreProperty("password")]
        public string Password { get; set; }

        [FirestoreProperty("role")]
        public string Role { get; set; } = string.Empty;

        [FirestoreProperty("branchId")]
        public string BranchId { get; set; }    
    }   
}