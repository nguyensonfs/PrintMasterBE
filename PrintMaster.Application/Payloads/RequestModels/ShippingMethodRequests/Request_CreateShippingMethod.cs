using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.ShippingMethodRequests
{
    public class Request_CreateShippingMethod
    {
        [Required(ErrorMessage = "ShippingMethodName is required")]
        public string ShippingMethodName { get; set; } = string.Empty;
    }
}
