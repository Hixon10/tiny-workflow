using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Infrastructure.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;
using Rhino.Mocks;
using Service;
using Service.Exceptions;

namespace ServiceTest
{
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void GetCurrentBalance_Должна_вернуться_правильная_сумма_баланса_счёта()
        {
            // Arrange
            double balance = 10;

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.AccountRepository.Get()).Return(new List<Account> {new Account { Balance = balance }});

            ApplicationLogService applicationLogService = mocks.DynamicMock<ApplicationLogService>(unitOfWork);

            AccountService accountService = new AccountService(unitOfWork, applicationLogService); 

            // Act
            mocks.ReplayAll();
            var result = accountService.GetCurrentBalance();

            //Assert
            Assert.AreEqual(balance, result);
        }

//        [Test]
//        public void WithdrawMoney_Баланс_должен_уменьшиться_при_снимании_денег_со_счёта()
//        {
//            // Arrange
//            double balance = 10;
//
//            var mocks = new MockRepository();
//           
//            IUnitOfWork unitOfWork = mocks.DynamicMock<IUnitOfWork>();
//            unitOfWork.Stub(svc => svc.AccountRepository.Get()).Return(new List<Account> {new Account {Balance = balance}});
//            AccountService accountService = new AccountService(unitOfWork);
//
//            ApplicationUser user = mocks.DynamicMock<ApplicationUser>();
//            user.Stub(p => p.CanGiveMoney()).Return(true);
//
//            Application application = mocks.DynamicMock<Application>();
//
//            // Act
//            mocks.ReplayAll();
//            var result = accountService.WithdrawMoney(1, application, user);
//
//            //Assert
//            Assert.AreEqual(balance - 1, result);
//        }
//
//        [Test]
//        [ExpectedException(typeof(BusinessLayerException), ExpectedMessage = "Запрещено снимать со счёта сумму, большую, чем текущий баланс!")]
//        public void WithdrawMoney_Баланс_не_должен_измениться_когда_снимается_сумма_больше_чем_есть_на_счёте()
//        {
//            // Arrange
//            double balance = 10;
//
//            var mocks = new MockRepository();
//            IUnitOfWork unitOfWork = mocks.DynamicMock<IUnitOfWork>();
//            unitOfWork.Stub(svc => svc.AccountRepository.Get())
//                .Return(new List<Account> { new Account { Balance = balance } });
//
//            AccountService accountService = new AccountService(unitOfWork);
//
//            // Act
//            mocks.ReplayAll();
//            var result = accountService.WithdrawMoney(11, new Application(), new ApplicationUser());
//        }
//
//        [Test]
//        [ExpectedException(typeof(BusinessLayerException), ExpectedMessage = "Запрещено снимать со счёта отрицательную сумму!")]
//        public void WithdrawMoney_Запрещено_снимать_отрицательную_сумму()
//        {
//            // Arrange
//            double balance = 10;
//
//            var mocks = new MockRepository();
//            IUnitOfWork unitOfWork = mocks.DynamicMock<IUnitOfWork>();
//            unitOfWork.Stub(svc => svc.AccountRepository.Get())
//                .Return(new List<Account> { new Account { Balance = balance } });
//
//            AccountService accountService = new AccountService(unitOfWork);
//
//            // Act
//            mocks.ReplayAll();
//            var result = accountService.WithdrawMoney(-11, new Application(), new ApplicationUser());
//        }

        [Test]
        public void DepositMoney_Баланс_должен_увеличиваться_при_ложени_денег_на_счёт()
        {
            // Arrange
            double balance = 10;

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.DynamicMock<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.AccountRepository.Get())
                .Return(new List<Account> { new Account { Balance = balance } });

            ApplicationLogService applicationLogService = mocks.DynamicMock<ApplicationLogService>(unitOfWork);

            AccountService accountService = new AccountService(unitOfWork, applicationLogService); 

            // Act
            mocks.ReplayAll();
            var result = accountService.DepositMoney(13);

            //Assert
            Assert.AreEqual(balance + 13, result);
        }

        [Test]
        [ExpectedException(typeof(BusinessLayerException), ExpectedMessage = "Запрещено ложить на счёт отрицательную сумму!")]
        public void WithdrawMoney_Запрещено_класть_на_счёт_отрицательную_сумму()
        {
            // Arrange
            double balance = 10;

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.DynamicMock<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.AccountRepository.Get())
                .Return(new List<Account> { new Account { Balance = balance } });

            ApplicationLogService applicationLogService = mocks.DynamicMock<ApplicationLogService>(unitOfWork);

            AccountService accountService = new AccountService(unitOfWork, applicationLogService); 

            // Act
            mocks.ReplayAll();
            var result = accountService.DepositMoney(-11);
        }
    }
}
