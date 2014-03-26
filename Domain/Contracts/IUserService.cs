using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис для работы с Пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить Пользователя по Логину
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Пользователь</returns>
        ApplicationUser GetUserByLogin(string login);

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns>Все пользователи</returns>
        List<ApplicationUser> GetAllUsers(); 

        /// <summary>
        /// Изменить роли пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="roles">Список ролей</param>
        void EditUserRoles(ApplicationUser user, IEnumerable<IdentityRole> roles);
    }
}
