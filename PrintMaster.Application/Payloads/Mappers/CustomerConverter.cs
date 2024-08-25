using PrintMaster.Application.Payloads.ResponseModels.DataCustomer;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class CustomerConverter
    {
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        public CustomerConverter(IBaseRepository<Customer> baseCustomerRepository)
        {
            _baseCustomerRepository = baseCustomerRepository;
        }
        public DataResponseCustomer EntityToDTO(Customer customer)
        {
            return new DataResponseCustomer
            {
                Address = customer.Address,
                FullName = customer.FullName,
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };
        }
    }
}
