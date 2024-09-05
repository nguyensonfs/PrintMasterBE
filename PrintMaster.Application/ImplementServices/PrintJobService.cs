using Microsoft.AspNetCore.Http;
using PrintMaster.Application.Handle.HandleEmail;
using PrintMaster.Application.Handle.HandleTemplate;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.PrintJobRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataPrintJob;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;
using System.Net;
using System.Net.Mail;

namespace PrintMaster.Application.ImplementServices
{
    public class PrintJobService : IPrintJobService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<PrintJob> _basePrintJobRepository;
        private readonly IBaseRepository<ResourceForPrintJob> _baseResourceForPrintJobRepository;
        private readonly IBaseRepository<ResourcePropertyDetail> _baseResourceRepository;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly PrintJobConverter _printerConverter;
        private readonly IBaseRepository<Notification> _notificationRepository;
        private readonly IBaseRepository<Permission> _permissionsRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IAuthService _authService;
        private readonly ResourceForPrintJobConverter _resourceForPrintJobConverter;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Team> _teamRepository;
        private readonly IBaseRepository<ConfirmEmail> _confirmEmailRepository;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IEmailService _emailService;
        private readonly DesignConverter _designConverter;
        private readonly IBaseRepository<KeyPerformanceIndicator> _keyPerformanceIndicatorsRepository;
        private readonly IBaseRepository<Resource> _resourceRepo;
        private readonly IBaseRepository<ResourceProperty> _resourcePropertyRepo;
        private readonly IBaseRepository<Bill> _billRepository;
        private readonly IBaseRepository<ResourceType> _resourceTypeRepo;

        public PrintJobService(IBaseRepository<User> baseUserRepository,
                               IBaseRepository<Project> baseProjectRepository,
                               IBaseRepository<PrintJob> basePrintJobRepository,
                               IBaseRepository<ResourceForPrintJob> baseResourceForPrintJobRepository,
                               IBaseRepository<ResourcePropertyDetail> baseResourceRepository,
                               IHttpContextAccessor contextAccessor,
                               PrintJobConverter printerConverter,
                               IBaseRepository<Design> baseDesignRepository,
                               IBaseRepository<Notification> notificationRepository,
                               IBaseRepository<Permission> permissionsRepository,
                               IBaseRepository<Role> roleRepository,
                               IAuthService authService,
                               IUserRepository userRepository,
                               IBaseRepository<Team> teamRepository,
                               IBaseRepository<ConfirmEmail> confirmEmailRepository,
                               IBaseRepository<Customer> customerRepository,
                               IEmailService emailService,
                               DesignConverter designConverter,
                               ResourceForPrintJobConverter resourceForPrintJobConverter,
                               IBaseRepository<KeyPerformanceIndicator> keyPerformanceIndicatorsRepository,
                               IBaseRepository<Resource> resourceRepo,
                               IBaseRepository<ResourceProperty> resourcePropertyRepo,
                               IBaseRepository<Bill> billRepository,
                               IBaseRepository<ResourceType> resourceTypeRepo)
        {
            _baseUserRepository = baseUserRepository;
            _baseProjectRepository = baseProjectRepository;
            _basePrintJobRepository = basePrintJobRepository;
            _baseResourceForPrintJobRepository = baseResourceForPrintJobRepository;
            _baseResourceRepository = baseResourceRepository;
            _contextAccessor = contextAccessor;
            _printerConverter = printerConverter;
            _baseDesignRepository = baseDesignRepository;
            _notificationRepository = notificationRepository;
            _permissionsRepository = permissionsRepository;
            _roleRepository = roleRepository;
            _authService = authService;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _confirmEmailRepository = confirmEmailRepository;
            _customerRepository = customerRepository;
            _emailService = emailService;
            _designConverter = designConverter;
            _resourceForPrintJobConverter = resourceForPrintJobConverter;
            _keyPerformanceIndicatorsRepository = keyPerformanceIndicatorsRepository;
            _resourcePropertyRepo = resourcePropertyRepo;
            _resourceRepo = resourceRepo;
            _billRepository = billRepository;
            _resourceTypeRepo = resourceTypeRepo;
        }

