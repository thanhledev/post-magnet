using Ninject.Modules;
using Ninject.Web.Common;

using PostMagnet.Domain.Helpers;
using PostMagnet.Infrastructure.Data.Helpers;

namespace PostMagnet.Infrastructure.DependResolution
{
    public class HelperModule : NinjectModule
    {
        public override void Load()
        {
            // Bind Helpers
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
}
