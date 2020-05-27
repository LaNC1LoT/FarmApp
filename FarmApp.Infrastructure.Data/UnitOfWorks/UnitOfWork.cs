using FarmApp.Domain.Interfaces.Repositories;
using FarmApp.Domain.Interfaces.UnitOfWorks;
using FarmApp.Infrastructure.Data.Contexts;
using FarmApp.Infrastructure.Data.Repositoreies;
using System.Threading;
using System.Threading.Tasks;

namespace FarmApp.Infrastructure.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructor

        private readonly FarmAppContext farmAppContext;
        public UnitOfWork(FarmAppContext context)
        {
            farmAppContext = context;
        }

        #endregion

        #region Repositories

        private IUserRepository userRepository;
        public virtual IUserRepository UserRepository
        { 
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(farmAppContext);
                return userRepository;
            }
        }

        #endregion

        #region Save Entity

        public virtual void Save()
        {
            farmAppContext.SaveChanges();
        }

        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return farmAppContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    farmAppContext?.Dispose();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                userRepository = null;

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~UnitOfWork()
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
