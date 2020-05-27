using FarmApp.Domain.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

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


        //TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        //int Count(Expression<Func<TEntity, int>> filter = null);
    }
}
