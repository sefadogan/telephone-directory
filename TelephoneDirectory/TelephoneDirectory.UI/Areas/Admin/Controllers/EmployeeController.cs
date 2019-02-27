using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Employee> employees = _uow.EmployeeRepository.ListAll(x => x.IsActive && x.Role.Name.Contains("Admin") == false).OrderByDescending(x => x.CreateDate).ToList();
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
        public ActionResult Edit(int id)
        {
            Session["SelectedEmployeeId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.

            Employee employee = _uow.EmployeeRepository.BringById(id);
            VMAreaEmployeeEdit vmAreaEmployeeEdit = VMAreaEmployeeEdit.Parse(employee);

            ViewData["Departments"] = _uow.DepartmentRepository.ListAll(x => x.IsActive);
            ViewData["Supervisors"] = _uow.EmployeeRepository.ListAll(x => x.IsActive && x.Role.Name.Contains("Admin") == false);
            ViewData["Titles"] = _uow.TitleRepository.ListAll(x => x.IsActive && x.Name.Contains("Admin") == false);
            return View(vmAreaEmployeeEdit);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            
            List<Employee> employes = _uow.EmployeeRepository.ListAll(x=>x.EmployeeId != id);
            
            foreach (var item in employes)
            {
                if(item.SupervisorId == id)
                {
                    jsonResult.Data = new
                    {
                        success = false,
                        message = "Could not delete! The employee you want to delete, another employee's manager."
                    };
                    return jsonResult;
                }
            }

            var result = _uow.EmployeeRepository.Delete(id);
            if(!_uow.SaveChanges())
            {
                jsonResult.Data = new
                {
                    success = result,
                    message = result.Message,
                };
                return jsonResult;
            }

            jsonResult.Data = new
            {
                success = result,
                message = result.Message,
            };
            return jsonResult;
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

            model.DisplayName = model.FirstName + " " + model.LastName;
            model.CreateDate = DateTime.Now;
            model.IsActive = true;
            model.RoleId = 2;

            var result = _uow.EmployeeRepository.Add(model);
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while adding an employee";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Create");
            }

            TempData["ProcessResult"] = "Employee created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            Employee employee = _uow.EmployeeRepository.BringById(Convert.ToInt32(Session["SelectedEmployeeId"]));

            model.CreateDate = employee.CreateDate;
            model.RoleId = employee.RoleId;
            model.EmployeeId = employee.EmployeeId;
            model.DisplayName = model.FirstName + " " + model.LastName;
            model.IsActive = employee.IsActive;

            var result = _uow.EmployeeRepository.Update(model);

            Session.Remove("SelectedEmployeeId");
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while updating employee.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ProcessResult"] = "Employee updated successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
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