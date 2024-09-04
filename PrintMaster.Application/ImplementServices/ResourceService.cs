using PrintMaster.Application.Handle.HandlePagination;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.ResponseModels.DataResource;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class ResourceService : IResourceService
    {
        private readonly IBaseRepository<Resource> _baseResourceRepository;
        private readonly ResourceConverter _converter;

        public ResourceService(IBaseRepository<Resource> baseResourceRepository, ResourceConverter converter)
        {
            _baseResourceRepository = baseResourceRepository;
            _converter = converter;
        }

        public async Task<IQueryable<DataResponseResource>> GetAllResources(string? resourceName)
        {
            var query = await _baseResourceRepository.GetAllAsync(record => record.IsDeleted == false);
            if (!string.IsNullOrEmpty(resourceName))
            {
                query = query.Where(x => x.ResourceName.ToLower().Contains(resourceName.ToLower()));
            }
            var result = query.Select(x => _converter.EntityToDTO(x));
            return result;
        }
    }
}
