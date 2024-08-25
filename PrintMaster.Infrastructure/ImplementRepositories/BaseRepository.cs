using Microsoft.EntityFrameworkCore;
using PrintMaster.Domain.InterfaceRepositories;
using PrintMaster.Infrastructure.DataContext;
using System.Linq.Expressions;

namespace PrintMaster.Infrastructure.ImplementRepositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        #region Private Variables

        protected IDbContext _IdbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;

        #endregion Private Variables

        #region Public/Protected Properties
        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }

                return _dbSet;
            }
        }
        #endregion
        public BaseRepository(IDbContext dbContext)
        {
            _IdbContext = dbContext;
            _dbContext = (DbContext)dbContext;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = predicate != null ? DBSet.Where(predicate) : DBSet;
            return query.AsQueryable();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? DBSet.Where(predicate) : DBSet.AsQueryable();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DBSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IdbContext.CommitChangesAsync();

            return entity;
        }

        public async Task<IQueryable<TEntity>> CreateAsync(IQueryable<TEntity> entity)
        {
            DBSet.AddRange(entity);
            await _IdbContext.CommitChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IdbContext.CommitChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = predicate != null ? DBSet.Where(predicate) : DBSet;
            var dataEntities = query;
            if (dataEntities != null)
            {
                DBSet.RemoveRange(dataEntities);
                await _IdbContext.CommitChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var data = await DBSet.FindAsync(id);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, params object[] keyValues)
        {
            var data = await DBSet.FindAsync(keyValues);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<IQueryable<TEntity>> UpdateAsync(IQueryable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _IdbContext.CommitChangesAsync();
            return entities;
        }

        public async Task<TEntity> GetByIDAsync(Guid id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIDAsync(Guid? id)
        {
            return await DBSet.FindAsync(id);
        }

        public void ClearTrackedChanges()
        {
            var changedEntriesCopy = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = predicate != null ? DBSet.Where(predicate) : DBSet;
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(List<string> includes, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, predicate);
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrWhiteSpace(include))
            {
                query = BuildQueryable(new List<string> { include }, predicate);
                return await query.CountAsync();
            }
            else
            {
                return await CountAsync(predicate);
            }
        }

        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DBSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null && includes.Count > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }

            return query;
        }
    }
}
