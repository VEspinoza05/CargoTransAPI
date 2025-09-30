using Google.Cloud.Firestore;
using CargoTransAPI.Models;

namespace CargoTransAPI.Repositories
{
    public class UserRepository
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _usersCollection;

        public UserRepository(FirestoreDb db)
        {
            _db = db;
            _usersCollection = _db.Collection("user");
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var snapshot = await _usersCollection.GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var user = d.ConvertTo<UserModel>();
                user.UserId = d.Id;
                return user;
            })
            .ToList();
        }
    }
}