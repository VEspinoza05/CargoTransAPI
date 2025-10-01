using CargoTransAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CargoTransAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentRepository _shipmentRepository;

        public ShipmentController(ShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var shipments = await _shipmentRepository.GetAllShipmentsAsync();
            return Ok(shipments);
        }
    }
}