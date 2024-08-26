using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataCustomer;

namespace PrintMaster.Application.InterfaceServices
{
    public interface ICustomerService
    {
        Task<IQueryable<DataResponseCustomer>> GetAllCustomers(Request_InputCustomer request);
    }
}
