using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataCustomer;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.ImplementServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        public CustomerService(IBaseRepository<Customer> baseCustomerRepository)
        {
            _baseCustomerRepository = baseCustomerRepository;
        }

        public async Task<IQueryable<DataResponseCustomer>> GetAllCustomers(Request_InputCustomer request)
        {
            try
            {
                var query = await _baseCustomerRepository.GetAllAsync(x => x.IsDeleted == false);
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    query = query.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
                }
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(x => x.FullName.ToLower().Contains(request.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(x => x.Address.ToLower().Contains(request.Address.ToLower()));
                }
                return query.Select(x => new DataResponseCustomer
                {
                    Address = x.Address,
                    FullName = x.FullName,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, nameof(request));
            }
        }
    }
}
