using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class PostExtraPaymentRepository : IPostExtraPaymentRepository
    {
        private UnitOfWork _unitOfWork;

        public PostExtraPaymentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(PostExtraPayment postExtraPayment)
        {
            Session.Save(postExtraPayment);
        }

        public void Update(PostExtraPayment postExtraPayment)
        {
            Session.Update(postExtraPayment);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<PostExtraPayment>(id));
        }

        public PostExtraPayment GetById(int id)
        {
            return Session.Get<PostExtraPayment>(id);
        }

        public PostExtraPayment FindBy(System.Linq.Expressions.Expression<System.Func<PostExtraPayment, bool>> predicate)
        {
            return Session.Query<PostExtraPayment>().Where(predicate).FirstOrDefault();
        }

        public PostExtraPayment FindBy(Domain.SpecificationFramework.ISpecification<PostExtraPayment> spec)
        {
            return Session.Query<PostExtraPayment>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<PostExtraPayment> List()
        {
            return Session.Query<PostExtraPayment>();
        }

        public IQueryable<PostExtraPayment> FindList(System.Linq.Expressions.Expression<System.Func<PostExtraPayment, bool>> predicate)
        {
            return Session.Query<PostExtraPayment>().Where(predicate);
        }

        public IQueryable<PostExtraPayment> FindList(Domain.SpecificationFramework.ISpecification<PostExtraPayment> spec)
        {
            return Session.Query<PostExtraPayment>().Where(spec.SpecExpression);
        }
    }
}
