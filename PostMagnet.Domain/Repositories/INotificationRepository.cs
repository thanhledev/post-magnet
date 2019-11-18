using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Domain.Repositories
{
    public interface INotificationRepository
    {
        /* CRUD Repository Methods */
        void Create(Notification notification);
        void Update(Notification notification);
        void Delete(int id);

        Notification GetById(int id);
        Notification FindBy(Expression<Func<Notification, bool>> predicate);
        Notification FindBy(ISpecification<Notification> spec);

        IQueryable<Notification> List();
        IQueryable<Notification> FindList(Expression<Func<Notification, bool>> predicate);
        IQueryable<Notification> FindList(ISpecification<Notification> spec);
    }
}
