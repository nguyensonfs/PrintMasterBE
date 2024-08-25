namespace PrintMaster.Application.Payloads.RequestModels.InputRequests
{
    public class Request_InputProject
    {
        public string? ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? LeaderId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
