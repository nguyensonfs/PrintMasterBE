using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class UserConverter
    {
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Notification> _notificationRepository;
        private readonly NotificationConverter _notificationConverter;
        public UserConverter(IBaseRepository<Team> baseTeamRepository,
                             IBaseRepository<Notification> notificationRepository,
                             NotificationConverter notificationConverter)
        {
            _baseTeamRepository = baseTeamRepository;
            _notificationRepository = notificationRepository;
            _notificationConverter = notificationConverter;
        }
        public DataResponseUser EntityToDTO(User user)
        {
            var teamName = "ADMIN";
            if (user.TeamId.HasValue)
            {
                var team = _baseTeamRepository.GetAsync(x => x.Id == user.TeamId.Value).Result;
                teamName = team?.Name ?? "ADMIN";
            }

            var notifications =  _notificationRepository.GetAllAsync(x => x.UserId == user.Id).Result;
            var notificationDTOs = notifications.Select(x => _notificationConverter.EntityToDTO(x));

            return new DataResponseUser
            {
                CreateTime = user.CreateTime,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FullName = user.FullName,
                Avatar = user.Avatar,
                Id = user.Id,
                Gender = user.Gender.ToString(),
                PhoneNumber = user.PhoneNumber,
                Status = user.Status.ToString(),
                TeamName = teamName,
                Notifications = notificationDTOs
            };
        }
    }
}
