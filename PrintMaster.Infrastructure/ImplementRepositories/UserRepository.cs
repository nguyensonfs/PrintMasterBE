using Microsoft.EntityFrameworkCore;
using PrintMaster.Commons.Utils;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;
using PrintMaster.Infrastructure.DataContext;

namespace PrintMaster.Infrastructure.ImplementRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRolesToUser(User user, List<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser = await GetRolesOfUserAsync(user);
                if (await StringUltilities.IsStringInListAsync(role, rolesOfUser.ToList()))
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    var permission = await _context.Permissions.Where(record => record.UserId == user.Id && record.RoleId == roleItem.Id).SingleOrDefaultAsync();
                    permission.RoleId = roleItem.Id;
                    _context.Permissions.Update(permission);
                }
                else
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    if (roleItem == null)
                    {
                        throw new ArgumentNullException("Không có quyền này");
                    }
                    _context.Permissions.Add(new Permission { 
                        UserId = Guid.Parse((user.Id).ToString()), 
                        RoleId = Guid.Parse((roleItem.Id).ToString()) 
                    });
                }
            }
            _context.SaveChanges();
        }

        public async Task DeleteRolesOfUserAsync(User user, List<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser = await GetRolesOfUserAsync(user);
                var listPermission = new List<Permission>();
                if (await StringUltilities.IsStringInListAsync(role, rolesOfUser.ToList()))
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    var permission = await _context.Permissions.SingleOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == roleItem.Id);
                    if (permission != null)
                    {
                        listPermission.Add(permission);
                    }
                }
                else
                {

                    throw new ArgumentNullException("Không có quyền này");

                }
                _context.Permissions.RemoveRange(listPermission);
            }
            _context.SaveChanges();
        }

        public async Task<IEnumerable<string>> GetRolesOfUserAsync(User user)
        {
            List<string> roles = new List<string>();
            var listRoles =  _context.Permissions.Where(x => x.UserId == user.Id).AsQueryable();
            foreach (var item in listRoles.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                roles.Add(role.RoleCode);
            }
            return roles.AsEnumerable();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByPhone(string phoneNumber)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()));
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            return user;
        }
    }
}
