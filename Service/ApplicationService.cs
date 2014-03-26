using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Contracts;
using Infrastructure.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using Service.Exceptions;

namespace Service
{
    public class ApplicationService : BaseService, IApplicationService
    {
        private readonly IApplicationLogService applicationLogService;

        public ApplicationService(IUnitOfWork unitOfWork, IApplicationLogService applicationLogService)
            : base(unitOfWork)
        {
            this.applicationLogService = applicationLogService;
        }

        public void CreateApplication(ApplicationUser user, double requestedMoney, ProductCategory category)
        {
            var application = new Application(user, category, requestedMoney);

            unitOfWork.ApplicationRepository.Insert(application);
            unitOfWork.Save();

            applicationLogService.CreateLogForNewApplication(application, user);
        }

        public List<Application> GetAllApplications()
        {
            return unitOfWork.ApplicationRepository.GetAll().ToList();
        }

        public void ApproveApplication(Guid applicationId, ApplicationUser user)
        {
            var application = unitOfWork.ApplicationRepository.GetById(applicationId);

            if (application == null)
            {
                throw new BusinessLayerException("Заявка не найдена");
            }

            ApproveApplication(application, user);
        }

        public void ApproveApplication(Application application, ApplicationUser user)
        {
            if (!user.CanApproveApplication())
            {
                throw new BusinessLayerException("У вас нет прав одобрять заявку");
            }

            if (ChangeStatusApplication(application, user))
            {
                throw new BusinessLayerException("Вы уже изменяли статус этой заявки!");
            }

            if (!CanChangeStatus(application))
            {
                throw new BusinessLayerException("Эта заявка уже имеет конечный статус!");
            }

            applicationLogService.CreateLog(application, ApplicationLogStatus.Statuses.UnderApproval, user);

            var howManyRolesCanApproveApplication = ApplicationRole.HowManyRolesCanApproveApplication();

            var howManyTimesApplicationWasApproved = GetHowManyTimesApplicationWasApproved(application);

            if (howManyRolesCanApproveApplication == howManyTimesApplicationWasApproved)
            {
                applicationLogService.CreateLog(application, ApplicationLogStatus.Statuses.Approved, user);
            }
        }

        public void RefuseApplication(Guid applicationId, ApplicationUser user)
        {
            var application = unitOfWork.ApplicationRepository.GetById(applicationId);

            if (application == null)
            {
                throw new BusinessLayerException("Заявка не найдена");
            }

            RefuseApplication(application, user);
        }

        public void RefuseApplication(Application application, ApplicationUser user)
        {
            if (!user.CanApproveApplication())
            {
                throw new BusinessLayerException("У вас нет прав отклонять заявку!");
            }

            if (ChangeStatusApplication(application, user))
            {
                throw new BusinessLayerException("Вы уже изменяли статус этой заявки!");
            }

            if (!CanChangeStatus(application))
            {
                throw new BusinessLayerException("Эта заявка уже имеет конечный статус!");
            }

            applicationLogService.CreateLog(application, ApplicationLogStatus.Statuses.Denied, user);
        }

        public void RefuseAllActiveApplications(ApplicationUser user)
        {
            var activeApplications = GetAllApplications().Where(CanChangeStatus);

            foreach (var application in activeApplications)
            {
                applicationLogService.CreateLog(application, ApplicationLogStatus.Statuses.Denied, user);
            }
        }

        public List<Application> GetApplicationsForApproving(ApplicationUser user)
        {
            if (!user.CanApproveApplication())
            {
                throw new BusinessLayerException("У вас нет прав отклонять заявку!");
            }

            var allRolesInSystem = unitOfWork.ApplicationRoleRepository.GetAll().ToList();
            var myMajorRole = allRolesInSystem.FirstOrDefault(p => p.Name == user.GetMajorRole().Name);

            allRolesInSystem.Remove(myMajorRole);

            allRolesInSystem = allRolesInSystem.Where(p => p.CanApproveApplication()).ToList();

            if (myMajorRole == null || 
                !myMajorRole.CanApproveApplication() || 
                myMajorRole.Priority == ApplicationRole.Priorities.NotApprove)
            {
                throw new BusinessLayerException("У вас нет прав отклонять заявку!");
            }

            var prevRole = allRolesInSystem.Where(p => p.Priority < myMajorRole.Priority).OrderByDescending(p => p.Priority).FirstOrDefault();

            var applicationWithRigthStatuses = GetAllApplications()
                                               .Where(CanChangeStatus)
                                               .Where(p => !IsRoleApproveApplication(myMajorRole, p)).ToList();

            if (prevRole == null)
            {
                return applicationWithRigthStatuses;
            }

            return applicationWithRigthStatuses.Where(p => IsRoleApproveApplication(prevRole, p)).ToList();
        }

        public List<Application> GetApplicationsForPayment(ApplicationUser user)
        {
            if (!user.CanGiveMoney())
            {
                throw new BusinessLayerException("У вас нет прав выплачивание денег!");
            }

            return GetAllApplications().Where(p => p.GetCurrentStatus() == ApplicationLogStatus.Statuses.Approved).ToList();
        }

        /// <summary>
        /// Сколько раз уже одобрили эту заявку?
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <returns>Число одобрений</returns>
        private int GetHowManyTimesApplicationWasApproved(Application application)
        {
            var logs = application.ApplicationLogs;

            var status = ApplicationLogStatus.Statuses.UnderApproval.ToString();

            var howManyTimesApplicationWasApproved = logs.Count(log => log.ApplicationLogStatus.Name == status);

            return howManyTimesApplicationWasApproved + 1; // 1 - это, возможно, из-за лениновсти вычислений: последнего лога сразу нету
        }

        /// <summary>
        /// У этой заявки ещё можно менять статус?
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <returns></returns>
        private bool CanChangeStatus(Application application)
        {
            if (application.GetCurrentStatus() == ApplicationLogStatus.Statuses.Created ||
                application.GetCurrentStatus() == ApplicationLogStatus.Statuses.UnderApproval)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Я уже изменял статус заявки?
        /// </summary>
        /// <param name="application">Заявка</param>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        private bool ChangeStatusApplication(Application application, ApplicationUser user)
        {
            if (application.ApplicationLogs.Any(p => p.ApplicationUser.UserName == user.UserName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Какая-либо роль из списка ролей одобряла заявку?
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        private bool AreRolesApproveApplication(IEnumerable<IdentityRole> roles, Application application)
        {
            return roles.Any(role => IsRoleApproveApplication(role, application));
        }

        /// <summary>
        /// Данная роль уже одобряла заявку?
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="application">Заявка</param>
        /// <returns></returns>
        private bool IsRoleApproveApplication(IdentityRole role, Application application)
        {
            var logs = application.ApplicationLogs.ToList();
            
            foreach (var log in logs)
            {
                var roles = log.ApplicationUser.Roles;
                if (roles.Any(p => p.Role.Name == role.Name && p.Role.Name != ApplicationRole.RoleTypes.Employee.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
