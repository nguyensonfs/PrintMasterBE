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
                        NameOfResourceType = "Consumable",
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    },
                    new ResourceType()
                    {
                        NameOfResourceType = "Non-consumable",
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.Resources.Any())
            {
                var resourceTypeConsumable = _context.ResourceTypes.FirstOrDefault(r => r.NameOfResourceType == "Consumable");
                var resourceTypeNonConsumable = _context.ResourceTypes.FirstOrDefault(r => r.NameOfResourceType == "Non-consumable");
                _context.Resources.AddRange(
                    new Resource()
                    {
                        ResourceName = "Machinery",
                        ResourceTypeId = resourceTypeNonConsumable.Id,
                        Image = "https://www.anlocviet.vn/upload/images/van-phong-pham-quan-hai-ba-trung-3.jpg",
                        AvailableQuantity = 10,
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    },
                    new Resource()
                    {
                        ResourceName = "Office supplies",
                        ResourceTypeId = resourceTypeConsumable.Id,
                        Image = "https://png.pngtree.com/thumb_back/fw800/background/20230722/pngtree-metallic-machinery-a-3d-rendered-illustration-for-your-background-image_3781977.jpg",
                        AvailableQuantity = 350,
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.ResourceProperties.Any())
            {
                var resourceMachinery = _context.Resources.FirstOrDefault(r => r.ResourceName == "Machinery");
                var resourceOfficeSupplies = _context.Resources.FirstOrDefault(r => r.ResourceName == "Office supplies");
                _context.ResourceProperties.AddRange(
                    new ResourceProperty()
                    {
                        ResourcePropertyName = "Giấy",
                        Quantity = 300,
                        ResourceId = resourceOfficeSupplies.Id,
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    },
                    new ResourceProperty()
                    {
                        ResourcePropertyName = "Mực",
                        Quantity = 50,
                        ResourceId = resourceOfficeSupplies.Id,
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    },
                    new ResourceProperty()
                    {
                        ResourcePropertyName = "Máy in",
                        Quantity = 10,
                        ResourceId = resourceMachinery.Id,
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.ResourcePropertyDetails.Any())
            {
                var resourcePrinterPaper = _context.ResourceProperties.FirstOrDefault(r => r.ResourcePropertyName == "Giấy");
                var resourcePrinterInk = _context.ResourceProperties.FirstOrDefault(r => r.ResourcePropertyName == "Mực");
                var resourcePrinter = _context.ResourceProperties.FirstOrDefault(r => r.ResourcePropertyName == "Máy in");
                _context.ResourcePropertyDetails.AddRange(
                #region Giấy
                    new ResourcePropertyDetail()
                    {
                        Name = "Giấy A4",
                        Quantity = 100,
                        ResourcePropertyId = resourcePrinterPaper.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Giấy A3",
                        Quantity = 100,
                        ResourcePropertyId = resourcePrinterPaper.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Giấy A2",
                        Quantity = 100,
                        ResourcePropertyId = resourcePrinterPaper.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                #endregion
                #region Mực
                    new ResourcePropertyDetail()
                    {
                        Name = "Mực Xanh",
                        Quantity = 5,
                        ResourcePropertyId = resourcePrinterInk.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Mực Đỏ",
                        Quantity = 10,
                        ResourcePropertyId = resourcePrinterInk.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Mực Tím",
                        Quantity = 10,
                        ResourcePropertyId = resourcePrinterInk.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Mực Vàng",
                        Quantity = 25,
                        ResourcePropertyId = resourcePrinterInk.Id,
                        Id = Guid.NewGuid(),
                        Price = 1000,
                        IsDeleted = false,
                    },
                #endregion
                #region Máy in
                    new ResourcePropertyDetail()
                    {
                        Name = "Máy in 3D",
                        Quantity = 5,
                        ResourcePropertyId = resourcePrinter.Id,
                        Id = Guid.NewGuid(),
                        Price = 24000000,
                        IsDeleted = false,
                    },
                    new ResourcePropertyDetail()
                    {
                        Name = "Máy in laser",
                        Quantity = 5,
                        ResourcePropertyId = resourcePrinter.Id,
                        Id = Guid.NewGuid(),
                        Price = 240000000,
                        IsDeleted = false,
                    }
                    #endregion
                    );
                _context.SaveChanges();
            }
            if (!_context.Users.Any())
            {
                var adminRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Admin");
                var managerRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Manager");
                var employeeRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Employee");
                var leaderRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Leader");
                var designerRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Designer");
                var deliverRole = _context.Roles.FirstOrDefault(r => r.RoleCode == "Deliver");

                var salesTeam = _context.Teams.FirstOrDefault(t => t.Name == "Sales");
                var technicalTeam = _context.Teams.FirstOrDefault(t => t.Name == "Technical");
                var deliveryTeam = _context.Teams.FirstOrDefault(t => t.Name == "Delivery");

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
                    PhoneNumber = "0987654321",
                    Permissions = managerRole != null ? new List<Permission>
                    {
                        new() { Role = managerRole },
                        new() { Role = leaderRole }
                    } : null
                };

                var deliverManagerUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "deliverleader",
                    Password = SecurityUtilities.HashPassword("deliverleader123"),
                    Email = "deliverleader@gmail.com",
                    FullName = "Deliver Manager",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = deliveryTeam?.Id,
                    PhoneNumber = "0987654333",
                    Permissions = managerRole != null ? new List<Permission>
                    {
                        new() { Role = managerRole },
                        new() { Role = leaderRole }
                    } : null
                };

                var designerUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "designer",
                    Password = SecurityUtilities.HashPassword("designer123"),
                    Email = "designer@gmail.com",
                    FullName = "Designer",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = technicalTeam?.Id,
                    PhoneNumber = "0987654323",
                    Permissions = designerRole != null ? new List<Permission>
                    {
                        new() { Role = designerRole },
                        new() { Role = employeeRole }
                    } : null
                };

                var deliverUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "deliver",
                    Password = SecurityUtilities.HashPassword("deliver123"),
                    Email = "deliver@gmail.com",
                    FullName = "Deliver",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = deliveryTeam?.Id,
                    PhoneNumber = "0987654324",
                    Permissions = deliverRole != null ? new List<Permission>
                    {
                        new() { Role = deliverRole },
                        new() { Role = employeeRole }
                    } : null
                };

                var saleUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "saler",
                    Password = SecurityUtilities.HashPassword("saler123"),
                    Email = "saler@gmail.com",
                    FullName = "Saler",
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    Gender = GenderEnum.Female,
                    Status = UserStatus.Activated,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    CreateTime = DateTime.Now,
                    TeamId = salesTeam?.Id,
                    PhoneNumber = "0987654328",
                    Permissions = employeeRole != null ? new List<Permission>
                    {
                        new() { Role = employeeRole }
                    } : null
                };

                _context.Users.AddRange(adminUser, saleManagerUser, technicalManagerUser, designerUser, deliverUser, saleUser, deliverManagerUser);

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
                        Email = "minhquan@gmail.com",
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
            };
            if (!_context.ShippingMethods.Any())
            {
                _context.ShippingMethods.AddRange(
                    new ShippingMethod
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        ShippingMethodName = "Chuyển phát nhanh"
                    },
                    new ShippingMethod
                    {
                        Id = Guid.NewGuid(),
                        IsDeleted = false,
                        ShippingMethodName = "Vận chuyển hoả tốc"
                    }
                    );
                _context.SaveChanges();
            }
        }
    }
}
