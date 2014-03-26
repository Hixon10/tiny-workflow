using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Статус записи в логе
    /// </summary>
    public class ApplicationLogStatus : BaseEntity
    {
        /// <summary>
        /// Существующие статусы лога
        /// </summary>
        public enum Statuses
        {
            Created = 0,
            Denied = 1,
            UnderApproval = 2,
            Approved = 3,
            Paid = 4
        };

        /// <summary>
        /// Название статуса
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание Стутуса
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Логи с некоторым статусом
        /// </summary>
        public virtual ICollection<ApplicationLog> ApplicationLogs { get; set; }
    }
}
