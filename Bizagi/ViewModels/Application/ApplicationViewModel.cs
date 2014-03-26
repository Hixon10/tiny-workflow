using System;
using System.ComponentModel.DataAnnotations;

namespace Bizagi.ViewModels.Application
{
    /// <summary>
    /// View Model Одной заявки
    /// </summary>
    public class ApplicationViewModel
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Запрашиваемая сумма денег
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Нельзя запрашивать отрицательное число денег")]
        [Required]
        [Display(Name = "Сумма")]
        public double RequestedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Дата создания заявки
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public string CreatedDateTime
        {
            get; 
            set;
        }

        /// <summary>
        /// Имя создателя заявки
        /// </summary>
        [Required]
        [Display(Name = "Создатель")]
        public string ApplicationOwnerName
        {
            get; 
            set; 
        }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [Required]
        [Display(Name = "Статус")]
        public string Status
        {
            get; 
            set;
        }

        /// <summary>
        /// Номенклатура
        /// </summary>
        [Required]
        [Display(Name = "Номенклатура")]
        public string ProductCategory
        {
            get; 
            set; 
        }
    }
}