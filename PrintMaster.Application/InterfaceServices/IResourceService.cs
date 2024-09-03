using PrintMaster.Application.Handle.HandlePagination;
using PrintMaster.Application.Payloads.ResponseModels.DataResource;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IResourceService
    {
        Task<PageResult<DataResponseResource>> GetAllRosources(string? resourceName,int pageSize, int pageNumber);
    }
}
