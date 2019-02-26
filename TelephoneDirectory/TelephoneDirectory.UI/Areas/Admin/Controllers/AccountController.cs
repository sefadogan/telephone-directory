using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.Model.ViewModel;

namespace TelephoneDirectory.UI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View(new VMAccountLogin());
        }
        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            return View();
        }
    }
}