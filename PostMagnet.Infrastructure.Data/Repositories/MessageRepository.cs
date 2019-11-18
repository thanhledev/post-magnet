using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private UnitOfWork _unitOfWork;

        public MessageRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Message message)
        {
            Session.Save(message);
        }

        public void Update(Message message)
        {
            Session.Update(message);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Message>(id));
        }

        public Message GetById(int id)
        {
            return Session.Get<Message>(id);
        }

        public Message FindBy(System.Linq.Expressions.Expression<System.Func<Message, bool>> predicate)
        {
            return Session.Query<Message>().Where(predicate).FirstOrDefault();
        }

        public Message FindBy(Domain.SpecificationFramework.ISpecification<Message> spec)
        {
            return Session.Query<Message>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Message> List()
        {
            return Session.Query<Message>();
        }

        public IQueryable<Message> FindList(System.Linq.Expressions.Expression<System.Func<Message, bool>> predicate)
        {
            return Session.Query<Message>().Where(predicate);
        }

        public IQueryable<Message> FindList(Domain.SpecificationFramework.ISpecification<Message> spec)
        {
            return Session.Query<Message>().Where(spec.SpecExpression);
        }
    }
}
