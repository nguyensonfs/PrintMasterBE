using System.Linq.Expressions;

namespace PrintMaster.Domain.InterfaceRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IQueryable<TEntity>> CreateAsync(IQueryable<TEntity> entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, params Object[] keyValues);
        Task<IQueryable<TEntity>> UpdateAsync(IQueryable<TEntity> entities);
        Task<TEntity> GetByIDAsync(Guid id);
        Task<TEntity> GetByIDAsync(Guid? id);
        void ClearTrackedChanges();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(List<string> includes, Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> predicate = null);
    }
}
