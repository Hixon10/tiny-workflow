using System.Collections.Generic;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис для работы с Ролями системы
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Изменить приоритеты ролей
        /// </summary>
        /// <param name="priorities">Словарь вида роль-приоритет</param>
        void ChangeRolesPriority(Dictionary<ApplicationRole.RoleTypes, ApplicationRole.Priorities> priorities);

        /// <summary>
        /// Получить все роли системы
        /// </summary>
        /// <returns>Роли</returns>
        List<ApplicationRole> GetRoles();

        /// <summary>
        /// Получить Роли пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Роли</returns>
        List<ApplicationRole.RoleTypes> GetRolesByUser(ApplicationUser user);

        /// <summary>
        /// Получить Роль по типу
        /// </summary>
        /// <param name="type">Тип Роли</param>
        /// <returns>Роль</returns>
        ApplicationRole GetRoleByType(ApplicationRole.RoleTypes type);
    }
}
