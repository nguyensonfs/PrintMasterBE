using PrintMaster.Application.Payloads.RequestModels.DesignRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataDesign;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IDesignService
    {
        Task<ResponseObject<DataResponseDesign>> CreateDesign(Guid designerId, Guid ProjectId, Request_CreateDesign request);
        Task<ResponseObject<DataResponseDesign>> UpdateDesign(Guid designerId, Guid ProjectId, Guid DesignId, Request_UpdateDesign request);
        Task<ResponseMessages> ApprovalDesign( Guid ProjectId, Guid DesignId, Request_DesignApproval request);
    }
}
