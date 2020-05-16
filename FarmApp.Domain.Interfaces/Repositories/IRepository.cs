using FarmApp.Domain.Core.Interfaces;
using System;

namespace FarmApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Помечает сущность на добавление
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Create(TEntity entity);

        /// <summary>
        /// Помечает сущность на редактирование
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Update(TEntity entitiy);

        /// <summary>
        /// Помечает сущность на удаление
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Delete(TEntity entitiy);
    }
}
