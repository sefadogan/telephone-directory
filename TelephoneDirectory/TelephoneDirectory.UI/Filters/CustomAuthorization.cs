using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelephoneDirectory.UI.Filters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["LoggedEmployee"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller", "Account"},
                    {"Action", "Login"}
                });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}