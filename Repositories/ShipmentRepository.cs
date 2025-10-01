using CargoTransAPI.Models;
using Google.Cloud.Firestore;

namespace CargoTransAPI.Repositories
{
    public class ShipmentRepository
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _shipmentsCollection;

        public ShipmentRepository(FirestoreDb db)
        {
            _db = db;
            _shipmentsCollection = _db.Collection("shipment");
        }

        public async Task<List<ShipmentModel>> GetAllShipmentsAsync()
        {
            var snapshot = await _shipmentsCollection.GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var shipment = d.ConvertTo<ShipmentModel>();
                shipment.ShipmentId = d.Id;
                return shipment;
            })
            .ToList();
        }
    }
}