namespace PrintMaster.Application.Payloads.RequestModels.InputRequests
{
    public class Request_InputUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? TeamId { get; set; }
    }
}
