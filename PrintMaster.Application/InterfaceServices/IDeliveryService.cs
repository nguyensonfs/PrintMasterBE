using PrintMaster.Application.Payloads.RequestModels.DeliveryRequests;
using PrintMaster.Application.Payloads.RequestModels.SearchRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataDelivery;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IDeliveryService
    {
        Task<ResponseObject<DataResponseDelivery>> CreateDelivery(Request_CreateDelivery request);
        Task<ResponseObject<DataResponseDelivery>> ConfirmDeliveryTaskByShipper(Guid shipperId, Guid deliveryId);
        Task<ResponseObject<DataResponseDelivery>> ConfirmOrderDeliveryCompletionByShipper(Guid shipperId, Guid deliveryId, Request_ShipperConfirmDelivery request);
        Task<IQueryable<DataResponseDelivery>> GetAllDelivery(Request_SearchDelivery search);
        Task<DataResponseDelivery> GetDeliveryById(Guid id);
    }
}
