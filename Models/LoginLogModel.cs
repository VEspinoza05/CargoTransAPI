using Google.Cloud.Firestore;

namespace CargoTransAPI.Models
{
    [FirestoreData]
    public class LoginLogModel
    {
        public string LoginLogId { get; set; } = string.Empty;

        [FirestoreProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        [FirestoreProperty("username")]
        public string Username { get; set; } = string.Empty;

        [FirestoreProperty("userEmail")]
        public string UserEmail { get; set; } = string.Empty;

        [FirestoreProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [FirestoreProperty("role")]
        public string Role { get; set; }  = string.Empty;

        [FirestoreProperty("branchCity")]
        public string BranchCity { get; set; }  = string.Empty;
    }
}