using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PostMagnet.Web.Backend.Security
{
    public class ActionAuthorization : AuthorizeAttribute
    {
        public string Privilege { get; set; }

        protected virtual EmployeePrincipal CurrentUser
        {
            get { return HttpContext.Current.User as EmployeePrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;

            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                CurrentUser.Privileges = (List<string>)HttpContext.Current.Session[CurrentUser.Username];

                if (!String.IsNullOrEmpty(Privilege))
                {
                    if (!CurrentUser.IsInPrivilege(Privilege))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    }
                }
            }
            else
                base.OnAuthorization(filterContext);
        }
    }
}