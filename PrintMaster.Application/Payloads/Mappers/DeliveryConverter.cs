using PrintMaster.Application.Payloads.ResponseModels.DataDelivery;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class DeliveryConverter
    {
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<ShippingMethod> _baseShippingMethodRepository;
        private readonly CustomerConverter _customerConverter;
        private readonly UserConverter _userConverter;
        private readonly ProjectConverter _projectConverter;
        public DeliveryConverter(IBaseRepository<Customer> baseCustomerRepository,
                                 IBaseRepository<User> baseUserRepository,
                                 IBaseRepository<Project> baseProjectRepository,
                                 IBaseRepository<ShippingMethod> baseShippingMethodRepository,
                                 CustomerConverter customerConverter,
                                 UserConverter userConverter,
                                 ProjectConverter projectConverter)
        {
            _baseCustomerRepository = baseCustomerRepository;
            _baseUserRepository = baseUserRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseShippingMethodRepository = baseShippingMethodRepository;
            _customerConverter = customerConverter;
            _userConverter = userConverter;
            _projectConverter = projectConverter;
        }

        public DataResponseDelivery EntityToDTO(Delivery delivery)
        {
            return new DataResponseDelivery
            {
                ActualDeliveryTime = delivery.ActualDeliveryTime,
                DeliveryAddress = delivery.DeliveryAddress,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
                Id = delivery.Id,
                EstimateDeliveryTime = delivery.EstimateDeliveryTime,
                Customer = _customerConverter.EntityToDTO(_baseCustomerRepository.GetAsync(x => x.Id == delivery.CustomerId).Result),
                Deliver = _userConverter.EntityToDTO(_baseUserRepository.GetAsync(x => x.Id == delivery.DeliverId).Result),
                Project = _projectConverter.EntityToDTO(_baseProjectRepository.GetAsync(x => x.Id == delivery.ProjectId).Result),
                ShippingMethodName = _baseShippingMethodRepository.GetAsync(x => x.Id == delivery.ShippingMethodId).Result.ShippingMethodName,
            };
        }
    }
}
