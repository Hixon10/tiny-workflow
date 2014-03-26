using System;

namespace Domain.Contracts
{
    /// <summary>
    /// Сервис Счёта компании
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Положить деньги на счёт
        /// </summary>
        /// <param name="amountOfMoney">Количество денег</param>
        /// <returns>Количество денег на счёте</returns>
        double DepositMoney(double amountOfMoney);

        /// <summary>
        /// Снять наличные со счёта
        /// </summary>
        /// <param name="application">Заявка, для которой происходит выплата</param>
        /// <param name="user">Кто выплатил деньги</param>
        /// <returns>Количество денег на счёте</returns>
        double WithdrawMoney(Application application, ApplicationUser user);

        /// <summary>
        /// Снять наличные со счёта
        /// </summary>
        /// <param name="applicationId"> ID Заявки, для которой происходит выплата</param>
        /// <param name="user">Кто выплатил деньги</param>
        /// <returns>Количество денег на счёте</returns>
        double WithdrawMoney(Guid applicationId, ApplicationUser user);

        /// <summary>
        /// Получить текущий баланс счёта
        /// </summary>
        /// <returns>Текущий баланс счёта</returns>
        double GetCurrentBalance();
    }
}

