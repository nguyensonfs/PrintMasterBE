using PrintMaster.Commons.Enumerates;
using System.Text.Json.Serialization;

namespace PrintMaster.Application.Payloads.RequestModels.DesignRequests
{
    public class Request_DesignApproval
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DesignApproval DesignApproval { get; set; }
    }
}
