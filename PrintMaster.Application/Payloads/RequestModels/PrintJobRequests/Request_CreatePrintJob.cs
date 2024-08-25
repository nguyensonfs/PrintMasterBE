namespace PrintMaster.Application.Payloads.RequestModels.PrintJobRequests
{
    public class Request_CreatePrintJob
    {
        public Guid DesignId { get; set; }
        public List<Request_CreateResourceForPrintJob>? ResourceForPrints { get; set; }
    }
}
