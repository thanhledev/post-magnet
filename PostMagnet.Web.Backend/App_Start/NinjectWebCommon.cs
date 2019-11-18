[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PostMagnet.Web.Backend.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PostMagnet.Web.Backend.App_Start.NinjectWebCommon), "Stop")]

namespace PostMagnet.Web.Backend.App_Start
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Reflection;
    using System.Collections.Generic;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc;
    using Ninject.Web.Mvc.FilterBindingSyntax;

    using PostMagnet.Infrastructure.DependResolution;
    using PostMagnet.Services.Internal;
    using PostMagnet.Web.Backend.Services;
    using PostMagnet.Web.Backend.Filters;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Bind local services
            kernel.Bind<IEmployeeServices>().To<EmployeeServices>();
            kernel.Bind<IRoleServices>().To<RoleServices>();
            kernel.Bind<IPermissionServices>().To<PermissionServices>();           
            kernel.Bind<ILogServices>().To<LogServices>();
            kernel.Bind<IPostServices>().To<PostServices>();
            kernel.Bind<IPostExtraPaymentServices>().To<PostExtraPaymentServices>();
            kernel.Bind<IWebsiteServices>().To<WebsiteServices>();
            kernel.Bind<IInvoiceServices>().To<InvoiceServices>();
            kernel.Bind<IMessageServices>().To<MessageServices>();
            kernel.Bind<INotificationServices>().To<NotificationServices>();

            // App Data & Infrastructure modules
            var modules = new List<NinjectModule>
            {
                new RepositoryModule(),
                new HelperModule()
            };

            kernel.BindFilter<UnitOfWorkFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<UnitOfWorkFilter>()
                .InRequestScope();
            kernel.Load(modules);
        }
    }
}