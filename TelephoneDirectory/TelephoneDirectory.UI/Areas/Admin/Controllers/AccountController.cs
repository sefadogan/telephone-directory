using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.BLL.UnitOfWork;
using TelephoneDirectory.DAL.Entities;
using TelephoneDirectory.Model.ViewModel;

namespace TelephoneDirectory.UI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly TelephoneDirectoryEntities _context;
        private readonly UnitOfWork _uow;

        public AccountController()
        {
            _context = new TelephoneDirectoryEntities();
            _uow = new UnitOfWork(_context);
        }

        public ActionResult Login()
        {
            if (Session["LoggedEmployee"] != null)
            {
                return RedirectToAction("List", "Employee");
            }

            return View(new VMAreaAccountLogin());
        }
        public ActionResult Logout()
        {
            Session.Remove("LoggedUser");
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Login(VMAreaAccountLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Employee employee = _uow.EmployeeRepository.GetEmployeeByEmail(model.Email);
            if (employee == null)
            {
                TempData["ProcessResult"] = "There is no such employee in our system.";
                TempData["AlertType"] = "info";
                return View(model);
            }
            else if (employee.Password != model.Password)
            {
                TempData["ProcessResult"] = "Your employee information is incorrect. Please try again.";
                TempData["AlertType"] = "danger";
                return View(model);
            }

            Session["LoggedEmployee"] = employee;

            return RedirectToAction("List", "Employee");
        }
    }
}