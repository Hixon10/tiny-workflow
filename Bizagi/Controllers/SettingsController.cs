using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bizagi.ViewModels.Settings;
using Domain;
using Domain.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using Service.Exceptions;

namespace Bizagi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly IUserService userService;
        private readonly IApplicationService applicationService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public SettingsController(IUserService userService, IApplicationService applicationService, IProductCategoryService productCategoryService, IAccountService accountService, IRoleService roleService)
        {
            this.userService = userService;
            this.applicationService = applicationService;
            this.productCategoryService = productCategoryService;
            this.accountService = accountService;
            this.roleService = roleService;
        }

        [HttpGet]
        public ActionResult UserList()
        {
            var model = userService.GetAllUsers().Select(p => new UserViewModel
            {
                UserName = p.UserName
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult EditUserRoles(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("id");
            }

            var user = userService.GetUserByLogin(id);

            if (user == null)
            {
                throw new BusinessLayerException("Пользователь не найден!");
            }
            
            var model = new EditUserRolesViewModel
            {
                UserName = id,
                Role = (ApplicationRole.RoleTypes)Enum.Parse(typeof(ApplicationRole.RoleTypes), user.GetMajorRole().Name, false)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserRoles(EditUserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserByLogin(model.UserName);

                if (user == null)
                {
                    throw new BusinessLayerException("Пользователь не найден!");
                }

                var role = roleService.GetRoleByType(model.Role);

                userService.EditUserRoles(user, new List<IdentityRole> { role });
            }

            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult ChangeRolesPriority()
        {
            var model = new ChangeRolesPriorityViewModel
            {
                AccountantPriority = roleService.GetRoleByType(ApplicationRole.RoleTypes.Accountant).Priority,
                DirectorPriority = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director).Priority
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangeRolesPriority(ChangeRolesPriorityViewModel model)
        {
            var priorities = new Dictionary<ApplicationRole.RoleTypes, ApplicationRole.Priorities>
            {
                {ApplicationRole.RoleTypes.Accountant, model.AccountantPriority},
                {ApplicationRole.RoleTypes.Director, model.DirectorPriority}
            };

            roleService.ChangeRolesPriority(priorities);

            applicationService.RefuseAllActiveApplications(GetCurrentUser());

            return View(model);
        }

        private string GetCurrentLogin()
        {
            return User.Identity.Name;
        }

        private ApplicationUser GetCurrentUser()
        {
            var login = GetCurrentLogin();

            return userService.GetUserByLogin(login);
        }
	}
}