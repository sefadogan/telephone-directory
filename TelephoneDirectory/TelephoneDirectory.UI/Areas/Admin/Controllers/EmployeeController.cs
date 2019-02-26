using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.BLL.UnitOfWork;
using TelephoneDirectory.DAL.Entities;
using TelephoneDirectory.Model.ViewModel;
using TelephoneDirectory.UI.Filters;

namespace TelephoneDirectory.UI.Areas.Admin.Controllers
{
    [CustomAuthorization]
    public class EmployeeController : Controller
    {
        private readonly TelephoneDirectoryEntities _context;
        private readonly UnitOfWork _uow;

        public EmployeeController()
        {
            _context = new TelephoneDirectoryEntities();
            _uow = new UnitOfWork(_context);
        }

        public ActionResult List()
        {
            List<Employee> employees = _uow.EmployeeRepository.ListAll().OrderByDescending(x => x.CreateDate).ToList();
            if (employees.Count == 0)
            {
                TempData["ProcessResult"] = "There are no employees to be listed.";
                TempData["AlertType"] = "info";
                return View();
            }

            List<VMAreaEmployee> vmAreaEmployee = VMAreaEmployee.Parse(employees);

            return View(vmAreaEmployee);
        }
    }
}