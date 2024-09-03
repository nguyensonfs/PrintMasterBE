using PrintMaster.Application.Payloads.ResponseModels.DataResource;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class ResourcePropertyConverter
    {
        private readonly ResourcePropertyDetailConverter _converter;
        private readonly IBaseRepository<ResourcePropertyDetail> _repository;
        public ResourcePropertyConverter(ResourcePropertyDetailConverter converter,
                                         IBaseRepository<ResourcePropertyDetail> repository)
        {
            _converter = converter;
            _repository = repository;
        }
        public DataResponseResourceProperty EntityToDTO(ResourceProperty resourceProperty)
        {
            return new DataResponseResourceProperty
            {
                Id = resourceProperty.Id,
                Quantity = resourceProperty.Quantity,
                ResourcePropertyName = resourceProperty.ResourcePropertyName,
                ResourcePropertyDetails = _repository.GetAllAsync(x => x.ResourcePropertyId == resourceProperty.Id
                && x.IsDeleted == false).Result
                .Select(x => _converter.EntityToDTO(x))
            };
        }
    }
}
