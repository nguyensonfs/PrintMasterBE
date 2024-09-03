using Microsoft.AspNetCore.Http;
using PrintMaster.Application.Handle.HandleFile;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.DesignRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataDesign;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class DesignService : IDesignService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Project> _baseProjectReposiroty;
        private readonly DesignConverter _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Notification> _notificationRepository;
        public DesignService(IBaseRepository<User> baseUserRepository,
                             IHttpContextAccessor httpContextAccessor,
                             IBaseRepository<Design> baseDesignRepository,
                             DesignConverter mapper,
                             IBaseRepository<Project> baseProjectReposiroty,
                             IBaseRepository<Team> baseTeamRepository,
                             IUserRepository userRepository,
                             IBaseRepository<Notification> notificationRepository)
        {
            _baseUserRepository = baseUserRepository;
            _httpContextAccessor = httpContextAccessor;
            _baseDesignRepository = baseDesignRepository;
            _mapper = mapper;
            _baseProjectReposiroty = baseProjectReposiroty;
            _baseTeamRepository = baseTeamRepository;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<ResponseMessages> ApprovalDesign(Guid ProjectId, Guid DesignId, Request_DesignApproval request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var leader = currentUser.FindFirst("Id").Value;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseMessages
                    {
                        Message = "Người dùng chưa được xác thực",
                        Status = StatusCodes.Status401Unauthorized
                    };
                }
                if (!currentUser.IsInRole("Leader"))
                {
                    return new ResponseMessages
                    {
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Status = StatusCodes.Status403Forbidden
                    };
                }
                var design = await _baseDesignRepository.GetByIDAsync(DesignId);
                if (design == null)
                {
                    return new ResponseMessages
                    {
                        Message = "Không tìm thấy thiết kế",
                        Status = StatusCodes.Status404NotFound
                    };
                }
                var project = await _baseProjectReposiroty.GetByIDAsync(ProjectId);
                if (project == null)
                {
                    return new ResponseMessages
                    {
                        Message = "Không tìm thấy dự án",
                        Status = StatusCodes.Status404NotFound
                    };
                }
                if (Guid.Parse(leader) != project.LeaderId)
                {
                    return new ResponseMessages
                    {
                        Message = "Bạn không phải là leader của dự án này",
                        Status = StatusCodes.Status403Forbidden
                    };
                }
                if (design.DesignStatus == Commons.Enumerates.DesignStatus.HasBeenApproved)
                {
                    return new ResponseMessages
                    {
                        Message = "Thiết kế này đã được duyệt trước đó",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                var listDesign = await _baseDesignRepository.GetAllAsync(record => record.ProjectId == project.Id
                    && !record.IsDeleted
                    && record.DesignerId == design.DesignerId
                    && record.DesignStatus == Commons.Enumerates.DesignStatus.HasBeenApproved);
                if (listDesign.ToList().Count >= 1)
                {
                    return new ResponseMessages
                    {
                        Message = "Đã có thiết kế được duyệt trước đó",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                if (request.DesignApproval.ToString().Equals("Agree"))
                {
                    design.DesignStatus = Commons.Enumerates.DesignStatus.HasBeenApproved;
                    design.ApproverId = Guid.Parse(leader);
                    await _baseDesignRepository.UpdateAsync(design);
                    project.Status = Commons.Enumerates.ProjectStatus.Approved;
                    project.Progress = 25;
                    await _baseProjectReposiroty.UpdateAsync(project);

                    var notification = new Notification
                    {
                        IsDeleted = false,
                        Content = "Thiết kế của bạn đã được duyệt",
                        Id = Guid.NewGuid(),
                        IsSeen = false,
                        Link = "",
                        UserId = design.DesignerId
                    };

                    notification = await _notificationRepository.CreateAsync(notification);
                    return new ResponseMessages
                    {
                        Message = "Đã duyệt thiết kế",
                        Status = StatusCodes.Status200OK
                    };
                }
                else
                {
                    design.DesignStatus = Commons.Enumerates.DesignStatus.Refuse;
                    design.ApproverId = Guid.Parse(leader);
                    await _baseDesignRepository.UpdateAsync(design);

                    var notification = new Notification
                    {
                        IsDeleted = false,
                        Content = "Thiết kế của bạn bị từ chối phê duyệt",
                        Id = Guid.NewGuid(),
                        IsSeen = false,
                        Link = "",
                        UserId = design.DesignerId
                    };

                    notification = await _notificationRepository.CreateAsync(notification);
                    return new ResponseMessages
                    {
                        Message = "Không duyệt thiết kế",
                        Status = StatusCodes.Status200OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Message = $"Có lỗi: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ResponseObject<DataResponseDesign>> CreateDesign(Guid designerId, Guid ProjectId, Request_CreateDesign request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Designer"))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var designer = await _baseUserRepository.GetByIDAsync(designerId);
                var team = await _baseTeamRepository.GetAsync(x => x.Id == designer.TeamId);
                if (!team.Name.Equals("Technical"))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng này không trong phòng ban kĩ thuật",
                        Data = null
                    };
                }
                var listDesign = await _baseDesignRepository.GetAllAsync(x => x.ProjectId == ProjectId
                && x.DesignStatus == Commons.Enumerates.DesignStatus.HasBeenApproved);
                if (listDesign.ToList().Count > 0)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Dự án này đã có thiết kế được phê duyệt",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(designer).Result.Contains("Designer"))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng không có quyền design",
                        Data = null
                    };
                }
                var project = await _baseProjectReposiroty.GetByIDAsync(ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin dự án không tồn tại",
                        Data = null
                    };
                }

                project.Status = Commons.Enumerates.ProjectStatus.Designing;
                await _baseProjectReposiroty.UpdateAsync(project);
                Design design = new Design
                {
                    IsDeleted = false,
                    DesignerId = designerId,
                    FilePath = await HandleUploadFile.Upfile(request.DesignImage),
                    DesignStatus = Commons.Enumerates.DesignStatus.NotYetApproved,
                    DesignTime = DateTime.Now,
                    ProjectId = ProjectId,
                    Id = Guid.NewGuid()
                };
                design = await _baseDesignRepository.CreateAsync(design);
                project.Status = Commons.Enumerates.ProjectStatus.AwaitingApproval;
                await _baseProjectReposiroty.UpdateAsync(project);
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo bản thiết kế thành công",
                    Data = _mapper.EntityToDTO(design)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseDesign>> UpdateDesign(Guid designerId, Guid ProjectId, Guid DesignId, Request_UpdateDesign request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                var design = await _baseDesignRepository.GetByIDAsync(DesignId);
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Designer") || design.DesignerId != Guid.Parse(currentUser.FindFirst("Id").Value))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện hành động này",
                        Data = null
                    };
                }
                var designer = await _baseUserRepository.GetByIDAsync(designerId);
                var project = await _baseProjectReposiroty.GetByIDAsync(ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy dự án",
                        Data = null
                    };
                }

                if (design == null)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy bản thiết kế",
                        Data = null
                    };
                }
                if (design.DesignStatus.ToString().Equals("HasBeenApproved") || design.DesignStatus.ToString().Equals("Refuse"))
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bản thiết kế đã được chấp nhận hoặc từ chối không thể chỉnh sửa",
                        Data = null
                    };
                }
                design.FilePath = await HandleUploadFile.Upfile(request.DesignImage);
                design.ProjectId = ProjectId;
                design = await _baseDesignRepository.UpdateAsync(design);
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thiết kế thành công!!!",
                    Data = _mapper.EntityToDTO(design)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDesign>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
