using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.UserRequests
{
    public class Request_ChangePassword
    {
        [Required(ErrorMessage = "OldPassword is required")]
        public string OldPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
