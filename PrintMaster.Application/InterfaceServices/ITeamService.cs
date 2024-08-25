using PrintMaster.Application.Payloads.RequestModels.TeamRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataTeam;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface ITeamService
    {
        Task<ResponseObject<DataResponseTeam>> CreateTeam(Request_CreateTeam request);
        Task<ResponseObject<DataResponseTeam>> UpdateTeam(Guid id,Request_UpdateTeam request);
        Task<ResponseMessages> DeleteTeam(Guid teamId);
        Task<IQueryable<DataResponseTeam>> GetAllTeams(string? name);
        Task<ResponseObject<DataResponseTeam>> GetTeamById(Guid teamId);
        Task<ResponseObject<DataResponseTeam>> ChangeManagerForTeam(Guid teamId, Guid managerId);
        Task<IQueryable<DataResponseUser>> GetAllUserByTeam(Guid teamId);
    }
}
