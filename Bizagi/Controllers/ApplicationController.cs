using System;
using System.Linq;
using System.Web.Mvc;
using Bizagi.ViewModels.Application;
using Domain;
using Domain.Contracts;
using Service.Exceptions;

namespace Bizagi.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private readonly IUserService userService;
        private readonly IApplicationService applicationService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IAccountService accountService;

        public ApplicationController(IUserService userService, IApplicationService applicationService, IProductCategoryService productCategoryService, IAccountService accountService)
        {
            this.userService = userService;
            this.applicationService = applicationService;
            this.productCategoryService = productCategoryService;
            this.accountService = accountService;
        }

        //
        // GET: /Application/
        public ActionResult Index()
        {
            var applications = applicationService.GetAllApplications();

            var currentUser = GetCurrentUser();

            if (!currentUser.CanViewAllApplications())
            {
                applications = applications.Where(p => p.ApplicationUser.UserName == currentUser.UserName).ToList();
            }

            var model = applications.Select(p => new ApplicationViewModel
            {
                ApplicationOwnerName = p.ApplicationUser.UserName,
                RequestedMoney = p.RequestedMoney,
                CreatedDateTime = p.GetCreateDateTime().ToString("dd.MM.yyyy"),
                ProductCategory = p.ProductCategory.Name,
                Status = p.GetCurrentApplicationLogStatus().Description,
                Id = p.Id
            });

            return View(model);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel
            {
                Caregories = productCategoryService.GetAllCategories()
            };

            return View(model);
        }

        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = productCategoryService.GetCategoryByName(model.Category);

                    applicationService.CreateApplication(GetCurrentUser(), model.RequestedMoney, category);

                    return RedirectToAction("Index");
                }
                catch (BusinessLayerException businessLayerException)
                {
                    ModelState.AddModelError("", businessLayerException.Message);

                    model = new CreateViewModel
                    {
                        Caregories = productCategoryService.GetAllCategories()
                    };

                    return View(model); 
                }
            }

            model = new CreateViewModel
            {
                Caregories = productCategoryService.GetAllCategories()
            };


            return View(model); 
        }

        [HttpGet]
        [Authorize(Roles = "Accountant")]
        public ActionResult GiveMoney()
        {
            var applicationForGiveMoney = applicationService.GetApplicationsForPayment(GetCurrentUser());

            var model = applicationForGiveMoney.Select(p => new ApplicationViewModel
            {
                ApplicationOwnerName = p.ApplicationUser.UserName,
                RequestedMoney = p.RequestedMoney,
                CreatedDateTime = p.GetCreateDateTime().ToString("dd.MM.yyyy"),
                ProductCategory = p.ProductCategory.Name,
                Status = p.GetCurrentApplicationLogStatus().Description,
                Id = p.Id
            });

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Accountant")]
        [ValidateAntiForgeryToken]
        public ActionResult GiveMoneyForApplication(Guid id)
        {
            accountService.WithdrawMoney(id, GetCurrentUser());
            return RedirectToAction("GiveMoney");
        }

        [HttpGet]
        [Authorize(Roles = "Accountant, Director")]
        public ActionResult Approve()
        {
            var applicationForApprov = applicationService.GetApplicationsForApproving(GetCurrentUser());

            var model = applicationForApprov.Select(p => new ApplicationViewModel
            {
                ApplicationOwnerName = p.ApplicationUser.UserName,
                RequestedMoney = p.RequestedMoney,
                CreatedDateTime = p.GetCreateDateTime().ToString("dd.MM.yyyy"),
                ProductCategory = p.ProductCategory.Name,
                Status = p.GetCurrentApplicationLogStatus().Description,
                Id = p.Id
            });

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Accountant, Director")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveApplication(Guid id)
        {
            applicationService.ApproveApplication(id, GetCurrentUser());
            return RedirectToAction("Approve");
        }

        [HttpPost]
        [Authorize(Roles = "Accountant, Director")]
        [ValidateAntiForgeryToken]
        public ActionResult Refuse(Guid id)
        {
            applicationService.RefuseApplication(id, GetCurrentUser());
            return RedirectToAction("Approve");
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