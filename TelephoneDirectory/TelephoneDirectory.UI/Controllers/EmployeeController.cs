using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.BLL.UnitOfWork;
using TelephoneDirectory.DAL.Entities;
using TelephoneDirectory.Model.ViewModel;

namespace TelephoneDirectory.UI.Controllers
{
    public class EmployeeController : Controller
    {
        readonly TelephoneDirectoryEntities _context;
        readonly UnitOfWork _uow;

        public EmployeeController()
        {
            _context = new TelephoneDirectoryEntities();
            _uow = new UnitOfWork(_context);
        }

        public ActionResult List()
        {
            List<Employee> employees = _uow.EmployeeRepository.ListAll(x => x.IsActive).OrderByDescending(x => x.CreateDate).ToList();

            if (employees.Count == 0)
            {
                TempData["ProcessResult"] = "There are no employees to be listed.";
                TempData["AlertType"] = "info";
                return View();
            }

            List<VMEmployee> vmEmployee = VMEmployee.Parse(employees);

            return View(vmEmployee);
        }
        public ActionResult Detail(int id)
        {
            Employee employee = _uow.EmployeeRepository.BringById(id);
            if (employee == null)
            {
                TempData["ProcessResult"] = "There are no employees to be displayed.";
                TempData["AlertType"] = "info";
                return RedirectToAction("List");
            }

            return View(employee);
        }
    }
}