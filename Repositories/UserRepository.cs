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

        public async Task<UserModel?> GetUserAsync(string id)
        {
            var doc = await _usersCollection.Document(id).GetSnapshotAsync();
            var user = doc.Exists ? doc.ConvertTo<UserModel>() : null;
            if (user != null)
            {
                user.UserId = id;
            }
            return user;
        }

        public async Task<UserModel?> CreateUserAsync(UserModel user)
        {
            DocumentReference docRef = _usersCollection.Document();
            await docRef.SetAsync(user);
            string newDocId = docRef.Id;
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<UserModel>();
            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            DocumentReference docRef = _usersCollection.Document(user.UserId);
            await docRef.SetAsync(user, SetOptions.Overwrite);
            return user;
        }
    }
}