﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Service.Exceptions;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ApplicationUser GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new NullReferenceException(login);
            }

            return unitOfWork.GetContext().Set<ApplicationUser>().FirstOrDefault(p => p.UserName == login);
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return unitOfWork.GetContext().Set<ApplicationUser>().ToList();
        }

        public void EditUserRoles(ApplicationUser user, IEnumerable<IdentityRole> roles)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(unitOfWork.GetContext()));

            ClearUserRoles(um, user.Id);

            foreach (var role in roles)
            {
                AddUserToRole(um, user.Id, role.Name);
            }
        }

        private void ClearUserRoles(UserManager<ApplicationUser> um, string userId)
        {
            var user = um.FindById(userId);

            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);

            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, role.Role.Name);
            }
        }

        private bool AddUserToRole(UserManager<ApplicationUser> um, string userId, string roleName)
        {
            var idResult = um.AddToRole(userId, roleName);

            return idResult.Succeeded;
        }
    }
}
