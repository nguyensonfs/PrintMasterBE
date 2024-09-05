using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.PrintJobRequests;

namespace PrintMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintJobsController : ControllerBase
    {
        private readonly IPrintJobService _jobService;

        public PrintJobsController(IPrintJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePrintJob([FromBody] Request_CreatePrintJob request)
        {
            var response = await _jobService.CreatePrintJob(request);
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


    }
}
