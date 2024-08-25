using PrintMaster.Application.Payloads.ResponseModels.DataRole;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IRoleService
    {
        Task<IQueryable<DataResponseRole>> GetAllRoles();
    }
}
