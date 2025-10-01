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

        public async Task<List<ShipmentModel>> GetAllShipmentsAsync()
        {
            var snapshot = await _shipmentsCollection.GetSnapshotAsync();
            var shipments = snapshot.Documents.Select(d =>
            {
                var shipment = d.ConvertTo<ShipmentModel>();
                shipment.ShipmentId = d.Id;
                return shipment;
            })
            .ToList();

            foreach (var shipment in shipments)
            {
                shipment.UserData = await _userRepository.GetUserAsync(shipment.UserId);
            }

            return shipments;
        }
    }
}