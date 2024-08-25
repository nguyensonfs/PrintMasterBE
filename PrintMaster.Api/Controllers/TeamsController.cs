using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Commons.Constants;

namespace PrintMaster.Api.Controllers
{
    [Route(DefaultValue.DEFAULT_CONTROLLER_ROUTE_WITHOUT_ACTION)]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamsController(ITeamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTeams(string? name)
        {
            try
            {
                var response = await _service.GetAllTeams(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{teamId}/users")]
        public async Task<ActionResult> GetAllUserByTeam(Guid teamId)
        {
            try
            {
                var response = await _service.GetAllUserByTeam(teamId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamById(Guid teamId)
        {
            var response = await _service.GetTeamById(teamId);
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> CreateTeam(Request_CreateTeam request)
        {
            var response = await _service.CreateTeam(request);
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

        [HttpPut("{teamId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTeam(Guid teamId, Request_UpdateTeam request)
        {
            var response = await _service.UpdateTeam(teamId, request);
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

        [HttpPatch("{teamId}/change-manager")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeTeamManager(Guid teamId, Guid managerId)
        {
            var response = await _service.ChangeManagerForTeam(teamId, managerId);
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

        [HttpDelete("{teamId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeam(Guid teamId)
        {
            var response = await (_service.DeleteTeam(teamId));
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
