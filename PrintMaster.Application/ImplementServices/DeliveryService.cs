using Microsoft.AspNetCore.Http;
using PrintMaster.Application.Handle.HandleEmail;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.DeliveryRequests;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataDelivery;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IBaseRepository<Delivery> _baseDeliveryRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly DeliveryConverter _deliveryConverter;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Team> _teamRepository;
        private readonly IBaseRepository<ShippingMethod> _shippingMethodRepository;
        private readonly IBaseRepository<Project> _projectRepository;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IBaseRepository<Notification> _notificationRepository;
        private readonly IBaseRepository<Bill> _billRepository;
        private readonly IEmailService _emailService;
        public DeliveryService(IBaseRepository<Delivery> baseDeliveryRepository,
                               DeliveryConverter deliveryConverter,
                               IHttpContextAccessor contextAccessor,
                               IUserRepository userRepository,
                               IBaseRepository<User> baseUserRepository,
                               IBaseRepository<Team> teamRepository,
                               IBaseRepository<Customer> customerRepository,
                               IBaseRepository<Notification> notificationRepository,
                               IEmailService emailService,
                               IBaseRepository<Project> projectRepository,
                               IBaseRepository<ShippingMethod> shippingMethodRepository,
                               IBaseRepository<Bill> billRepository)
        {
            _baseDeliveryRepository = baseDeliveryRepository;
            _deliveryConverter = deliveryConverter;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _baseUserRepository = baseUserRepository;
            _teamRepository = teamRepository;
            _notificationRepository = notificationRepository;
            _emailService = emailService;
            _projectRepository = projectRepository;
            _customerRepository = customerRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _billRepository = billRepository;
        }
        public async Task<ResponseObject<DataResponseDelivery>> CreateDelivery(Request_CreateDelivery request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
                        Data = null
                    };
                }
                var user = await _baseUserRepository.GetAsync(x => x.Id == Guid.Parse(currentUser.FindFirst("Id").Value));
                var team = await _teamRepository.GetAsync(x => x.Id == user.TeamId);
                if (!currentUser.IsInRole("Manager") && !team.Name.Equals("Delivery"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var project = await _projectRepository.GetByIDAsync(request.ProjectId);
                if (project == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Dự án không tồn tại",
                        Data = null
                    };
                }
                if (!project.Status.ToString().Equals("Completed"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Dự án chưa được hoàn thành! Không thể giao đến cho khách hàng",
                        Data = null
                    };
                }
                var shippingMethod = await _shippingMethodRepository.GetAsync(x => x.Id == request.ShippingMethodId);
                if (shippingMethod == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phương thức giao hàng không tồn tại",
                        Data = null
                    };
                }
                var deliver = await _baseUserRepository.GetByIDAsync(request.DeliverId);
                if (deliver == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin người giao hàng không tồn tại",
                        Data = null
                    };
                }
                var teamDeliver = await _teamRepository.GetAsync(x => x.Id == deliver.TeamId);
                var checkRole = _userRepository.GetRolesOfUserAsync(deliver).Result.Contains("Deliver");
                if (!checkRole || !teamDeliver.Name.Equals("Delivery"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Data = null,
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Người dùng không có quyền hoặc không nằm trong phòng ban Delivery"
                    };
                }

                var customer = await _customerRepository.GetAsync(x => x.Id == request.CustomerId);
                if (customer == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin khách hàng không tồn tại",
                        Data = null
                    };
                }
                Delivery delivery = new Delivery
                {
                    DeliveryAddress = customer.Address,
                    IsDeleted = false,
                    CustomerId = request.CustomerId,
                    DeliverId = request.DeliverId,
                    DeliveryStatus = Commons.Enumerates.DeliveryStatus.Waiting,
                    EstimateDeliveryTime = request.EstimateDeliveryTime,
                    Id = Guid.NewGuid(),
                    ProjectId = request.ProjectId,
                    ShippingMethodId = request.ShippingMethodId,
                };
                delivery = await _baseDeliveryRepository.CreateAsync(delivery);
                Notification notification = new Notification
                {
                    IsDeleted = false,
                    Content = $"Bạn đã được chỉ định giao đơn hàng {project.ProjectName}! Thời gian bắt đầu giao hàng là vào thời điểm này",
                    Id = Guid.NewGuid(),
                    IsSeen = false,
                    Link = "",
                    UserId = deliver.Id
                };
                notification = await _notificationRepository.CreateAsync(notification);

                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin giao hàng thành công",
                    Data = _deliveryConverter.EntityToDTO(delivery),
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseDelivery>> ShipperConfirmDelivery(Guid shipperId, Request_ShipperConfirmDelivery request)
        {
            var deliver = await _baseUserRepository.GetByIDAsync(shipperId);
            try
            {
                var team = await _teamRepository.GetAsync(x => x.Id == deliver.TeamId);
                if (team == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phòng ban không tồn tại",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(deliver).Result.Contains("Deliver") || !team.Name.Equals("Delivery"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var delivery = await _baseDeliveryRepository.GetByIDAsync(request.DeliveryId);
                if (delivery == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin giao hàng không tồn tại",
                        Data = null
                    };
                }
                if (shipperId != delivery.DeliverId)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn không phải là người giao đơn hàng này",
                        Data = null
                    };
                }
                if (delivery.DeliveryStatus.ToString().Equals("Delivered"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Đơn hàng đã được giao từ trước đó",
                        Data = null
                    };
                }
                delivery.DeliveryStatus = Commons.Enumerates.DeliveryStatus.Delivering;
                deliver.UpdateTime = DateTime.Now;
                await _baseDeliveryRepository.UpdateAsync(delivery);

                var project = await _projectRepository.GetAsync(x => x.Id == delivery.ProjectId);


                var customer = await _customerRepository.GetByIDAsync(project.CustomerId);
                var message = new EmailMessage(new string[] { customer.Email },
                                               "Thông báo đơn hàng của bạn: ",
                                               $"Đơn hàng đã được gửi đến bạn");
                var responseMessage = _emailService.SendEmail(message);


                var bill = await _billRepository.GetAsync(x => x.ProjectId == project.Id
                && x.BillStatus == Commons.Enumerates.BillStatus.UnPaid);
                if (bill != null)
                {
                    bill.BillStatus = Commons.Enumerates.BillStatus.Paid;
                    await _billRepository.UpdateAsync(bill);
                }
                delivery.DeliveryStatus = Commons.Enumerates.DeliveryStatus.Delivered;
                await _baseDeliveryRepository.UpdateAsync(delivery);
                project.Status = Commons.Enumerates.ProjectStatus.Delivered;
                await _projectRepository.UpdateAsync(project);
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đơn hàng đã được giao thành công! khách hàng cũng đã nhận hàng",
                    Data = _deliveryConverter.EntityToDTO(delivery)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseDelivery>> ShipperConfirmOrderDelivery(Guid shipperId,
                                                                                            Request_ShipperConfirmOrderDelivery request)
        {
            var deliver = await _baseUserRepository.GetByIDAsync(shipperId);
            try
            {
                var team = await _teamRepository.GetAsync(x => x.Id == deliver.TeamId);
                if (team == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Phòng ban không tồn tại",
                        Data = null
                    };
                }
                if (!_userRepository.GetRolesOfUserAsync(deliver).Result.Contains("Deliver") || !team.Name.Equals("Delivery"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
                var delivery = await _baseDeliveryRepository.GetByIDAsync(request.DeliveryId);
                if (delivery == null)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin giao hàng không tồn tại",
                        Data = null
                    };
                }
                if (shipperId != delivery.DeliverId)
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Bạn không phải là người giao đơn hàng này"
                    };
                }
                if (delivery.DeliveryStatus.ToString().Equals("Delivered"))
                {
                    return new ResponseObject<DataResponseDelivery>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Đơn hàng đã được giao từ trước đó",
                        Data = null
                    };
                }
                delivery.DeliveryStatus = Commons.Enumerates.DeliveryStatus.Delivering;
                deliver.UpdateTime = DateTime.Now;
                await _baseDeliveryRepository.UpdateAsync(delivery);

                var project = await _projectRepository.GetAsync(x => x.Id == delivery.ProjectId);
                Notification notification = new Notification
                {
                    IsDeleted = false,
                    Content = "Người giao hàng đã nhận giao đơn!",
                    Id = Guid.NewGuid(),
                    IsSeen = false,
                    Link = "",
                    UserId = project.LeaderId
                };

                notification = await _notificationRepository.CreateAsync(notification);

                var customer = await _customerRepository.GetByIDAsync(project.CustomerId);
                var message = new EmailMessage(new string[] { customer.Email },
                                               "Thông báo đơn hàng của bạn: ",
                                               "Đơn đặt hàng của bạn đang được giao");
                var responseMessage = _emailService.SendEmail(message);

                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Xác nhận nhiệm vụ giao đơn hàng",
                    Data = _deliveryConverter.EntityToDTO(delivery)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDelivery>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<IQueryable<DataResponseDelivery>> GetAllDelivery(Request_InputDelivery input)
        {
            var query = await _baseDeliveryRepository.GetAllAsync(record => record.IsDeleted == false);
            if (input.ProjectId.HasValue)
            {
                query = query.Where(record => record.ProjectId == input.ProjectId);
            }
            if (input.CustomerId.HasValue)
            {
                query = query.Where(record => record.CustomerId == input.CustomerId);
            }
            if (input.DeliverId.HasValue)
            {
                query = query.Where(record => record.DeliverId == input.DeliverId);
            }
            if (input.DeliveryStatus.HasValue)
            {
                query = query.Where(record => record.DeliveryStatus == input.DeliveryStatus);
            }
            return query.Select(item => _deliveryConverter.EntityToDTO(item));
        }

        public async Task<DataResponseDelivery> GetDeliveryById(Guid id)
        {
            var query = await _baseDeliveryRepository.GetByIDAsync(id);
            return _deliveryConverter.EntityToDTO(query);
        }
    }
}
