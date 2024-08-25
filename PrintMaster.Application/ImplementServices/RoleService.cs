using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.ResponseModels.DataRole;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<Role> _baseRoleRepository;

        public RoleService(IBaseRepository<Role> baseRoleRepository)
        {
            _baseRoleRepository = baseRoleRepository;
        }

        public async Task<IQueryable<DataResponseRole>> GetAllRoles()
        {
            var query = await _baseRoleRepository.GetAllAsync(x => x.IsDeleted == false);
            return query.Select(x => new DataResponseRole
            {
                Id = x.Id,
                RoleCode = x.RoleCode,
                RoleName = x.RoleName
            });
        }
    }
}
