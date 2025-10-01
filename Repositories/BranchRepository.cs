using Google.Cloud.Firestore;
using CargoTransAPI.Models;

namespace CargoTransAPI.Repositories
{
    [FirestoreData]
    public class BranchRepository
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _branchesCollection;

        public BranchRepository(FirestoreDb db)
        {
            _db = db;
            _branchesCollection = _db.Collection("branch");
        }

        public async Task<BranchModel?> GetBranchAsync(string id)
        {
            var doc = await _branchesCollection.Document(id).GetSnapshotAsync();
            var branch = doc.Exists ? doc.ConvertTo<BranchModel>() : null;
            if (branch != null)
            {
                branch.BranchId = id;
            }
            return branch;
        }
    }
}