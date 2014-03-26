using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Contracts;
using Infrastructure.Contracts;
using Service.Exceptions;

namespace Service
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void ChangeRolesPriority(Dictionary<ApplicationRole.RoleTypes, ApplicationRole.Priorities> priorities)
        {
            if (!priorities.Values.Any(p => p > ApplicationRole.Priorities.NotApprove))
            {
                throw new BusinessLayerException("Хотя бы одна роль должна одобрять заявку!");
            }

            var prioritiesWithoutNotApprove = priorities.Values.ToList();
            prioritiesWithoutNotApprove.RemoveAll(p => p.ToString() == ApplicationRole.Priorities.NotApprove.ToString());

            var hashSet = new HashSet<string>();

            var isHaveDuplicates = prioritiesWithoutNotApprove.Any(priority => !hashSet.Add(priority.ToString()));

            if (isHaveDuplicates)
            {
                throw new BusinessLayerException("Нельзя назначать одинаковые приоритеты ролям!");
            }

            foreach (var priority in priorities)
            {
                var role = GetRoleByType(priority.Key);
                role.Priority = priority.Value;

                if (role.CanApproveApplication())
                {
                    unitOfWork.ApplicationRoleRepository.Update(role);
                }
                else
                {
                    throw new BusinessLayerException("Попытка изменить приоритет одобрения заявки у роли, которая не имеет право на это действие!");
                }
            }

            unitOfWork.Save();
        }

        public List<ApplicationRole> GetRoles()
        {
            var roles = unitOfWork.ApplicationRoleRepository.GetAll();

            if (roles == null)
            {
                throw new BusinessLayerException("Нет ролей");
            }

            return roles.ToList();
        }

        public List<ApplicationRole.RoleTypes> GetRolesByUser(ApplicationUser user)
        {
            var roles = user.Roles.Select(p => p.Role.Name);

            return roles.Select(p => (ApplicationRole.RoleTypes) Enum.Parse(typeof (ApplicationRole.RoleTypes), p, false)).ToList();
        }

        public ApplicationRole GetRoleByType(ApplicationRole.RoleTypes type)
        {
            if (type == 0)
            {
                throw new BusinessLayerException("Не задан тип роли для поиска");
            }

            var roleName = type.ToString();

            var roles = unitOfWork.ApplicationRoleRepository.Get(p => p.Name == roleName);

            if (roles == null || roles.ToList().Count == 0)
            {
                throw new BusinessLayerException("Роль не найдена"); 
            }

            return roles.FirstOrDefault();
        }
    }
}