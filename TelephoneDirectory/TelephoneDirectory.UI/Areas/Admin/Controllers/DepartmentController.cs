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
    public class DepartmentController : Controller
    {
        private readonly TelephoneDirectoryEntities _context;
        private readonly UnitOfWork _uow;

        public DepartmentController()
        {
            _context = new TelephoneDirectoryEntities();
            _uow = new UnitOfWork(_context);
        }

        public ActionResult List()
        {
            List<Department> departments = _uow.DepartmentRepository.ListAll(x => x.IsActive).OrderByDescending(x => x.CreateDate).ToList();
            if (departments.Count == 0)
            {
                TempData["ProcessResult"] = "There are no departments to be listed.";
                TempData["AlertType"] = "info";
                return View();
            }

            List<VMAreaDepartment> vmAreaDepartment = VMAreaDepartment.Parse(departments);

            return View(vmAreaDepartment);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedDepartmentId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.

            Department department = _uow.DepartmentRepository.BringById(id);
            VMAreaDepartmentEdit vmAreaDepartmentEdit = VMAreaDepartmentEdit.Parse(department);

            return View(vmAreaDepartmentEdit);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            List<Employee> employes = _uow.EmployeeRepository.ListAll();

            foreach (var item in employes)
            {
                if (item.DepartmentId == id)
                {
                    jsonResult.Data = new
                    {
                        success = false,
                        message = "There are employees under the department you are trying to delete."
                    };
                    return jsonResult;
                }
            }

            var result = _uow.DepartmentRepository.Delete(id);
            if (!_uow.SaveChanges())
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
        public ActionResult Create(Department model)
        {
            #region Check whether department in the system

            Department dept = _uow.DepartmentRepository.BringByName(model.Name);
            if (dept != null && dept.Name == model.Name)
            {
                TempData["ProcessResult"] = "There is a department with this name in the system";
                TempData["AlertType"] = "danger";
                return View();
            }

            #endregion

            model.CreateDate = DateTime.Now;
            model.IsActive = true;

            var result = _uow.DepartmentRepository.Add(model);
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while creating an department.";
                TempData["AlertType"] = "danger";
                return View();
            }

            TempData["ProcessResult"] = "Department created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Department model)
        {

            Department department = _uow.DepartmentRepository.BringById(Convert.ToInt32(Session["SelectedDepartmentId"]));
            department.Name = model.Name;
            department.Description = model.Description;
            department.Abridgment = model.Abridgment;

            var result = _uow.DepartmentRepository.Update(model);

            Session.Remove("SelectedDepartmentId");
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while updating department.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ProcessResult"] = "Department updated successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }
    }
}