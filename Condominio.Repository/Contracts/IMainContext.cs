using Condominio.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Repository.Contracts
{
    public interface IMainContext
    {
        public Task<int> SaveChangesAsync();
        public int SaveChanges();

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity;
    }
}
