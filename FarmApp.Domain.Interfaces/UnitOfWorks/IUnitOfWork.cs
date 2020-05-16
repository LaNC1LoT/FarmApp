using FarmApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FarmApp.Domain.Interfaces.UnitOfWorks
{
    /// <summary>
    /// Pattern UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Получает репозиторий для работы с сущностью User
        /// </summary>
        IUserRepository UserRepository {get;}

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        void Save();

        /// <summary>
        /// Асинхронное сохранение сущности, в возможностью отмены
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Task</returns>
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