        public async Task<ResponseObject<DataResponsePrintJob>> CreatePrintJob(Request_CreatePrintJob request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Leader"))
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var design = await _baseDesignRepository.GetByIDAsync(request.DesignId);
                if (design == null)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thiết kế",
                        Data = null
                    };
                }
                if (!design.DesignStatus.ToString().Equals("HasBeenApproved"))
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Thiết kế này chưa được duyệt hoặc đã bị từ chối"
                    };
                }

                PrintJob printJob = new PrintJob
                {
                    IsDeleted = false,
                    DesignId = request.DesignId,
                    Id = Guid.NewGuid(),
                    PrintJobStatus = Commons.Enumerates.PrintJobStatus.Completed,
                };
                await _basePrintJobRepository.CreateAsync(printJob);
                printJob.ResourceForPrints = await CreateListResourceForPrintJob(printJob.Id, request.ResourceForPrints);
                await _basePrintJobRepository.UpdateAsync(printJob);



                var project = await _baseProjectRepository.GetByIDAsync(design.ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin dự án",
                        Data = null
                    };
                }
                project.Status = Commons.Enumerates.ProjectStatus.Completed;
                project.Progress = 100;
                project.ActualEndDate = DateTime.Now;
                await _baseProjectRepository.UpdateAsync(project);

                var customer = await _customerRepository.GetByIDAsync(project.CustomerId);

                var message = new EmailMessage(new string[] { customer.Email }, "Thông báo", $"Thiết kế bạn đặt đã hoàn thành");
                var responseMessage = _emailService.SendEmail(message);

                var notification = await CreateNotification(project.Id);
                return new ResponseObject<DataResponsePrintJob>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "In ấn đã hoàn thành",
                    Data = _printerConverter.EntityToDTO(printJob)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponsePrintJob>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private async Task<Notification> CreateNotification(Guid projectId)
        {
            var project = await _baseProjectRepository.GetByIDAsync(projectId);
            Notification notification = new Notification
            {
                IsDeleted = false,
                Content = $"Thiết kế của dự án {project.ProjectName} đã được được in!",
                Id = Guid.NewGuid(),
                IsSeen = false,
                Link = "",
                UserId = project.LeaderId
            };
            notification = await _notificationRepository.CreateAsync(notification);
            return notification;
        }

        private async Task<List<ResourceForPrintJob>> CreateListResourceForPrintJob(Guid printJobId,
                                                                                    List<Request_CreateResourceForPrintJob> requests)
        {
            var printJob = await _basePrintJobRepository.GetByIDAsync(printJobId);
            if (printJob == null)
            {
                throw new ArgumentNullException(nameof(printJob));
            }
            List<ResourceForPrintJob> listResult = new List<ResourceForPrintJob>();
            foreach (var request in requests)
            {
                var resource = await _baseResourceRepository.GetByIDAsync(request.ResourcePropertyDetailId);
                if (resource == null)
                {
                    throw new ArgumentNullException(nameof(resource));
                }
                if (resource.Quantity == 0)
                {
                    throw new ArgumentException("Hết hàng");
                }
                if (request.Quantity > resource.Quantity)
                {
                    throw new ArgumentException("Không đủ số lượng");
                }
                ResourceForPrintJob item = new ResourceForPrintJob
                {
                    IsDeleted = false,
                    Id = Guid.NewGuid(),
                    PrintJobId = printJobId,
                    Quantity = request.Quantity,
                    ResourcePropertyDetailId = request.ResourcePropertyDetailId
                };
                item = await _baseResourceForPrintJobRepository.CreateAsync(item);
                var resourceProperty = await _resourcePropertyRepo.GetAsync(x => x.Id == resource.ResourcePropertyId);
                var resourceItem = await _resourceRepo.GetAsync(x => x.Id == resourceProperty.ResourceId);
                var resourceType = await _resourceTypeRepo.GetAsync(x => x.Id == resourceItem.ResourceTypeId);
                if (!resourceType.NameOfResourceType.Equals("Non-consumable"))
                {
                    resource.Quantity -= request.Quantity;
                    await _baseResourceRepository.UpdateAsync(resource);

                    resourceItem.AvailableQuantity -= request.Quantity;
                    await _resourceRepo.UpdateAsync(resourceItem);
                }
                listResult.Add(item);
            }
            return listResult;
        }

        public async Task<IQueryable<DataResponsePrintJob>> GetAllPrintJobs()
        {
            var query = await _basePrintJobRepository.GetAllAsync(record => record.IsDeleted == false);
            return query.Select(item => _printerConverter.EntityToDTO(item));
        }

        public async Task<ResponseObject<DataResponsePrintJob>> GetPrintJobById(Guid printJobId)
        {
            var query = await _basePrintJobRepository.GetAsync(record => record.Id == printJobId);
            return new ResponseObject<DataResponsePrintJob> { 
                Status=StatusCodes.Status200OK,
                Message="Lấy dữ liệu thành công",
                Data = _printerConverter.EntityToDTO(query) 
            };
        }
    }

}
