using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Категории номенклатуры
    /// </summary>
    public class ProductCategory : BaseEntity 
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name
        {
            get; 
            set;
        }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
