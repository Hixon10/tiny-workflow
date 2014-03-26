using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bizagi.ViewModels.Balance;
using Domain.Contracts;

namespace Bizagi.Controllers
{
    [Authorize(Roles = "Accountant, Director")]
    public class BalanceController : BaseController
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;

        public BalanceController(IUserService userService, IAccountService accountService)
        {
            this.userService = userService;
            this.accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = "Accountant, Director")]
        public ActionResult Index()
        {
            var model = new BalanceViewModel
            {
                RequestedMoney = accountService.GetCurrentBalance()
            };


            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Accountant")]
        public ActionResult DepositMoney()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Accountant")]
        [ValidateAntiForgeryToken]
        public ActionResult DepositMoney(BalanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountService.DepositMoney(model.RequestedMoney);
            }

            return RedirectToAction("Index");
        }
	}
}