using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Счёт в компании
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// Текущий баланс 
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Баланс не может быть отрицательным")]
        [Required]
        public double Balance
        {
            get;
            set;
        }
    }
}
