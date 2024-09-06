using PrintMaster.Application.Payloads.RequestModels.ShippingMethodRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataShippingMethod;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IShippingMethodService
    {
        Task<ResponseObject<DataResponseShippingMethod>> CreateShippingMethod(Request_CreateShippingMethod request);
        Task<IQueryable<DataResponseShippingMethod>> GetAllShippingMethod();
    }
}
