using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.ProjectRequests
{
    public class Request_CreateProject
    {
        [Required(ErrorMessage = "ProjectName is required")]
        public string ProjectName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "RequestDescriptionFromCustomer is required")]
        public string RequestDescriptionFromCustomer { get; set; } = string.Empty;
        [Required]
        public Guid LeaderId { get; set; }
        [Required]
        public DateTime ExpectedEndDate { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CommissionPercentage { get; set; }
        public IFormFile ImageDescription { get; set; }
    }
}
