using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelephoneDirectory.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound(string aspxerrorpath)
        {
            if (!IsNull(aspxerrorpath))
                return RedirectToAction("NotFound");
            return View();
        }

        public ActionResult InternalServer(string aspxerrorpath)
        {
            if (!IsNull(aspxerrorpath))
                return RedirectToAction("InternalServer");
            return View();
        }

        private bool IsNull(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return false;
        }
    }
}