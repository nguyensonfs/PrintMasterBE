using Microsoft.Extensions.DependencyInjection;
using PrintMaster.Application.ImplementServices;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;

namespace PrintMaster.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterConverter(this IServiceCollection services)
        {
            services.AddScoped<UserConverter>();
            services.AddScoped<TeamConverter>();
            services.AddScoped<NotificationConverter>();
            services.AddScoped<ProjectConverter>();
            services.AddScoped<CustomerConverter>();
            services.AddScoped<DesignConverter>();
            services.AddScoped<ResourceForPrintJobConverter>();
            services.AddScoped<PrintJobConverter>();
            services.AddScoped<ResourcePropertyDetailConverter>();
            services.AddScoped<DeliveryConverter>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBlacklistedTokenService, BlacklistedTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDesignService, DesignService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();
            services.AddScoped<IPrintJobService, PrintJobService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
