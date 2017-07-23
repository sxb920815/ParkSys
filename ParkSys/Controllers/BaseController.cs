using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repair.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["UserId"] == null)
            {
                if (this.RouteData.Values["Controller"].ToString() != "Login")
                {
                    filterContext.Result = new RedirectResult("/Login/Index");
                }
            }
            base.OnActionExecuting(filterContext);


        }

    }
}
