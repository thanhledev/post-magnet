using Ninject.Modules;

using PostMagnet.Domain.Repositories;
using PostMagnet.Infrastructure.Data.Repositories;

namespace PostMagnet.Infrastructure.DependResolution
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            // Bind Repositories
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IInvoiceRepository>().To<InvoiceRepository>();
            Bind<ILogRepository>().To<LogRepository>();
            Bind<IPermissonRepository>().To<PermissionRepository>();
            Bind<IPostExtraPaymentRepository>().To<PostExtraPaymentRepository>();
            Bind<IPostRepository>().To<PostRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IWebsiteRepository>().To<WebsiteRepository>();
            Bind<IMessageRepository>().To<MessageRepository>();
            Bind<INotificationRepository>().To<NotificationRepository>();
        }
    }
}
