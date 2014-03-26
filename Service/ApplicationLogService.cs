using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Infrastructure.Contracts;
using Service.Exceptions;

namespace Service
{
    public class ApplicationLogService : BaseService, IApplicationLogService
    {
        public ApplicationLogService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void CreateLogForNewApplication(Application application, ApplicationUser user)
        {
            var applicationLogStatus = GetApplicationLogStatusByName(ApplicationLogStatus.Statuses.Created);

            var log = new ApplicationLog(user, application, DateTime.Now, applicationLogStatus);

            CreateLog(log);
        }

        public void CreateLog(Application application, ApplicationLogStatus.Statuses status, ApplicationUser user)
        {
            if (application.GetCurrentStatus() == ApplicationLogStatus.Statuses.Denied ||
                application.GetCurrentStatus() == ApplicationLogStatus.Statuses.Paid)
            {
                throw new BusinessLayerException("Запрещено менять статус " + status.ToString() + " на статус " + application.GetCurrentStatus());
            }

            var applicationLogStatus = GetApplicationLogStatusByName(status);

            var log = new ApplicationLog(user, application, DateTime.Now, applicationLogStatus);

            CreateLog(log);
        }

        private void CreateLog(ApplicationLog log)
        {
            unitOfWork.ApplicationLogRepository.Insert(log);
            unitOfWork.Save();
        }

        private ApplicationLogStatus GetApplicationLogStatusByName(ApplicationLogStatus.Statuses status)
        {
            var statusName = status.ToString();

            return unitOfWork.ApplicationLogStatusRepository.Get(p => p.Name == statusName).FirstOrDefault();
        }
    }
}
