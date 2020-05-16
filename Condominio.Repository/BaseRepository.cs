using Condominio.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Condominio.Repository.Contracts;
using Condominio.Repository;

namespace Condominio.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
          where TEntity : BaseEntity          
    {
        protected DbSet<TEntity> DbSet;
        protected IMainContext MainContext;

        public BaseRepository(IMainContext mainContext)
        {
            MainContext = mainContext;
            DbSet = MainContext.GetSet<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            return await DbSet.AsNoTracking().Where(where).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(bool asNoTracking = true)
        {
            return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(long Id, bool asNoTracking = true)
        {
            return asNoTracking
                ? await DbSet.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == Id).ConfigureAwait(false)
                : await DbSet.FindAsync(Id).ConfigureAwait(false);
        }

        public virtual Task DeleteAsync(Guid Id)
        {
            var existing = DbSet.Find(Id);
            if (existing != null) DbSet.Remove(existing);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(Func<TEntity, bool> where)
        {
            DbSet.RemoveRange(DbSet.ToList().Where(where));
            return Task.CompletedTask;
        }

    }
}
