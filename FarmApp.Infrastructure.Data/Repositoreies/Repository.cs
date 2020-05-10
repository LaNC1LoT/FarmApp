using FarmApp.Domain.Core.Entity;
using FarmApp.Domain.Core.Interfaces;
using FarmApp.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FarmApp.Infrastructure.Data.Repositoreies
{
    public class Repository<TContext> : IRepository
        where TContext : DbContext, new()
    {
        protected Repository()
        {
            farmAppContext = new TContext();
        }

        protected readonly TContext farmAppContext;

        public virtual void Create<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            farmAppContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            farmAppContext.Set<TEntity>().Attach(entity);
            farmAppContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var dbSet = farmAppContext.Set<TEntity>();
            if (farmAppContext.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            farmAppContext.SaveChanges();
        }

        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return farmAppContext.SaveChangesAsync(cancellationToken);
        }

        #region IDisposable Support

        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    farmAppContext.Dispose();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~Repository()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
