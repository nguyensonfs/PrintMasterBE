using PrintMaster.Application.Payloads.ResponseModels.DataNotification;

namespace PrintMaster.Application.Payloads.ResponseModels.DataUser
{
    public class DataResponseUser : DataResponseBase
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string? TeamName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateTime { get; set; }
        public IQueryable<DataResponseNotification>? Notifications { get; set; }
    }
}
