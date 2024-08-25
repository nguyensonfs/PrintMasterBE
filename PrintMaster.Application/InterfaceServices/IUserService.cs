using PrintMaster.Application.Payloads.RequestModels.InputRequests;
using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Application.Payloads.RequestModels.UserRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IUserService
    {
        Task<IQueryable<DataResponseUser>> GetAllUsers(Request_InputUser request);
        Task<DataResponseUser> GetUserById(Guid id);
        Task<ResponseObject<DataResponseUser>> UpdateUser(Guid id, Request_UpdateUser request);
        Task<ResponseMessages> ChangeDepartmentForUser(Guid EmployeeId, Request_ChangeDepartmentForUser request);
        Task<IQueryable<string>> GetRolesByUserId(Guid userId);
        Task<IQueryable<DataResponseUser>> GetAllUserContainsManagerRole();
        Task<IQueryable<DataResponseUser>> GetAllUserContainsLeaderRole();
        Task<IQueryable<DataResponseUser>> GetAllEmployee();
    }
}
