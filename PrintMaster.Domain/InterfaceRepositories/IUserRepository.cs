using PrintMaster.Domain.Entities;

namespace PrintMaster.Domain.InterfaceRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByPhone(string phoneNumber);
        Task AddRolesToUser(User user, List<string> listRoles);
        Task<IEnumerable<string>> GetRolesOfUserAsync(User user);
        Task DeleteRolesOfUserAsync(User user, List<string> listRoles);

    }
}
