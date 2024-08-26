using PrintMaster.Application.Payloads.ResponseModels.DataProject;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class ProjectConverter
    {
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly CustomerConverter _customerConverter;
        private readonly UserConverter _userConverter;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly DesignConverter _designConverter;
        private readonly IBaseRepository<PrintJob> _printJobRepository;
        public ProjectConverter(IBaseRepository<Project> baseProjectRepository,
                                CustomerConverter customerConverter,
                                UserConverter userConverter,
                                IBaseRepository<Customer> baseCustomerRepository,
                                IBaseRepository<Design> baseDesignRepository,
                                IBaseRepository<User> baseUserRepository,
                                DesignConverter designConverter,
                                IBaseRepository<PrintJob> printJobRepository)
        {
            _baseProjectRepository = baseProjectRepository;
            _customerConverter = customerConverter;
            _userConverter = userConverter;
            _baseUserRepository = baseUserRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _baseDesignRepository = baseDesignRepository;
            _designConverter = designConverter;
            _printJobRepository = printJobRepository;
        }
        public DataResponseProject EntityToDTO(Project project)
        {
            var customer = _baseCustomerRepository.GetByIDAsync((Guid)project.CustomerId).Result;
            var leader = _baseUserRepository.GetByIDAsync((Guid)project.LeaderId).Result;
            var design = _baseDesignRepository.GetAsync(item => item.ProjectId == project.Id && item.DesignStatus == Commons.Enumerates.DesignStatus.HasBeenApproved).Result;
            var printJob = design != null ? _printJobRepository.GetAsync(item => item.DesignId == design.Id).Result : null;

            var imageUrl = string.IsNullOrEmpty(project.ImageDescription)
                   ? null
                   : $"/uploads/{project.ImageDescription}";

            return new DataResponseProject
            {
                ActualEndDate = project.ActualEndDate,
                Customer = customer.FullName,
                Description = project.Description,
                ExpectedEndDate = project.ExpectedEndDate,
                Id = project.Id,
                Leader = leader.FullName,
                Progress = project.Progress,
                PhoneLeader = leader.PhoneNumber,
                EmailLeader = leader.Email,
                PhoneCustomer = customer.PhoneNumber,
                EmailCustomer = customer.Email,
                AddressCustomer = customer.Address,
                PrintJobId = printJob == null ? Guid.Empty : printJob.Id,
                ProjectName = project.ProjectName,
                ProjectStatus = project.Status.ToString(),
                CustomerId = customer.Id,
                RequestDescriptionFromCustomer = project.RequestDescriptionFromCustomer,
                StartDate = project.StartDate,
                StartingPrice = project.StartingPrice,
                CommissionPercentage = project.CommissionPercentage,
                ImageDescription = imageUrl,
                EmployeeCreateName = _baseUserRepository.GetAsync(x => x.Id == project.EmployeeCreateId).Result.FullName,
                Designs = _baseDesignRepository.GetAllAsync(x => x.IsDeleted == false && x.ProjectId == project.Id).Result.Select(x => _designConverter.EntityToDTO(x))
            };
        }
    }
}
