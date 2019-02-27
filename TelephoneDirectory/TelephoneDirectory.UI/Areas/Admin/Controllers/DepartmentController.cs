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
    }
}