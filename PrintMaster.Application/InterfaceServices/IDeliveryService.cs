using PrintMaster.Application.Payloads.RequestModels.DeliveryRequests;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataDelivery;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IDeliveryService
    {
        Task<ResponseObject<DataResponseDelivery>> CreateDelivery(Request_CreateDelivery request);
        Task<ResponseObject<DataResponseDelivery>> ShipperConfirmOrderDelivery(Guid shipperId, Request_ShipperConfirmOrderDelivery request);
        Task<ResponseObject<DataResponseDelivery>> ShipperConfirmDelivery(Guid shipperId, Request_ShipperConfirmDelivery request);
        Task<IQueryable<DataResponseDelivery>> GetAllDelivery(Request_InputDelivery input);
        Task<DataResponseDelivery> GetDeliveryById(Guid id);
    }
}
