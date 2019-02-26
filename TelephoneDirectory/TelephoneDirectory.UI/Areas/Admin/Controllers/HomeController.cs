using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.DAL.Entities;
using TelephoneDirectory.Model.ViewModel.Partials;

namespace TelephoneDirectory.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Partials
        public PartialViewResult GetSideBarMenuPartial()
        {
            Employee employee = Session["LoggedEmployee"] as Employee;

            VMAreaSideBarMenu vmAreaSideBarMenu = VMAreaSideBarMenu.Parse(employee);
            return PartialView("_SideBarMenuPartial", vmAreaSideBarMenu);
        }
        public PartialViewResult GetTopBarPartial()
        {
            Employee employee = Session["LoggedEmployee"] as Employee;

            VMAreaTopBarMenu vmAreaTopBarMenu = VMAreaTopBarMenu.Parse(employee);
            return PartialView("_TopBarPartial", vmAreaTopBarMenu);
        }
        #endregion
    }
}