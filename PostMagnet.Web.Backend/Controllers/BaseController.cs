using System.Web.Mvc;

using PostMagnet.Web.Backend.Security;

namespace PostMagnet.Web.Backend.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new EmployeePrincipal User
        {
            get { return HttpContext.User as EmployeePrincipal; }
        }
    }
}