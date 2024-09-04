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
        public async Task<IActionResult> GetAllRosources(string? resourceName)
        {
            try
            {
                var response = await _resourceService.GetAllResources(resourceName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
