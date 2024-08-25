namespace PrintMaster.Application.Payloads.RequestModels.DeliveryRequests
{
    public class Request_CreateDelivery
    {
        public Guid ShippingMethodId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid DeliverId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime EstimateDeliveryTime { get; set; }
    }
}
