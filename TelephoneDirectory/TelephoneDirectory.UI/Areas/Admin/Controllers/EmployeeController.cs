using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.BLL.UnitOfWork;
using TelephoneDirectory.Core;
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
            List<Employee> employees = _uow.EmployeeRepository.ListAll(x => x.Role.Name.Contains("Admin") == false).OrderByDescending(x => x.CreateDate).ToList();
            if (employees.Count == 0)
            {
                TempData["ProcessResult"] = "There are no employees to be listed.";
                TempData["AlertType"] = "info";
                return View();
            }

            List<VMAreaEmployee> vmAreaEmployee = VMAreaEmployee.Parse(employees);

            return View(vmAreaEmployee);
        }
        public ActionResult Create()
        {
            ViewData["Departments"] = _uow.DepartmentRepository.ListAll(x => x.IsActive);
            ViewData["Supervisors"] = _uow.EmployeeRepository.ListAll(x => x.IsActive && x.Role.Name.Contains("Admin") == false);
            ViewData["Titles"] = _uow.TitleRepository.ListAll(x => x.IsActive && x.Name.Contains("Admin") == false);
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            #region Check whether the system is user

            Employee emp = _uow.EmployeeRepository.BringByEmail(model.Email);
            if (emp != null && emp.Email == model.Email)
            {
                TempData["ProcessResult"] = "There is a employee in the system for this mail.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Create"); // return view dediğimde Create action unun get ine düşmüyor.
            }

            emp = _uow.EmployeeRepository.BringByTelephone(model.Telephone);
            if (emp != null && emp.Telephone == model.Telephone)
            {
                TempData["ProcessResult"] = "There is a employee in the system for this telephone.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Create");
            }

            #endregion

            Employee employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DisplayName = model.FirstName + " " + model.LastName,
                Telephone = model.Telephone,
                Email = model.Email,
                Password = model.Password,
                CreateDate = DateTime.Now,
                IsActive = model.IsActive,
                RoleId = 2, // Worker olarak eklemek için. Uygun olmadığının farkındayım.
                SupervisorId = model.SupervisorId,
                TitleId = model.TitleId,
                DepartmentId = model.DepartmentId
            };

            var result = _uow.EmployeeRepository.Add(model);
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while adding an employee";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Create");
            }

            TempData["ProcessResult"] = "Employee created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string newPasswordVerify)
        {
            Employee employee = Session["LoggedEmployee"] as Employee;

            // Frontend tarafta javascript kullanmadığım için kontrolleri burada sağladım. Çok tekrar ve gereksiz gözüktüğünün farkındayım.
            Employee model = _uow.EmployeeRepository.BringById(employee.EmployeeId);
            if (currentPassword != model.Password)
            {
                TempData["ProcessResult"] = "The current password you entered is incorrect. Please check and try again.";
                TempData["AlertType"] = "danger";
                return View();
            }
            else if (newPassword != newPasswordVerify)
            {
                TempData["ProcessResult"] = "Password repeats are incorrect. Please check and try again.";
                TempData["AlertType"] = "danger";
                return View();
            }

            model.Password = newPassword;
            var result = _uow.EmployeeRepository.Update(model);

            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while updating the password.";
                TempData["AlertType"] = "danger";
                return View();
            }

            TempData["ProcessResult"] = "Password changed successfully.";
            TempData["AlertType"] = "success";
            return View();
        }
    }
}