using PrintMaster.Application.Payloads.ResponseModels.DataTeam;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class TeamConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly UserConverter _converter;
        public TeamConverter(IBaseRepository<User> baseUserRepository,
                             UserConverter converter,
                             IBaseRepository<Team> baseTeamRepository)
        {
            _baseUserRepository = baseUserRepository;
            _converter = converter;
            _baseTeamRepository = baseTeamRepository;
        }
        public DataResponseTeam EntityToDTO(Team entity)
        {
            var teamItem = _baseTeamRepository.GetByIDAsync(entity.Id).Result;
            var user = _baseUserRepository.GetByIDAsync(entity.ManagerId).Result;
            DataResponseTeam? team;
            if (_baseUserRepository.GetAllAsync(x => x.TeamId == entity.Id).Result
                != null)
            {
                team = new DataResponseTeam
                {
                    CreateTime = entity.CreateTime,
                    Description = entity.Description,
                    Id = entity.Id,
                    ManagerName = user != null ? user.FullName : "",
                    Name = entity.Name,
                    NumberOfMember = entity.NumberOfMember,
                    UpdateTime = entity.UpdateTime,
                    Users = (IQueryable<ResponseModels.DataUser.DataResponseUser>?)_baseUserRepository.GetAllAsync(x => x.TeamId == entity.Id).Result
                .Select(x => _converter.EntityToDTO(x))
                };
            }
            else
            {
                team = new DataResponseTeam
                {
                    CreateTime = entity.CreateTime,
                    Description = entity.Description,
                    Id = entity.Id,
                    ManagerName = user != null ? user.FullName : "",
                    Name = entity.Name,
                    NumberOfMember = entity.NumberOfMember,
                    UpdateTime = entity.UpdateTime,
                    Users = null
                };
            }

            return team;
        }
    }
}
