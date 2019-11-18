using System.Web.Mvc;

using PostMagnet.Web.Backend.Security;

namespace PostMagnet.Web.Backend.ViewPageExtensions
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new EmployeePrincipal User
        {
            get { return base.User as EmployeePrincipal; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new EmployeePrincipal User
        {
            get { return base.User as EmployeePrincipal; }
        }
    }
}