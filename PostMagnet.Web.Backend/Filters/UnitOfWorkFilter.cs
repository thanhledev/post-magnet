using System;
using System.Web.Mvc;

using PostMagnet.Domain.Helpers;
using Ninject;

namespace PostMagnet.Web.Backend.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
                UnitOfWork.BeginTransaction();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
                UnitOfWork.CommitTransaction();
        }
    }
}