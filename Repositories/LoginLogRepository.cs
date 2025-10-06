using CargoTransAPI.Models;
using Google.Cloud.Firestore;

namespace CargoTransAPI.Repositories
{
    public class LoginLogRepository
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _loginLogsCollection;

        public LoginLogRepository(FirestoreDb db)
        {
            _db = db;
            _loginLogsCollection = _db.Collection("loginLog");
        }

        public async Task<List<LoginLogModel>> GetLoginLogsAsync()
        {
            var snapshot = await _loginLogsCollection.GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var loginLog = d.ConvertTo<LoginLogModel>();
                loginLog.LoginLogId = d.Id;
                return loginLog;
            })
            .ToList();
        }

        public async Task CreateLoginLogAsync(LoginLogModel loginLog)
        {
            DocumentReference docRef = _loginLogsCollection.Document();
            await docRef.SetAsync(loginLog);
        }
    }
}