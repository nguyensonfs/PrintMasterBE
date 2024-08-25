using PrintMaster.Application.Payloads.ResponseModels.DataDesign;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class DesignConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly UserConverter _userConverter;
        public DesignConverter(IBaseRepository<User> baseUserRepository,
                               IBaseRepository<Project> baseProjectRepository,
                               IBaseRepository<Customer> baseCustomerRepository,
                               IBaseRepository<Design> baseDesignRepository,
                               UserConverter userConverter)
        {
            _baseUserRepository = baseUserRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseDesignRepository = baseDesignRepository;
            _userConverter = userConverter;
        }
        public DataResponseDesign EntityToDTO(Design design)
        {
            var approver = _baseUserRepository.GetByIDAsync((Guid)design.ApproverId);
            var designer = _baseUserRepository.GetByIDAsync((Guid)design.DesignerId);
            return new DataResponseDesign
            {
                Approver = approver.Result != null ? approver.Result.FullName : null,
                Designer = designer.Result.FullName,
                DesignImage = design.FilePath,
                DesignStatus = design.DesignStatus.ToString(),
                DesignTime = design.DesignTime,
                Id = design.Id
            };
        }
    }
}
