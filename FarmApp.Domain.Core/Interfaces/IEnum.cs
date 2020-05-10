using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для импорта Enum в БД
    /// </summary>
    public interface IEnum
    {
        /// <summary>
        /// Получает или устанавливает Id записи в БД
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает значение Enum
        /// </summary>
        string EnumName { get; set; }

        /// <summary>
        /// Получает или устанавливает описание
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Получает или устанавливает статус записи
        /// </summary>
        bool? IsDeleted { get; set; }
    }
}
