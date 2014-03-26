using System;
using System.Data.Entity;
using Domain;
using Infrastructure.Contracts;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        private IGenericRepository<ProductCategory> productCategoryRepository;
        private IGenericRepository<Account> accountRepository;
        private IGenericRepository<Application> applicationRepository;
        private IGenericRepository<ApplicationLog> applicationLogRepository;
        private IGenericRepository<ApplicationLogStatus> applicationLogStatusRepository;
        private IGenericRepository<ApplicationRole> applicationRoleRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Репозиторий Роли
        /// </summary>
        public IGenericRepository<ApplicationRole> ApplicationRoleRepository
        {
            get
            {

                if (this.applicationRoleRepository == null)
                {
                    this.applicationRoleRepository = new GenericRepository<ApplicationRole>(context);
                }
                return applicationRoleRepository;
            }
        }

        /// <summary>
        /// Репозиторий Категорий номенклатуры
        /// </summary>
        public IGenericRepository<ProductCategory> ProductCategoryRepository
        {
            get
            {

                if (this.productCategoryRepository == null)
                {
                    this.productCategoryRepository = new GenericRepository<ProductCategory>(context);
                }
                return productCategoryRepository;
            }
        }

        /// <summary>
        /// Репозиторий Статуса записи в логе
        /// </summary>
        public IGenericRepository<ApplicationLogStatus> ApplicationLogStatusRepository
        {
            get
            {

                if (this.applicationLogStatusRepository == null)
                {
                    this.applicationLogStatusRepository = new GenericRepository<ApplicationLogStatus>(context);
                }
                return applicationLogStatusRepository;
            }
        }

        /// <summary>
        /// Репозиторий Изменения статусов заявки
        /// </summary>
        public IGenericRepository<ApplicationLog> ApplicationLogRepository
        {
            get
            {

                if (this.applicationLogRepository == null)
                {
                    this.applicationLogRepository = new GenericRepository<ApplicationLog>(context);
                }
                return applicationLogRepository;
            }
        }

        /// <summary>
        /// Репозиторий Заявки на получение денег
        /// </summary>
        public IGenericRepository<Application> ApplicationRepository
        {
            get
            {

                if (this.applicationRepository == null)
                {
                    this.applicationRepository = new GenericRepository<Application>(context);
                }
                return applicationRepository;
            }
        }

        /// <summary>
        /// Репозиторий Счёта в компании
        /// </summary>
        public IGenericRepository<Account> AccountRepository
        {
            get
            {

                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(context);
                }
                return accountRepository;
            }
        }

        /// <summary>
        /// Сохранить все сделанные изменения контекста в Базу данных
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        public DbContext GetContext()
        {
            return context;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
