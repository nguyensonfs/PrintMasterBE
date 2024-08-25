using PrintMaster.Commons.Enumerates;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrintMaster.Application.Payloads.RequestModels.UserRequests
{
    public class Request_Register
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateTime DateOfBirth { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required(ErrorMessage = "Gender is required")]
        public GenderEnum Gender { get; set; }
        [Required(ErrorMessage = "Team is required")]
        public Guid TeamId { get; set; }
    }
}
