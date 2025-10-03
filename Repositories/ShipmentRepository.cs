using CargoTransAPI.DTOs;
using CargoTransAPI.Models;
using Google.Cloud.Firestore;

namespace CargoTransAPI.Repositories
{
    public class ShipmentRepository
    {
        private readonly FirestoreDb _db;
        private readonly UserRepository _userRepository;
        private readonly CollectionReference _shipmentsCollection;

        public ShipmentRepository(FirestoreDb db, UserRepository userRepository)
        {
            _db = db;
            _userRepository = userRepository;
            _shipmentsCollection = _db.Collection("shipment");
        }

        public async Task<List<ShipmentModel>> GetAllShipmentsAsync(string? city = null, string? isOriginOrDestination = null)
        {
            QuerySnapshot snapshot;

            if (isOriginOrDestination != null && city != null)
            {
                Query query = _shipmentsCollection.WhereEqualTo(isOriginOrDestination, city);
                snapshot = await query.GetSnapshotAsync();
            }
            else
            {
                snapshot = await _shipmentsCollection.GetSnapshotAsync();
            }

            var shipments = snapshot.Documents.Select(d =>
            {
                var shipment = d.ConvertTo<ShipmentModel>();
                shipment.ShipmentId = d.Id;
                return shipment;
            })
            .ToList();

            return shipments;
        }

        public async Task CreateShipmentAsync(ShipmentModel shipment)
        {
            DocumentReference docRef = _shipmentsCollection.Document();
            await docRef.SetAsync(shipment);
        }
    }
}