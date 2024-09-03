using PrintMaster.Application.Payloads.ResponseModels.DataResource;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class ResourceConverter
    {
        private readonly IBaseRepository<ResourceProperty> _reposiroty;
        private readonly ResourcePropertyConverter _propertyConverter;
        private readonly IBaseRepository<ResourceType> _resourceTypeRepository;
        public ResourceConverter(IBaseRepository<ResourceProperty> reposiroty,
                                 ResourcePropertyConverter propertyConverter,
                                 IBaseRepository<ResourceType> resourceTypeRepository)
        {
            _reposiroty = reposiroty;
            _propertyConverter = propertyConverter;
            _resourceTypeRepository = resourceTypeRepository;
        }
        public DataResponseResource EntityToDTO(Resource entity)
        {
            return new DataResponseResource
            {
                AvailableQuantity = entity.AvailableQuantity,
                Id = entity.Id,
                Image = entity.Image,
                ResourceName = entity.ResourceName,
                ResourceStatus = entity.Status.ToString(),
                ResourceProperties = _reposiroty.GetAllAsync(x => x.ResourceId
                == entity.Id).Result.Select(x => _propertyConverter.EntityToDTO(x)),
                ResourceTypeName = _resourceTypeRepository.GetByIDAsync(entity.ResourceTypeId).Result.NameOfResourceType
            };
        }
    }
}
