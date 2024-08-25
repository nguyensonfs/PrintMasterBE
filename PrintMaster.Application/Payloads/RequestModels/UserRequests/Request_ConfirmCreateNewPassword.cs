using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.UserRequests
{
    public class Request_ConfirmCreateNewPassword
    {
        [Required(ErrorMessage = "ConfirmCode is required")]
        public string ConfirmCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
