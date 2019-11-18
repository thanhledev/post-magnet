using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class NotificationServices : INotificationServices
    {
        // Repositories will be injected
        INotificationRepository _notificationRepository;

        public NotificationServices(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public void Update(Notification notification)
        {
            _notificationRepository.Update(notification);
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public Notification FindBy(System.Linq.Expressions.Expression<System.Func<Notification, bool>> predicate)
        {
            return _notificationRepository.FindBy(predicate);
        }

        public Notification FindBy(ISpecification<Notification> spec)
        {
            return _notificationRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Notification> List()
        {
            return _notificationRepository.List();
        }

        public System.Linq.IQueryable<Notification> FindList(System.Linq.Expressions.Expression<System.Func<Notification, bool>> predicate)
        {
            return _notificationRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Notification> FindList(ISpecification<Notification> spec)
        {
            return _notificationRepository.FindList(spec);
        }
    }
}