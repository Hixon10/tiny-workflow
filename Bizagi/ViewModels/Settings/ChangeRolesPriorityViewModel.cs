using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Bizagi.ViewModels.Settings
{
    /// <summary>
    /// View Model изменения приоритета одобрения заявки
    /// </summary>
    public class ChangeRolesPriorityViewModel
    {
        /// <summary>
        /// Приоритет бухгалтера
        /// </summary>
        [Required]
        [Display(Name = "Приоритет бухгалтера")]
        public ApplicationRole.Priorities AccountantPriority
        {
            get; 
            set; 
        }

        /// <summary>
        /// Приоритет директора
        /// </summary>
        [Required]
        [Display(Name = "Приоритет директора")]
        public ApplicationRole.Priorities DirectorPriority
        {
            get;
            set;
        }
    }
}