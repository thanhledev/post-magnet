using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

using PostMagnet.Web.Backend.Security;
using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // AutoMapper Configuration
            AutoMapperConfiguration.Configure();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                EmployeePrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<EmployeePrincipalSerializeModel>(authTicket.UserData);
                EmployeePrincipal newUser =
                    Mapper.Map<EmployeePrincipalSerializeModel, EmployeePrincipal>(serializeModel);

                HttpContext.Current.User = newUser;
            }
        }
    }
}
