using Microsoft.AspNetCore.Http;
using PrintMaster.Application.Handle.HandleFile;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.RequestModels.ProjectRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataProject;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class ProjectService : IProjectService
    {
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Notification> _baseNotificationRepository;
        private readonly ProjectConverter _converter;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        public ProjectService(IBaseRepository<Project> baseProjectRepository,
                              IBaseRepository<User> baseUserRepository,
                              IHttpContextAccessor httpContextAccessor,
                              IBaseRepository<Customer> baseCustomerRepository,
                              IBaseRepository<Design> baseDesignRepository,
                              ProjectConverter converter,
                              IBaseRepository<Team> baseTeamRepository,
                              IUserRepository userRepository,
                              IBaseRepository<Notification> baseNotificationRepository)
        {
            _baseProjectRepository = baseProjectRepository;
            _baseUserRepository = baseUserRepository;
            _httpContextAccessor = httpContextAccessor;
            _baseCustomerRepository = baseCustomerRepository;
            _baseDesignRepository = baseDesignRepository;
            _converter = converter;
            _baseTeamRepository = baseTeamRepository;
            _userRepository = userRepository;
            _baseNotificationRepository = baseNotificationRepository;
        }
        public async Task<ResponseObject<DataResponseProject>> CreateProject(Request_CreateProject request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var userId = Guid.Parse(currentUser.FindFirst("Id").Value);
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            try
            {
                var user = await _baseUserRepository.GetByIDAsync(userId);
                var team = await _baseTeamRepository.GetAsync(x => x.Id == user.TeamId);
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (!currentUser.IsInRole("Employee"))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var projectCount = await _baseProjectRepository.CountAsync(x => x.Id == userId);

                if (currentUser.IsInRole("Employee") && team.Name.Equals("Sales") && projectCount >= 1)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Số dự án bạn được tạo đạt số tối đa là 1",
                        Data = null
                    };
                }
                var leader = await _baseUserRepository.GetByIDAsync(request.LeaderId);
                if (leader == null)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy leader",
                        Data = null
                    };
                }
                var teamLeader = await _baseTeamRepository.GetAsync(x => x.Id == leader.TeamId);
                if (!teamLeader.Name.Equals("Technical"))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng này không phải nhân viên phòng ban kĩ thuật",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(leader).Result.Contains("Leader"))
                {
                    await _userRepository.AddRolesToUser(leader, new List<string> { "Leader" });
                }
                var customer = await _baseCustomerRepository.GetByIDAsync(request.CustomerId);
                if (customer == null)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy khách hàng",
                        Data = null
                    };
                }
                var project = new Project
                {
                    IsDeleted = false,
                    CustomerId = request.CustomerId,
                    Description = request.Description,
                    ExpectedEndDate = request.ExpectedEndDate,
                    Id = Guid.NewGuid(),
                    LeaderId = request.LeaderId,
                    ProjectName = request.ProjectName,
                    ImageDescription = await HandleUploadFile.Upfile(request.ImageDescription),
                    Status = Commons.Enumerates.ProjectStatus.Initialization,
                    CommissionPercentage = request.CommissionPercentage,
                    RequestDescriptionFromCustomer = request.RequestDescriptionFromCustomer,
                    StartDate = DateTime.Now,
                    EmployeeCreateId = leader.Id,
                    StartingPrice = request.StartingPrice
                };
                project = await _baseProjectRepository.CreateAsync(project);

                Notification notification = new Notification
                {
                    IsDeleted = false,
                    Content = $"Bạn đã được phân công dự án {project.ProjectName} Vui lòng kiểm tra thông báo",
                    Link = "",
                    Id = Guid.NewGuid(),
                    IsSeen = false,
                    UserId = request.LeaderId
                };

                notification = await _baseNotificationRepository.CreateAsync(notification);

                return new ResponseObject<DataResponseProject>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin dự án thành công",
                    Data = _converter.EntityToDTO(project)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseProject>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = $"Có lỗi: {ex.Message}"
                };
            }
        }

        public async Task<IQueryable<DataResponseProject>> GetAllProject(Request_InputProject? request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new Exception("Người dùng chưa được xác thực");
            }
            var query = await _baseProjectRepository.GetAllAsync(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(request.ProjectName))
            {
                query = query.Where(x => x.ProjectName.ToLower().Contains(request.ProjectName.ToLower()));
            }
            if (request.LeaderId.HasValue)
            {
                query = query.Where(x => x.LeaderId == request.LeaderId);
            }
            if (request.CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == request.CustomerId);
            }
            if (request.StartDate.HasValue && !request.EndDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= request.StartDate);
            }
            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= request.StartDate && x.StartDate <= request.EndDate);
            }
            return query.Select(x => _converter.EntityToDTO(x));
        }
    }
}
