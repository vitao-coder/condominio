﻿using Condominio.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Condominio.Repository.Contracts;
namespace Condominio.Repository.Contracts
{
    public interface IBaseRepository<TEntity>
       where TEntity : BaseEntity       
    {
        public Task AddAsync(TEntity entity);

        public Task AddAsync(IEnumerable<TEntity> entities);

        public Task UpdateAsync(TEntity entity);

        public Task UpdateAsync(IEnumerable<TEntity> entities);

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where, bool asNoTracking = true);

        public Task<IEnumerable<TEntity>> GetAsync(bool asNoTracking = true);

        public Task<TEntity> GetAsync(long Id, bool asNoTracking = true);

        public Task DeleteAsync(Guid id);

        public Task DeleteAsync(TEntity entity);

        public Task DeleteAsync(Func<TEntity, bool> where);
    }
}
