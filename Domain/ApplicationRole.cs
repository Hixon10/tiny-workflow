using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain
{
    /// <summary>
    /// Роль Пользователя
    /// </summary>
    public class ApplicationRole : IdentityRole, IEquatable<ApplicationRole>
    {
        static readonly List<RoleTypes> whoCanApproveApplication = new List<RoleTypes> { RoleTypes.Accountant, RoleTypes.Director };

        /// <summary>
        /// Доступные статусы одобрения заявки
        /// </summary>
        public enum Priorities
        {
            First = 1,
            Second = 2,
            NotApprove = 0
        };

        /// <summary>
        /// Существующие роли в системе
        /// </summary>
        public enum RoleTypes
        {
            Employee = 1,
            Accountant = 2,
            Director = 3,
            Admin = 4
        };

        /// <summary>
        /// Приоритет Роли
        /// </summary>
        [Required]
        public Priorities Priority { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        [Required]
        public string Description { get; set; }

        public static int HowManyRolesCanApproveApplication()
        {
            return whoCanApproveApplication.Count;
        }

        /// <summary>
        /// Может одобрять заявку?
        /// </summary>
        /// <returns></returns>
        public bool CanApproveApplication()
        {
            return CanApproveApplication(Name);
        }

        /// <summary>
        /// Может одобрять заявку?
        /// </summary>
        /// <param name="role">Роль</param>
        /// <returns></returns>
        public static bool CanApproveApplication(IdentityRole role)
        {
            return CanApproveApplication(role.Name);
        }

        /// <summary>
        /// Может одобрять заявку?
        /// </summary>
        /// <param name="role">Название роли</param>
        /// <returns></returns>
        private static bool CanApproveApplication(string role)
        {
            return whoCanApproveApplication.Any(p => p.ToString() == role);
        }

        /// <summary>
        /// Могу выплачивать деньги?
        /// </summary>
        /// <returns></returns>
        public bool CanGiveMoney()
        {
            return CanGiveMoney(this.Name);
        }

        /// <summary>
        /// Могу выплачивать деньги?
        /// </summary>
        /// <param name="role">Название роли</param>
        /// <returns></returns>
        public static bool CanGiveMoney(IdentityRole role)
        {
            return CanGiveMoney(role.Name);
        }

        /// <summary>
        /// Могу выплачивать деньги?
        /// </summary>
        /// <param name="role">Название роли</param>
        /// <returns></returns>
        private static bool CanGiveMoney(string role)
        {
            return role == RoleTypes.Accountant.ToString();
        }

        /// <summary>
        /// Могу просматривать все объявления?
        /// </summary>
        /// <param name="role">Название роли</param>
        /// <returns></returns>
        public static bool CanViewAllApplications(IdentityRole role)
        {
            return CanViewAllApplications(role.Name);
        }

        /// <summary>
        /// Могу просматривать все объявления?
        /// </summary>
        /// <param name="role">Название роли</param>
        /// <returns></returns>
        private static bool CanViewAllApplications(string role)
        {
            return whoCanApproveApplication.Any(p => p.ToString() == role);
        }

        public bool Equals(ApplicationRole other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Priority == other.Priority;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApplicationRole)obj);
        }

        public override int GetHashCode()
        {
            return (int)Priority;
        }

        public static bool operator ==(ApplicationRole left, ApplicationRole right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ApplicationRole left, ApplicationRole right)
        {
            return !Equals(left, right);
        }
    }

}
