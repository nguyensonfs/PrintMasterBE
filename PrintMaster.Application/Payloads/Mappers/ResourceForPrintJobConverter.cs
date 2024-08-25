using PrintMaster.Application.Payloads.ResponseModels.DataShippingMethod;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class ResourceForPrintJobConverter
    {
        private readonly IBaseRepository<ResourcePropertyDetail> _repository;
        private readonly ResourcePropertyDetailConverter _converter;
        public ResourceForPrintJobConverter(IBaseRepository<ResourcePropertyDetail> repository, ResourcePropertyDetailConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public DataResponseResourceForPrintJob EntityToDTO(ResourceForPrintJob resourceForPrintJob)
        {
            return new DataResponseResourceForPrintJob
            {
                Id = resourceForPrintJob.Id,
                Resource = _converter.EntityToDTO(_repository.GetAsync(x => x.Id == resourceForPrintJob.ResourcePropertyDetailId).Result),
                Quantity = resourceForPrintJob.Quantity,
            };
        }
    }
}
