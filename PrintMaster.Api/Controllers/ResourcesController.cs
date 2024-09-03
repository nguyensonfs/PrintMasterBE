using Microsoft.AspNetCore.Mvc;
using PrintMaster.Application.InterfaceServices;

namespace PrintMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(string? resourceName, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                var response = await _resourceService.GetAllRosources(resourceName, pageSize, pageNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
