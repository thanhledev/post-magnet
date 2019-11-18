using System;
using System.Reflection;
using System.Web.Mvc;

namespace PostMagnet.Web.Backend.Filters
{
    public class HttpAjaxRequestOnlyFilter : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (!controllerContext.HttpContext.Request.IsAjaxRequest())
                throw new Exception("This action" + methodInfo.Name + " can only be called via an Ajax request");
            return true;
        }
    }
}