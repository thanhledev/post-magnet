using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private UnitOfWork _unitOfWork;

        public LogRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Log log)
        {
            Session.Save(log);
        }

        public void Update(Log log)
        {
            Session.Update(log);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Log>(id));
        }

        public Log GetById(int id)
        {
            return Session.Get<Log>(id);
        }

        public Log FindBy(System.Linq.Expressions.Expression<System.Func<Log, bool>> predicate)
        {
            return Session.Query<Log>().Where(predicate).FirstOrDefault();
        }

        public Log FindBy(Domain.SpecificationFramework.ISpecification<Log> spec)
        {
            return Session.Query<Log>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Log> List()
        {
            return Session.Query<Log>();
        }

        public IQueryable<Log> FindList(System.Linq.Expressions.Expression<System.Func<Log, bool>> predicate)
        {
            return Session.Query<Log>().Where(predicate);
        }

        public IQueryable<Log> FindList(Domain.SpecificationFramework.ISpecification<Log> spec)
        {
            return Session.Query<Log>().Where(spec.SpecExpression);
        }
    }
}
