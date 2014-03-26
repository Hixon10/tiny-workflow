using System.ComponentModel.DataAnnotations;

namespace Bizagi.ViewModels.Settings
{
    /// <summary>
    /// View Model Пользователя
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [Display(Name = "Пользователь")]
        public string UserName { 
            get; 
            set; 
        }
    }
}