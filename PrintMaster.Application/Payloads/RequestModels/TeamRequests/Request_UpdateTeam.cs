namespace PrintMaster.Application.Payloads.RequestModels.TeamRequests
{
    public class Request_UpdateTeam
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ManagerId { get; set; }
    }
}
