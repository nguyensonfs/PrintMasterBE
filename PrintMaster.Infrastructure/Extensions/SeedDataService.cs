using PrintMaster.Commons.Enumerates;
using PrintMaster.Commons.Utils;
using PrintMaster.Domain.Entities;
using PrintMaster.Infrastructure.DataContext;

namespace PrintMaster.Infrastructure.Extensions
{
    public class SeedDataService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public void Initialize()
        {

            if (!_context.Roles.Any())
            {

                _context.Roles.AddRange(
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Admin", RoleName = "Admin" },
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Manager", RoleName = "Manager" },
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Leader", RoleName = "Leader" },
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Designer", RoleName = "Designer" },
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Deliver", RoleName = "Deliver" },
                    new Role() { Id = Guid.NewGuid(), RoleCode = "Employee", RoleName = "Employee" }
                );

                _context.SaveChanges();
            }
            if (!_context.Teams.Any())
            {

                _context.Teams.AddRange(
                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Description = "Phòng ban kinh doanh",
                        Name = "Sales"
                    },
                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Description = "Phòng ban kỹ thuật",
                        Name = "Technical"
                    },
                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Description = "Phòng ban giao hàng",
                        Name = "Delivery"
                    }
                );

                _context.SaveChanges();
            }
            if (!_context.ResourceTypes.Any())
            {
                _context.ResourceTypes.AddRange(
                    new ResourceType()
                    {
                        NameOfResourceType = "Machines",
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.Users.Any())
            {
                var adminRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Admin");
                var managerRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Manager");
                var employeeRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Employee");
                var leaderRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Leader");

                var salesTeam = _context.Teams.FirstOrDefault(t => t.Name == "Sales");
                var technicalTeam = _context.Teams.FirstOrDefault(t => t.Name == "Technical");

                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = SecurityUtilities.HashPassword("admin123"),
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Male,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1985, 1, 1),
                    CreateTime = DateTime.Now,
                    TeamId = null,
                    PhoneNumber = "123456789",
                    Permissions = adminRole != null ? new List<Permission>
                    {
                        new() { Role = adminRole },
                        new() { Role = managerRole },
                        new() { Role = employeeRole}
                    } : null
                };

                var saleManagerUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "manager",
                    Password = SecurityUtilities.HashPassword("manager123"),
                    Email = "manager@gmail.com",
                    FullName = "Sales Manager",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = salesTeam?.Id,
                    PhoneNumber = "987654321",
                    Permissions = managerRole != null ? new List<Permission>
                    {
                        new() { Role = managerRole },
                        new() { Role = leaderRole }
                    } : null
                };

                var technicalManagerUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "technical",
                    Password = SecurityUtilities.HashPassword("technical123"),
                    Email = "technical@gmail.com",
                    FullName = "Technical Manager",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = technicalTeam?.Id,
                    PhoneNumber = "987654321",
                    Permissions = managerRole != null ? new List<Permission>
                    {
                        new() { Role = managerRole },
                        new() { Role = leaderRole }
                    } : null
                };

                _context.Users.AddRange(adminUser, saleManagerUser, technicalManagerUser);

                _context.SaveChanges();
            }
            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        FullName = "Trần Minh Quân",
                        PhoneNumber = "0328033706",
                        Address = "Bình Dương, Việt Nam",
                        Email = "minhquan@gmail.com"
                    }, new Customer
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        FullName = "Nguyễn Minh Long",
                        PhoneNumber = "0328033707",
                        Address = "Long An, Việt Nam",
                        Email = "minhlong@gmail.com"
                    }, new Customer
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        FullName = "Trần Văn An",
                        PhoneNumber = "0328033702",
                        Address = "Hải Dương, Việt Nam",
                        Email = "tranvanan@gmail.com"
                    }, new Customer
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        FullName = "Nguyễn Văn Tuấn",
                        PhoneNumber = "0328033704",
                        Address = "Nghệ An, Việt Nam",
                        Email = "nguyenvantuan@gmail.com"
                    }, new Customer
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        FullName = "Nguyễn Thị Thuỷ",
                        PhoneNumber = "032803399",
                        Address = "Hồ Chí Minh, Việt Nam",
                        Email = "nguyenthuy@gmail.com"
                    });
                _context.SaveChanges();
            }
        }
    }
}
