using System.Text.Json.Serialization;

namespace PrintMaster.Application.Payloads.RequestModels.DeliveryRequests
{
    public class Request_ShipperConfirmDelivery
    {
        public Guid DeliveryId { get; set; }
    }
}
