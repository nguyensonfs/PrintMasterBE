using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.ShippingMethodRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataCustomer;
using PrintMaster.Application.Payloads.ResponseModels.DataShippingMethod;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<ShippingMethod> _baseReposiroty;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Team> _teamRepository;
        public ShippingMethodService(IHttpContextAccessor contextAccessor,
                                     IBaseRepository<ShippingMethod> baseReposiroty,
                                     IBaseRepository<User> baseUserRepository,
                                     IUserRepository userRepository,
                                     IBaseRepository<Team> teamRepository)
        {
            _contextAccessor = contextAccessor;
            _baseReposiroty = baseReposiroty;
            _baseUserRepository = baseUserRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
        }

        public async Task<ResponseObject<DataResponseShippingMethod>> CreateShippingMethod(Request_CreateShippingMethod request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseShippingMethod>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetAsync(x => x.Id == Guid.Parse(currentUser.FindFirst("Id").Value));
                var team = await _teamRepository.GetAsync(x => x.Id == user.TeamId);
                if (!currentUser.IsInRole("Manager") || !team.Name.Equals("Delivery") || team.ManagerId != user.Id)
                {
                    return new ResponseObject<DataResponseShippingMethod>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                ShippingMethod shippingMethod = new ShippingMethod
                {
                    IsDeleted = false,
                    Id = Guid.NewGuid(),
                    ShippingMethodName = request.ShippingMethodName,
                };
                shippingMethod = await _baseReposiroty.CreateAsync(shippingMethod);
                return new ResponseObject<DataResponseShippingMethod>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thêm thông tin giao hàng thành công",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseShippingMethod>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<IQueryable<DataResponseShippingMethod>> GetAllShippingMethod()
        {
            try
            {
                var query = await _baseReposiroty.GetAllAsync(record => record.IsDeleted == false);

                return query.Select(item => new DataResponseShippingMethod
                {
                    Id = item.Id,
                    ShippingMethodName = item.ShippingMethodName,
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
