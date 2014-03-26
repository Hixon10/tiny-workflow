using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id
        {
            get; 
            set;
        }
    }
}
