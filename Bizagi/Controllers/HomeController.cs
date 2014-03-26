using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Contracts;

namespace Bizagi.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountService accountService;

        public HomeController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public ActionResult Index()
        {
            accountService.DepositMoney(10);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}