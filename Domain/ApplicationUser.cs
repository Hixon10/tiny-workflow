using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Созданные пользователем заявки
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; }

        /// <summary>
        /// Добавленные в лог записи
        /// </summary>
        public virtual ICollection<ApplicationLog> ApplicationLogs { get; set; }

        /// <summary>
        /// Может одобрять заявку?
        /// </summary>
        /// <returns></returns>
        public bool CanApproveApplication()
        {
            return Roles.Any(role => ApplicationRole.CanApproveApplication(role.Role));
        }

        /// <summary>
        /// Могу выплачивать деньги?
        /// </summary>
        /// <returns></returns>
        public bool CanGiveMoney()
        {
            return Roles.Any(role => ApplicationRole.CanGiveMoney(role.Role));
        }

        /// <summary>
        /// Могу просматривать все объявления?
        /// </summary>
        /// <returns></returns>
        public bool CanViewAllApplications()
        {
            return Roles.Any(role => ApplicationRole.CanViewAllApplications(role.Role));
        }

        /// <summary>
        /// Получить старшую роль пользователя
        /// </summary>
        /// <returns>Роль</returns>
        public IdentityRole GetMajorRole()
        {
            var roles = Roles.Select(p => p.Role);

            roles = roles.OrderByDescending(
                p => (ApplicationRole.RoleTypes) Enum.Parse(typeof (ApplicationRole.RoleTypes), p.Name, false));

            return roles.FirstOrDefault(p => (ApplicationRole.RoleTypes)Enum.Parse(typeof(ApplicationRole.RoleTypes), p.Name, false) != ApplicationRole.RoleTypes.Admin);
        }
    }
}