using FarmApp.Domain.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FarmApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Помечает сущность на добавление
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Create<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Помечает сущность на редактирование
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Update<TEntity>(TEntity entitiy) where TEntity : class, IEntity;

        /// <summary>
        /// Помечает сущность на удаление
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Delete<TEntity>(TEntity entitiy) where TEntity : class, IEntity;
     
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
