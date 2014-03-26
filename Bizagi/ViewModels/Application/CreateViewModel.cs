using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Bizagi.ViewModels.Application
{
    /// <summary>
    /// Создание заявки
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// Запрашиваемая сумма денег
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Нельзя запрашивать отрицательное число денег")]
        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Запрашиваемая сумма")]
        public double RequestedMoney
        {
            get;
            set;
        }

        public List<ProductCategory> Caregories
        {
            get; set;
        }

        /// <summary>
        /// Категория Номенклатуры
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Номенклатура")]
        public string Category
        {
            get; 
            set;
        }
    }
}