using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Изменение статусов заявки
    /// </summary>
    public class ApplicationLog : BaseEntity
    {
        public ApplicationLog()
        {
            
        }

        public ApplicationLog(ApplicationUser user, Application application, DateTime createDateTime,
            ApplicationLogStatus status)
        {
            ApplicationUserId = user.Id;
            ApplicationId = application.Id;
            CreateDateTime = createDateTime;
            ApplicationLogStatusId = status.Id;
        }

        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Кто изменил статус заявки
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// Заявка для которой меняется статус
        /// </summary>
        public virtual Application Application { get; set; }

        ///Дата добавления записи в Лог
        [Required]
        public DateTime CreateDateTime { get; set; }

        [Required]
        public Guid ApplicationLogStatusId { get; set; }

        /// <summary>
        /// Статус лога
        /// </summary>
        public virtual ApplicationLogStatus ApplicationLogStatus { get; set; }
    }
}
