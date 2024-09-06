using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.DeliveryRequests;
using PrintMaster.Application.Payloads.RequestModels.SearchRequests;

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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllDeliveries([FromQuery] Request_SearchDelivery request)
        {
            var result = await _deliveryService.GetAllDelivery(request);
            return Ok(result);
        }

        [HttpGet("{deliveryId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDeliveryById(Guid deliveryId)
        {
            var result = await _deliveryService.GetDeliveryById(deliveryId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePrintJob([FromBody] Request_CreateDelivery request)
        {
            var response = await _deliveryService.CreateDelivery(request);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else if (response.Status == StatusCodes.Status500InternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        // Xác nhận nhiệm vụ giao hàng
        [HttpPatch("{deliveryId}/confirm-assignment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ConfirmOrderAssignment(Guid deliveryId)
        {
            Guid shipperId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _deliveryService.ConfirmDeliveryTaskByShipper(shipperId, deliveryId);
            return StatusCode(result.Status, result);
        }

        // Xác nhận đơn hàng đã được giao
        [HttpPatch("{deliveryId}/confirm-delivery")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ConfirmDeliveryCompletion(Guid deliveryId, Request_ShipperConfirmDelivery request)
        {
            Guid shipperId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _deliveryService.ConfirmOrderDeliveryCompletionByShipper(shipperId, deliveryId, request);
            return StatusCode(result.Status, result);
        }
    }
}
