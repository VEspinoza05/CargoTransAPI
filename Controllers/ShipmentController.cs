using CargoTransAPI.Attributes;
using CargoTransAPI.DTOs;
using CargoTransAPI.Extensions;
using CargoTransAPI.Models;
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
        [FirebaseAuthorize("Administrador", "Encargado")]
        public async Task<IActionResult> GetAllShipments(string isOriginOrDestination = "")
        {
            var role = HttpContext.GetUserRole();
            List<ShipmentModel> shipments = new List<ShipmentModel>();

            if (role == "Encargado")
            {
                var city = HttpContext.GetUserBranchCity();

                if (isOriginOrDestination == "origin")
                {
                    shipments = await _shipmentRepository.GetAllShipmentsAsync(city, "originBranch");
                }
                else if (isOriginOrDestination == "destination")
                {
                    shipments = await _shipmentRepository.GetAllShipmentsAsync(city, "destinationBranch");
                }
                else
                {
                    return Ok(shipments);
                }
            }
            else
            {
                shipments = await _shipmentRepository.GetAllShipmentsAsync();
            }

            return Ok(shipments);
        }

        [HttpPost]
        [FirebaseAuthorize("Encargado")]
        public async Task<IActionResult> CreateShipment(NewShipmentDTO newShipmentDTO)
        {
            string city = HttpContext.GetUserBranchCity();
            string username = await HttpContext.GetUserDisplayNameAsync();

            ShipmentModel shipment = new ShipmentModel()
            {
                ShippingDate = newShipmentDTO.ShippingDate,
                OriginBranch = city,
                DestinationBranch = newShipmentDTO.DestinationBranch,
                State = "Enviado",
                CustomerName = newShipmentDTO.CustomerName,
                UserName = username,
            };

            await _shipmentRepository.CreateShipmentAsync(shipment);
            return Ok();
        }

        [HttpPut]
        [FirebaseAuthorize("Encargado")]
        public async Task<IActionResult> UpdateShipmentState(string shipmentId, string state)
        {
            await _shipmentRepository.UpdateShipmentStateAsync(shipmentId, state);
            return NoContent();
        }
    }
}