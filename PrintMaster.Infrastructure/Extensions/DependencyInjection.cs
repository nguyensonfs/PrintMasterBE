using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrintMaster.Commons.Constants;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;
using PrintMaster.Infrastructure.DataContext;
using PrintMaster.Infrastructure.ImplementRepositories;

namespace PrintMaster.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(AppSettingKeys.DEFAULT_CONNECTION);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IBaseRepository<User>,BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Bill>,BaseRepository<Bill>>();
            services.AddScoped<IBaseRepository<Team>,BaseRepository<Team>>();
            services.AddScoped<IBaseRepository<RefreshToken>,BaseRepository<RefreshToken>>();
            services.AddScoped<IBaseRepository<Permission>,BaseRepository<Permission>>();
            services.AddScoped<IBaseRepository<ConfirmEmail>,BaseRepository<ConfirmEmail>>();
            services.AddScoped<IBaseRepository<Role>,BaseRepository<Role>>();
            services.AddScoped<IBaseRepository<Notification>,BaseRepository<Notification>>();
            services.AddScoped<IBaseRepository<Customer>,BaseRepository<Customer>>();
            services.AddScoped<IBaseRepository<Design>,BaseRepository<Design>>();
            services.AddScoped<IBaseRepository<Project>,BaseRepository<Project>>();
            services.AddScoped<IBaseRepository<PrintJob>,BaseRepository<PrintJob>>();
            services.AddScoped<IBaseRepository<ShippingMethod>,BaseRepository<ShippingMethod>>();
            services.AddScoped<IBaseRepository<ResourceForPrintJob>,BaseRepository<ResourceForPrintJob>>();
            services.AddScoped<IBaseRepository<ResourcePropertyDetail>,BaseRepository<ResourcePropertyDetail>>();
            services.AddScoped<IBaseRepository<ResourceType>,BaseRepository<ResourceType>>();
            services.AddScoped<IBaseRepository<Resource>,BaseRepository<Resource>>();
            services.AddScoped<IBaseRepository<ResourceProperty>,BaseRepository<ResourceProperty>>();
            services.AddScoped<IBaseRepository<KeyPerformanceIndicator>,BaseRepository<KeyPerformanceIndicator>>();
            services.AddScoped<IBaseRepository<Delivery>,BaseRepository<Delivery>>();
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<IDbContext, AppDbContext>();
            services.AddTransient<SeedDataService>();
        }
    }
}
