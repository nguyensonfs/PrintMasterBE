using PrintMaster.Application.Payloads.RequestModels.UserRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataLogin;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Domain.Entities;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseUser>> Register(Request_Register request);
        Task<ResponseObject<DataResponseLogin>> Login(Request_Login request);
        Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user);
        Task<ResponseMessages> ChangePassword(Guid userId, Request_ChangePassword request);
        Task<ResponseMessages> ForgotPassword(string email);
        Task<ResponseMessages> ConfirmCreateNewPassword(Request_ConfirmCreateNewPassword request);
        Task<ResponseMessages> ConfirmRegister(string confirmCode);
    }
}
