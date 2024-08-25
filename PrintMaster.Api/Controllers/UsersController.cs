using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Commons.Constants;

namespace PrintMaster.Api.Controllers
{
    [Route(DefaultValue.DEFAULT_CONTROLLER_ROUTE_WITHOUT_ACTION)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] Request_InputUser request)
        {
            try
            {
                return Ok(await _userService.GetAllUsers(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetRolesByUserId(Guid userId)
        {
            try
            {
                return Ok(await _userService.GetRolesByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("managers")]
        public async Task<IActionResult> GetAllUserContainsManagerRole()
        {
            try
            {
                var response = await _userService.GetAllUserContainsManagerRole();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("leaders")]
        public async Task<IActionResult> GetAllUserContainsLeaderRole()
        {
            try
            {
                var response = await _userService.GetAllUserContainsLeaderRole();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var response = await _userService.GetAllEmployee();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{userId}/change-department")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeDepartmentForUser(Guid userId, Request_ChangeDepartmentForUser request)
        {
            var response = await _userService.ChangeDepartmentForUser(userId, request);
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
