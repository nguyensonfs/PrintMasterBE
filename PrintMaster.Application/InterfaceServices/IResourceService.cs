using PrintMaster.Application.Payloads.ResponseModels.DataResource;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IResourceService
    {
        Task<IQueryable<DataResponseResource>> GetAllResources(string? resourceName);
    }
}
