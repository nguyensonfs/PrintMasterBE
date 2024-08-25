using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.DeliveryRequests;

namespace PrintMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveriesController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPatch("{deliveryId}/confirm-assignment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ConfirmOrderAssignment(Guid deliveryId, [FromBody] Request_ShipperConfirmOrderDelivery request)
        {
            Guid shipperId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _deliveryService.ShipperConfirmOrderDelivery(shipperId, request);
            return StatusCode(result.Status, result);
        }

        // Xác nhận đơn hàng đã được giao
        [HttpPatch("{deliveryId}/confirm-delivery")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> ConfirmDelivery(Guid deliveryId, [FromForm] Request_ShipperConfirmDelivery request)
        {
            Guid shipperId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _deliveryService.ShipperConfirmDelivery(shipperId, request);
            return StatusCode(result.Status, result);
        }
    }
}
