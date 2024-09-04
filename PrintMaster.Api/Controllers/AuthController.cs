using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.UserRequests;
using PrintMaster.Commons.Constants;

namespace PrintMaster.Api.Controllers
{
    [Route(DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService
                             )
        {
            _authService = authService;

        }

        [HttpPost]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] Request_Register request)
        {
            var response = await _authService.Register(request);
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
        public async Task<IActionResult> ActivateAccount(string confirmCode)
        {
            var response = await _authService.ConfirmRegister(confirmCode);
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
        public async Task<IActionResult> Login([FromBody] Request_Login request)
        {
            var response = await _authService.Login(request);
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

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_ChangePassword request)
        {
            var userClaim = HttpContext.User.FindFirst("Id");
            if (userClaim == null)
            {
                return BadRequest("User ID not found in token.");
            }
            Guid id;
            bool parseResult = Guid.TryParse(userClaim.Value, out id);
            if (!parseResult)
            {
                return BadRequest("Invalid User ID.");
            }
            return Ok(await _authService.ChangePassword(id, request));
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            var response = await _authService.ForgotPassword(email);
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

        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromBody] Request_ConfirmCreateNewPassword request)
        {
            var response = await _authService.ConfirmCreateNewPassword(request);
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
