using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис для работы с Логами
    /// </summary>
    public interface IApplicationLogService
    {
        /// <summary>
        /// Создать запись в логе
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <param name="status">Статус</param>
        /// <param name="user">Пользователь</param>
        void CreateLog(Application application, ApplicationLogStatus.Statuses status, ApplicationUser user);

        /// <summary>
        /// Создать запись в логе для новой заявки
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <param name="user">Пользователь</param>
        void CreateLogForNewApplication(Application application, ApplicationUser user);
    }
}
