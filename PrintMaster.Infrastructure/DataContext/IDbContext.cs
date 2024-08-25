using Microsoft.EntityFrameworkCore;

namespace PrintMaster.Infrastructure.DataContext
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> SetEntity<TEntity>() where TEntity : class;
        Task<int> CommitChangesAsync();
    }
}
