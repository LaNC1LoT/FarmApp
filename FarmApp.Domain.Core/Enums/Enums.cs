using FarmApp.Domain.Core.Attributes;

namespace FarmApp.Domain.Core.Enums
{
    /// <summary>
    /// Роли
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// Администратор
        /// </summary>
        [EnumHelper("Администратор", false)]
        Admin = 1,

        /// <summary>
        /// Пользователь
        /// </summary>
        [EnumHelper("Пользователь", false)]
        User = 2,

        /// <summary>
        /// Тест
        /// </summary>
        [EnumHelper("Тест", true)]
        Test = 3
    }

    /// <summary>
    /// Типы регионов
    /// </summary>
    public enum RegionTypes
    {
        /// <summary>
        /// Страна
        /// </summary>
        [EnumHelper("Страна", false)]
        County = 1,

        /// <summary>
        /// Регион
        /// </summary>
        [EnumHelper("Регион", false)]
        Region = 2,

        /// <summary>
        /// Город
        /// </summary>
        [EnumHelper("Город", false)]
        Sity = 3,

        /// <summary>
        /// Деревня
        /// </summary>
        [EnumHelper("Деревня", false)]
        Village = 4,

        /// <summary>
        /// Микрорайон
        /// </summary>
        [EnumHelper("Микрорайон", false)]
        Microdistrict = 5
    }

    public enum Api
    {
        GetUser = 1,
        CreateUser = 2,
        UpdateUser = 3,
        DeleteUser = 4,
        AutorizationUser = 5
    }

    public enum Result
    {
        Success,
        Error,
        UnknownError,
        NotFound,
        NotAccess,
        NotValid
    }
}
