using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// Заявка на получение денег
    /// </summary>
    public class Application : BaseEntity
    {
        public Application()
        {
            
        }

        public Application(ApplicationUser user, ProductCategory category, double requestedMoney)
        {
            ApplicationUserId = user.Id;
            RequestedMoney = requestedMoney;
            ProductCategoryId = category.Id;
        }

        /// <summary>
        /// Запрашиваемая сумма денег
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Нельзя запрашивать отрицательное число денег")]
        [Required]
        public double RequestedMoney
        {
            get; 
            set;
        }

        [Required]
        public Guid ProductCategoryId { get; set; }

        /// <summary>
        /// Категория номенклатуры
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Создатель заявки
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Логи данной заявки
        /// </summary>
        public virtual ICollection<ApplicationLog> ApplicationLogs { get; set; }

        /// <summary>
        /// Получить Статус заявки
        /// </summary>
        /// <returns>Статус заявки</returns>
        public ApplicationLogStatus.Statuses GetCurrentStatus()
        {
            var log = GetFirstLogRow();

            return (ApplicationLogStatus.Statuses)Enum.Parse(typeof (ApplicationLogStatus.Statuses), log.ApplicationLogStatus.Name, false);
        }

        public ApplicationLogStatus GetCurrentApplicationLogStatus()
        {
            var log = GetFirstLogRow();

            return log.ApplicationLogStatus;
        }

        /// <summary>
        /// Получить Дату создания заявки
        /// </summary>
        /// <returns>Дата создания заявки</returns>
        public DateTime GetCreateDateTime()
        {
            var log = GetFirstLogRow();
            return log.CreateDateTime;
        }

        private ApplicationLog GetFirstLogRow()
        {
            var logs = ApplicationLogs.OrderByDescending(p => p.CreateDateTime);
            return logs.FirstOrDefault();
        }
    }
}
