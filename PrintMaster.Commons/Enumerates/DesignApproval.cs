using System.Text.Json.Serialization;

namespace PrintMaster.Commons.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DesignApproval
    {
        Agree = 0,
        Refuse = 1
    }
}
