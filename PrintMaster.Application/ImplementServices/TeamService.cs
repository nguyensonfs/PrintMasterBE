using Microsoft.AspNetCore.Http;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataTeam;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class TeamService : ITeamService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly TeamConverter _converter;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Notification> _notificationRepository;
        private readonly UserConverter _userConverter;

        public TeamService(IBaseRepository<User> baseUserRepository,
                           IBaseRepository<Team> baseTeamRepository,
                           TeamConverter converter,
                           IHttpContextAccessor contextAccessor,
                           IUserRepository userRepository,
                           IBaseRepository<Notification> notificationRepository,
                           UserConverter userConverter)
        {
            _baseUserRepository = baseUserRepository;
            _baseTeamRepository = baseTeamRepository;
            _converter = converter;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
            _userConverter = userConverter;
        }


        public async Task<ResponseObject<DataResponseTeam>> ChangeManagerForTeam(Guid teamId, Guid managerId)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var manager = await _baseUserRepository.GetByIDAsync(managerId);
                if (!_userRepository.GetRolesOfUserAsync(manager).Result.Contains("Manager"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người này không có quyền quản lý",
                        Data = null
                    };
                }
                var team = await _baseTeamRepository.GetByIDAsync(teamId);
                if (team == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy phòng ban",
                        Data = null
                    };
                }
                team.ManagerId = managerId;
                team.UpdateTime = DateTime.Now;
                await _baseTeamRepository.UpdateAsync(team);
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thay đổi trưởng phòng thành công",
                    Data = _converter.EntityToDTO(team)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseTeam>> CreateTeam(Request_CreateTeam request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var manager = await _baseUserRepository.GetByIDAsync(request.ManagerId);
                if (manager == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy người dùng",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(manager).Result.Contains("Manager"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người này không có quyền manager",
                        Data = null
                    };
                }

                var team = new Team
                {
                    IsDeleted = false,
                    CreateTime = DateTime.Now,
                    Description = request.Description,
                    Id = Guid.NewGuid(),
                    ManagerId = request.ManagerId,
                    Name = request.Name,
                    NumberOfMember = 0
                };
                team = await _baseTeamRepository.CreateAsync(team);

                Notification notification = new Notification
                {
                    IsDeleted = false,
                    Content = $"Bạn đã được chuyển lên làm quản lý của phòng ban {team.Name}!",
                    Id = Guid.NewGuid(),
                    IsSeen = false,
                    Link = "",
                    UserId = manager.Id
                };
                notification = await _notificationRepository.CreateAsync(notification);

                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin phòng ban thành công",
                    Data = _converter.EntityToDTO(team)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseMessages> DeleteTeam(Guid teamId)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực"
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseMessages { Status = StatusCodes.Status400BadRequest, Message = "Bạn không có quyền thực hiện chức năng này" };
                }

                var team = await _baseTeamRepository.GetByIDAsync(teamId);
                if (team == null)
                {
                    return new ResponseMessages { Status = StatusCodes.Status404NotFound, Message = "Không tìm thấy phòng ban" };
                }
                team.IsDeleted = true;
                team.UpdateTime = DateTime.Now;
                await _baseTeamRepository.UpdateAsync(team);
                return new ResponseMessages { Status = StatusCodes.Status200OK, Message = "Xóa thông tin phòng ban thành công" };
            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Có lỗi : {ex.Message}",
                };
            }
        }

        public async Task<IQueryable<DataResponseTeam>> GetAllTeams(string? name)
        {
            var query = await _baseTeamRepository.GetAllAsync(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(record => record.Name.ToLower().Contains(name.ToLower()));
            }
            return query.Select(x => _converter.EntityToDTO(x));
        }

        public async Task<IQueryable<DataResponseUser>> GetAllUserByTeam(Guid teamId)
        {
            var query = await _baseUserRepository.GetAllAsync(item => item.IsDeleted == false && item.TeamId == teamId);
            return query.Select(item => _userConverter.EntityToDTO(item));
        }


        public async Task<ResponseObject<DataResponseTeam>> GetTeamById(Guid teamId)
        {
            var team = await _baseTeamRepository.GetByIDAsync(teamId);
            if (team == null)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Phòng ban không tồn tại",
                    Data = null
                };
            }
            return new ResponseObject<DataResponseTeam>
            {
                Status = StatusCodes.Status200OK,
                Message = "Đã tìm thấy phòng ban",
                Data = _converter.EntityToDTO(team)
            };
        }

        public async Task<ResponseObject<DataResponseTeam>> UpdateTeam(Guid id, Request_UpdateTeam request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa xác thực",
                        Data = null
                    };
                }

                var manager = await _baseUserRepository.GetByIDAsync(request.ManagerId);
                if (manager == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy người dùng",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(manager).Result.Contains("Manager"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng này không có quyền quản lý",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetAsync(x => x.Id == Guid.Parse(currentUser.FindFirst("Id").Value));
                var team = await _baseTeamRepository.GetByIDAsync(id);
                if (team == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy phòng ban",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Manager") && team.ManagerId != user.Id)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                team.UpdateTime = DateTime.Now;
                team.Description = request.Description;
                team.Name = request.Name;
                team.ManagerId = request.ManagerId;
                await _baseTeamRepository.UpdateAsync(team);
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thông tin phòng ban thành công",
                    Data = _converter.EntityToDTO(team)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
