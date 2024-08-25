using PrintMaster.Application.Payloads.ResponseModels.DataPrintJob;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class PrintJobConverter
    {
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly IBaseRepository<ResourceForPrintJob> _repository;
        private readonly IBaseRepository<ResourcePropertyDetail> _propertyDetailRepository;
        private readonly ResourceForPrintJobConverter _converter;
        private readonly DesignConverter _designConverter;
        public PrintJobConverter(IBaseRepository<Design> baseDesignRepository,
                                 IBaseRepository<ResourceForPrintJob> repository,
                                 ResourceForPrintJobConverter converter,
                                 DesignConverter designConverter,
                                 IBaseRepository<ResourcePropertyDetail> propertyDetailRepository)
        {
            _baseDesignRepository = baseDesignRepository;
            _repository = repository;
            _converter = converter;
            _designConverter = designConverter;
            _propertyDetailRepository = propertyDetailRepository;
        }

        public DataResponsePrintJob EntityToDTO(PrintJob printJob)
        {
            var list = _repository.GetAllAsync(x => x.PrintJobId == printJob.Id).Result;
            List<string> result = new List<string>();

            foreach (var item in list)
            {
                var propertyDetail = _propertyDetailRepository.GetAllAsync(x => x.Id == item.ResourcePropertyDetailId);
                foreach (var property in propertyDetail.Result)
                {
                    result.Add(property.Name);
                }
            }
            return new DataResponsePrintJob
            {
                Id = printJob.Id,
                PrintJobStatus = printJob.PrintJobStatus.ToString(),
                DesignImage = _baseDesignRepository.GetAsync(x => x.Id == printJob.DesignId).Result.FilePath,
                DesignId = (Guid)printJob.DesignId,
                ResourceForPrints = result.AsQueryable(),
            };
        }
    }
}
