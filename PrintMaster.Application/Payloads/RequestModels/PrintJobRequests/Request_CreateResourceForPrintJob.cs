namespace PrintMaster.Application.Payloads.RequestModels.PrintJobRequests
{
    public class Request_CreateResourceForPrintJob
    {
        public Guid ResourcePropertyDetailId { get; set; }
        public int Quantity { get; set; }
    }
}
