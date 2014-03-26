using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bizagi.ViewModels.Balance
{
    /// <summary>
    /// View Model для представления баланса в системе
    /// </summary>
    public class BalanceViewModel
    {
        /// <summary>
        /// Сколько денег ложить на счёт
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Нельзя класть на счёт отрицательную сумму денег")]
        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Баланс компании")]
        public double RequestedMoney
        {
            get;
            set;
        }
    }
}