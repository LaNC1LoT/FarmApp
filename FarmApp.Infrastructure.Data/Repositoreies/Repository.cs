using FarmApp.Domain.Core.Interfaces;
using FarmApp.Domain.Interfaces.Repositories;
using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FarmApp.Infrastructure.Data.Repositoreies
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected FarmAppContext farmAppContext;

        public virtual void Create(TEntity entity)
        {
            farmAppContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            farmAppContext.Set<TEntity>().Attach(entity);
            farmAppContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = farmAppContext.Set<TEntity>();
            if (farmAppContext.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
        }
    }
}
