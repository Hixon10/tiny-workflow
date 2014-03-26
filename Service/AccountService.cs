using System;
using System.Linq;
using Domain;
using Domain.Contracts;
using Infrastructure.Contracts;
using Service.Exceptions;

namespace Service
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IApplicationLogService applicationLogService;

        public AccountService(IUnitOfWork unitOfWork, IApplicationLogService applicationLogService)
            : base(unitOfWork)
        {
            this.applicationLogService = applicationLogService;
        }

        public double DepositMoney(double amountOfMoney)
        {
            if (amountOfMoney < 0)
            {
                throw new BusinessLayerException("Запрещено ложить на счёт отрицательную сумму!");
            }

            var lastAccount = GetLastAccount();

            if (lastAccount == null)
            {
                lastAccount = new Account
                {
                    Balance = amountOfMoney
                };

                unitOfWork.AccountRepository.Insert(lastAccount);
                unitOfWork.Save();

                return amountOfMoney;
            }

            lastAccount.Balance += amountOfMoney;

            unitOfWork.AccountRepository.Update(lastAccount);
            unitOfWork.Save();

            return lastAccount.Balance;
        }

        public double WithdrawMoney(Guid applicationId, ApplicationUser user)
        {
            var application = unitOfWork.ApplicationRepository.GetById(applicationId);

            if (application == null)
            {
                throw new BusinessLayerException("Заявка не найдена");
            }

            return WithdrawMoney(application, user);
        }

        public double WithdrawMoney(Application application, ApplicationUser user)
        {
            if (!user.CanGiveMoney())
            {
                throw new BusinessLayerException("Вы не имеете право выдавать деньги!");
            }

            if (application.RequestedMoney < 0)
            {
                throw new BusinessLayerException("Запрещено снимать со счёта отрицательную сумму!");
            }

            if (application.GetCurrentStatus() != ApplicationLogStatus.Statuses.Approved)
            {
                throw new BusinessLayerException("Выплачивать деньги можно только одобренным заявкам!");
            }

            var currentBalance = GetCurrentBalance();

            if (currentBalance < application.RequestedMoney)
            {
                throw new BusinessLayerException("Запрещено снимать со счёта сумму, большую, чем текущий баланс!");
            }

            var lastAccount = GetLastAccount();

            if (lastAccount == null)
            {
                throw new BusinessLayerException("Запрещено снимать деньги с несуществующего счёта!");
            }

            lastAccount.Balance -= application.RequestedMoney;
            unitOfWork.AccountRepository.Update(lastAccount);
            unitOfWork.Save();

            applicationLogService.CreateLog(application, ApplicationLogStatus.Statuses.Paid, user);

            return lastAccount.Balance;
        }

        public double GetCurrentBalance()
        {
            var lastAccount = GetLastAccount();

            if (lastAccount == null)
            {
                return 0;
            }

            return lastAccount.Balance;
        }

        private Account GetLastAccount()
        {
            var accouts = unitOfWork.AccountRepository.Get();
            var lastAccount = accouts.FirstOrDefault();

            return lastAccount;
        }
    }
}