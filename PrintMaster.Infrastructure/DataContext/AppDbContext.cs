using Microsoft.EntityFrameworkCore;
using PrintMaster.Domain.Entities;

namespace PrintMaster.Infrastructure.DataContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext() { }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Design> Designs { get; set; }
        public virtual DbSet<ImportCoupon> ImportCoupons { get; set; }
        public virtual DbSet<KeyPerformanceIndicator> KeyPerformanceIndicators { get; set; }
        public virtual DbSet<PrintJob> PrintJobs { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceProperty> ResourceProperties { get; set; }
        public virtual DbSet<ResourcePropertyDetail> ResourcePropertyDetails { get; set; }
        public virtual DbSet<ResourceForPrintJob> ResourceForPrintJobs { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }

        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
