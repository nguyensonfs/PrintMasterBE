using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.RequestModels.ProjectRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataProject;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IProjectService
    {
        Task<ResponseObject<DataResponseProject>> CreateProject(Request_CreateProject request);
        Task<IQueryable<DataResponseProject>> GetAllProject(Request_InputProject? request);
    }
}
