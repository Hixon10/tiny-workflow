using System.Collections.Generic;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис по работе с Категориями
    /// </summary>
    public interface IProductCategoryService
    {
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns>Категории</returns>
        List<ProductCategory> GetAllCategories();

        /// <summary>
        /// Получить категорию по имени
        /// </summary>
        /// <param name="name">Имя категории</param>
        /// <returns>Категория</returns>
        ProductCategory GetCategoryByName(string name);
    }
}
