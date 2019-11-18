using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private UnitOfWork _unitOfWork;

        public WebsiteRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Website website)
        {
            Session.Save(website);
        }

        public void Update(Website website)
        {
            Session.Update(website);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Website>(id));
        }

        public Website GetById(int id)
        {
            return Session.Get<Website>(id);
        }

        public Website FindBy(System.Linq.Expressions.Expression<System.Func<Website, bool>> predicate)
        {
            return Session.Query<Website>().Where(predicate).FirstOrDefault();
        }

        public Website FindBy(Domain.SpecificationFramework.ISpecification<Website> spec)
        {
            return Session.Query<Website>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Website> List()
        {
            return Session.Query<Website>();
        }

        public IQueryable<Website> FindList(System.Linq.Expressions.Expression<System.Func<Website, bool>> predicate)
        {
            return Session.Query<Website>().Where(predicate);
        }

        public IQueryable<Website> FindList(Domain.SpecificationFramework.ISpecification<Website> spec)
        {
            return Session.Query<Website>().Where(spec.SpecExpression);
        }
    }
}
