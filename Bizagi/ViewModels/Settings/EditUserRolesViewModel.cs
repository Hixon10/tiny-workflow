using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;

namespace Bizagi.ViewModels.Settings
{
    /// <summary>
    /// View Model для редактирования пользователя
    /// </summary>
    public class EditUserRolesViewModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [Display(Name = "Пользователь")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        [Required]
        [Display(Name = "Роль пользователя")]
        public ApplicationRole.RoleTypes Role
        {
            get; 
            set; 
        }
    }
}