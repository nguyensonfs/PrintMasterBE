using PrintMaster.Commons.Enumerates;

namespace PrintMaster.Application.Payloads.RequestModels.InputRequests
{
    public class Request_InputDelivery
    {
        public Guid? ProjectId { get; set; }
        public Guid? DeliverId { get; set; }
        public Guid? CustomerId { get; set; }
        public DeliveryStatus? DeliveryStatus { get; set; }
    }
}
