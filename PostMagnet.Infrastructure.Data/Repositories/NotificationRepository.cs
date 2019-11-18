using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private UnitOfWork _unitOfWork;

        public NotificationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Notification notification)
        {
            Session.Save(notification);
        }

        public void Update(Notification notification)
        {
            Session.Update(notification);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Notification>(id));
        }

        public Notification GetById(int id)
        {
            return Session.Get<Notification>(id);
        }

        public Notification FindBy(System.Linq.Expressions.Expression<System.Func<Notification, bool>> predicate)
        {
            return Session.Query<Notification>().Where(predicate).FirstOrDefault();
        }

        public Notification FindBy(Domain.SpecificationFramework.ISpecification<Notification> spec)
        {
            return Session.Query<Notification>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Notification> List()
        {
            return Session.Query<Notification>();
        }

        public IQueryable<Notification> FindList(System.Linq.Expressions.Expression<System.Func<Notification, bool>> predicate)
        {
            return Session.Query<Notification>().Where(predicate);
        }

        public IQueryable<Notification> FindList(Domain.SpecificationFramework.ISpecification<Notification> spec)
        {
            return Session.Query<Notification>().Where(spec.SpecExpression);
        }
    }
}
