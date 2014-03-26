using Infrastructure.Contracts;

namespace Service
{
    /// <summary>
    /// Базовые класс Сервиса
    /// </summary>
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
