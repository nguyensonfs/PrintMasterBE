using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.UserRequests
{
    public class Request_Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
