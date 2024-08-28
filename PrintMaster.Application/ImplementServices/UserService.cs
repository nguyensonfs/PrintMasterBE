using Microsoft.AspNetCore.Http;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Application.Payloads.RequestModels.UserRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly UserConverter _converter;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IBaseRepository<User> baseUserRepository,
                           UserConverter converter,
                           IUserRepository userRepository,
                           IBaseRepository<Team> baseTeamRepository,
                           IHttpContextAccessor contextAccessor)
        {
            _baseUserRepository = baseUserRepository;
            _converter = converter;
            _userRepository = userRepository;
            _baseTeamRepository = baseTeamRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ResponseMessages> ChangeDepartmentForUser(Guid EmployeeId, Request_ChangeDepartmentForUser request)
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
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn không có quyền thực hiện chức năng này"
                    };

                }
                var employee = await _baseUserRepository.GetByIDAsync(EmployeeId);
                if (employee == null)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy nhân viên này"
                    };
                }
                if (_userRepository.GetRolesOfUserAsync(employee).Result.Contains("Manager"))
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Thực hiện không thành công! Người này đang giữ chức vụ trưởng phòng"
                    };
                }
                var team = await _baseTeamRepository.GetByIDAsync(request.TeamId);
                if (team == null)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy phòng ban"
                    };
                }
                employee.TeamId = request.TeamId;
                employee.UpdateTime = DateTime.Now;
                await _baseUserRepository.UpdateAsync(employee);
                team.NumberOfMember = await _baseUserRepository.CountAsync(x => x.TeamId == team.Id);
                await _baseTeamRepository.UpdateAsync(team);
                return new ResponseMessages
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thay đổi phòng ban cho người dùng thành công"
                };
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

        public async Task<IQueryable<string>> GetRolesByUserId(Guid userId)
        {
            var user = await _baseUserRepository.GetByIDAsync(userId);
            var query = await _userRepository.GetRolesOfUserAsync(user);
            return query.AsQueryable();
        }

        public async Task<IQueryable<DataResponseUser>> GetAllUsers(Request_InputUser request)
        {
            var query = await _baseUserRepository.GetAllAsync(x => x.IsDeleted == false);
            if (request.TeamId.HasValue)
            {
                query = query.Where(x => x.TeamId == request.TeamId);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.FullName.ToLower().Contains(request.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(x => x.Email.Equals(request.Email));
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Equals(request.PhoneNumber));
            }
            return query.Select(x => _converter.EntityToDTO(x));
        }

        public async Task<DataResponseUser> GetUserById(Guid id)
        {
            return _converter.EntityToDTO(await _baseUserRepository.GetByIDAsync(id));
        }

        public Task<ResponseObject<DataResponseUser>> UpdateUser(Guid id, Request_UpdateUser request)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<DataResponseUser>> GetAllUserContainsManagerRole()
        {
            var listUser = await _baseUserRepository.GetAllAsync(x => x.IsDeleted == false);
            List<User> users = new List<User>();
            foreach (var user in listUser)
            {
                var team = await _baseTeamRepository.GetAsync(x => x.Id == user.TeamId);
                if (_userRepository.GetRolesOfUserAsync(user).Result.Contains("Manager"))
                {
                    users.Add(user);
                }
            }
            return users.Select(x => _converter.EntityToDTO(x)).AsQueryable();
        }

        public async Task<IQueryable<DataResponseUser>> GetAllUserContainsLeaderRole()
        {
            var listUser = await _baseUserRepository.GetAllAsync(x => x.IsDeleted == false);
            List<User> users = new List<User>();
            foreach (var user in listUser)
            {
                var team = await _baseTeamRepository.GetAsync(x => x.Id == user.TeamId);
                if (_userRepository.GetRolesOfUserAsync(user).Result.Contains("Leader"))
                {
                    users.Add(user);
                }
            }
            return users.Select(x => _converter.EntityToDTO(x)).AsQueryable();
        }

        public async Task<IQueryable<DataResponseUser>> GetAllEmployee()
        {
            var listUser = await _baseUserRepository.GetAllAsync(x => x.IsDeleted == false);
            List<User> users = new List<User>();
            foreach (var user in listUser)
            {
                var team = await _baseTeamRepository.GetAsync(x => x.Id == user.TeamId);
                if (_userRepository.GetRolesOfUserAsync(user).Result.Contains("Employee"))
                {
                    users.Add(user);
                }
            }
            return users.Select(x => _converter.EntityToDTO(x)).AsQueryable();
        }

        public async Task<ResponseObject<DataResponseUser>> AddRoleToUser(Guid userId, List<string> roles)
        {
            try
            {
                var user = await _baseUserRepository.GetByIDAsync(userId);
                if (user == null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin người dùng",
                        Data = null
                    };
                }
                await _userRepository.AddRolesToUser(user, roles);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thêm quyền hạn cho người dùng thành công",
                    Data = _converter.EntityToDTO(user)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }
}
