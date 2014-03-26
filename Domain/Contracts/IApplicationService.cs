using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис для работы с Заявкой
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <param name="user">Владелец заявки</param>
        /// <param name="requestedMoney">Запрашиваемая сумма денег</param>
        /// <param name="category">Категория номенклатуры</param>
        void CreateApplication(ApplicationUser user, double requestedMoney, ProductCategory category);

        /// <summary>
        /// Получить все заявки в системе
        /// </summary>
        /// <returns>Все заявки в системе</returns>
        List<Application> GetAllApplications();

        /// <summary>
        /// Утвердить заявку
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <param name="user">Утверждающий пользователь</param>
        void ApproveApplication(Application application, ApplicationUser user);

        /// <summary>
        /// Утвердить заявку
        /// </summary>
        /// <param name="applicationId">Id Заявки</param>
        /// <param name="user">Утверждающий пользователь</param>
        void ApproveApplication(Guid applicationId, ApplicationUser user);

        /// <summary>
        /// Отклониить заявку
        /// </summary>
        /// <param name="applicationId">Id Заявки</param>
        /// <param name="user">Утверждающий пользователь</param>
        void RefuseApplication(Guid applicationId, ApplicationUser user);

        /// <summary>
        /// Отклониить заявку
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <param name="user">Утверждающий пользователь</param>
        void RefuseApplication(Application application, ApplicationUser user);

        /// <summary>
        /// Отклонить все активные заявки
        /// </summary>
        /// <param name="user">Утверждающий пользователь</param>
        void RefuseAllActiveApplications(ApplicationUser user);

        /// <summary>
        /// Получить заявки для утверждения
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Список заявок</returns>
        List<Application> GetApplicationsForApproving(ApplicationUser user);

        /// <summary>
        /// Получить заявки для выплаты денег
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Список заявок</returns>
        List<Application> GetApplicationsForPayment(ApplicationUser user);
    }
}
