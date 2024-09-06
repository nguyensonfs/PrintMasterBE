using PrintMaster.Commons.Enumerates;
using System.Text.Json.Serialization;

namespace PrintMaster.Application.Payloads.RequestModels.DeliveryRequests
{
    public class Request_ShipperConfirmDelivery
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeliveryStatus Status { get; set; }
    }
}
