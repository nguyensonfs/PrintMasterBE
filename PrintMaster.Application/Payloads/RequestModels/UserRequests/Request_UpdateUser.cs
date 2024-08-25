using Microsoft.AspNetCore.Http;
using PrintMaster.Commons.Enumerates;

namespace PrintMaster.Application.Payloads.RequestModels.UserRequests
{
    public class Request_UpdateUser
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public IFormFile? Avatar { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
