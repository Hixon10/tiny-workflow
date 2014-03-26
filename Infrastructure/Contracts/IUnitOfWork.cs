using System;
using System.Data.Entity;
using Domain;

namespace Infrastructure.Contracts
{
    /// <summary>
    /// Интерфейс Паттерна Unit Of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий Категорий номенклатуры
        /// </summary>
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }

        /// <summary>
        /// Репозиторий Статуса записи в логе
        /// </summary>
        IGenericRepository<ApplicationLogStatus> ApplicationLogStatusRepository { get; }

        /// <summary>
        /// Репозиторий Изменения статусов заявки
        /// </summary>
        IGenericRepository<ApplicationLog> ApplicationLogRepository { get; }

        /// <summary>
        /// Репозиторий Заявки на получение денег
        /// </summary>
        IGenericRepository<Application> ApplicationRepository { get; }

        /// <summary>
        /// Репозиторий Счёта в компании
        /// </summary>
        IGenericRepository<Account> AccountRepository { get; }

        /// <summary>
        /// Репозиторий Роли
        /// </summary>
        IGenericRepository<ApplicationRole> ApplicationRoleRepository { get; }

        /// <summary>
        /// Сохранить все сделанные изменения контекста в Базу данных
        /// </summary>
        void Save();

        /// <summary>
        /// Получить контекст
        /// </summary>
        /// <returns>DbContext</returns>
        DbContext GetContext();
    }
}