using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.TeamRequests
{
    public class Request_CreateTeam
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        public Guid ManagerId { get; set; }
    }
}
