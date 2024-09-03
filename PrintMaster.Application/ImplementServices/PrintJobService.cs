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

        public async Task<ResponseObject<DataResponsePrintJob>> ConfirmDonePrintJob(Guid printJobId)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
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
                var printJob = await _basePrintJobRepository.GetByIDAsync(printJobId);
                if (printJob == null)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin in ấn không tồn tại"
                    };
                }
                var design = await _baseDesignRepository.GetByIDAsync(printJob.DesignId);

                printJob.PrintJobStatus = Commons.Enumerates.PrintJobStatus.Completed;
                await _basePrintJobRepository.UpdateAsync(printJob);



                var project = await _baseProjectRepository.GetByIDAsync(design.ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponsePrintJob>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy dự án",
                        Data = null
                    };
                }
                project.Status = Commons.Enumerates.ProjectStatus.Completed;
                project.Progress = 100;
                project.ActualEndDate = DateTime.Now;
                await _baseProjectRepository.UpdateAsync(project);
                var resourceForProject = await _baseResourceForPrintJobRepository.GetAllAsync(x => x.PrintJobId == printJobId);
                foreach (var resource in resourceForProject)
                {
                    var resourceDetail = await _baseResourceRepository.GetAsync(x => x.Id == resource.ResourcePropertyDetailId);
                    project.StartingPrice += resourceDetail.Price * resourceDetail.Quantity;
                }
                await _baseProjectRepository.UpdateAsync(project);
                var kpi = await _keyPerformanceIndicatorsRepository.GetAsync(x => x.EmployeeId == project.EmployeeCreateId);
                if (kpi != null)
                {
                    kpi.ActuallyAchieved += 1;
                    await _keyPerformanceIndicatorsRepository.UpdateAsync(kpi);
                    if (kpi.ActuallyAchieved >= kpi.Target)
                    {

                        kpi.AchieveKPI = true;
                        await _keyPerformanceIndicatorsRepository.UpdateAsync(kpi);
                        Notification notification = new Notification
                        {
                            IsDeleted = false,
                            Content = $"Chúc mừng bạn đã hoàn thành KPI! chúng tôi sẽ có hình thức khen thưởng đối với bạn",
                            Id = Guid.NewGuid(),
                            IsSeen = false,
                            Link = "",
                            UserId = project.EmployeeCreateId
                        };
                        notification = await _notificationRepository.CreateAsync(notification);
                    }
                }

                var listUsers = await _baseUserRepository.GetAllAsync(x => x.IsDeleted == false);
                List<User> users = new List<User>();
                foreach (var user in listUsers)
                {
                    var roleOfUser = await _userRepository.GetRolesOfUserAsync(user);
                    var team = await _teamRepository.GetAsync(x => x.Id == user.TeamId);
                    if ((roleOfUser.Contains("Manager") && team.Name.Equals("Delivery")) || roleOfUser.Contains("Admin"))
                    {
                        users.Add(user);
                    }
                }
                foreach (var user in users)
                {
                    Notification notification = new Notification
                    {
                        IsDeleted = false,
                        Content = $"Qúa trình in ấn đã hoàn thành! Có thể giao cho khách hàng",
                        Id = Guid.NewGuid(),
                        IsSeen = false,
                        Link = "",
                        UserId = user.Id
                    };
                    notification = await _notificationRepository.CreateAsync(notification);
                }
                var customer = await _customerRepository.GetByIDAsync(project.CustomerId);

                var totalMoney = project.StartingPrice;

                Bill bill = new Bill
                {
                    IsDeleted = false,
                    BillName = $"Hóa đơn chi tiết cho dự án {project.ProjectName}! Bạn vui lòng kiểm tra",
                    BillStatus = Commons.Enumerates.BillStatus.UnPaid,
                    CustomerId = customer.Id,
                    EmployeeId = project.EmployeeCreateId,
                    ProjectId = project.Id,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    TotalMoney = project.StartingPrice,
                    TradingCode = "PrintMaster_" + DateTime.Now.Ticks,
                };
                bill = await _billRepository.CreateAsync(bill);
                var message = SendEmail(new EmailTo
                {
                    Mail = customer.Email,
                    Subject = "Thông tin đơn hàng của bạn: ",
                    Content = HandleTemplateEmail.GenerateNotificationBillEmail(bill)
                });
                var leader = await _baseUserRepository.GetByIDAsync(project.LeaderId);
                if (leader != null)
                {
                    await _userRepository.DeleteRolesOfUserAsync(leader, new List<string> { "Leader" });
                }

                return new ResponseObject<DataResponsePrintJob>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đã hoàn thành in ấn",
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
        public string SendEmail(EmailTo emailTo)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("krbabuto@gmail.com", "eutf zgwj ygii yzmb"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("krbabuto@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Xác nhận gửi email thành công, lấy mã để xác thực";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
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
                    PrintJobStatus = Commons.Enumerates.PrintJobStatus.Printing,
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
                project.Status = Commons.Enumerates.ProjectStatus.InProgress;
                project.Progress = 75;
                await _baseProjectRepository.UpdateAsync(project);


                var notification = await CreateNotification(project.Id);
                return new ResponseObject<DataResponsePrintJob>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đã bắt đầu quy trình in ấn",
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
                Content = $"Thiết kế của dự án {project.ProjectName} bắt đầu được in!",
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
