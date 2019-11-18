using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private UnitOfWork _unitOfWork;

        public InvoiceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Invoice invoice)
        {
            Session.Save(invoice);
        }

        public void Update(Invoice invoice)
        {
            Session.Update(invoice);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Invoice>(id));
        }

        public Invoice GetById(int id)
        {
            return Session.Get<Invoice>(id);
        }

        public Invoice FindBy(System.Linq.Expressions.Expression<System.Func<Invoice, bool>> predicate)
        {
            return Session.Query<Invoice>().Where(predicate).FirstOrDefault();
        }

        public Invoice FindBy(Domain.SpecificationFramework.ISpecification<Invoice> spec)
        {
            return Session.Query<Invoice>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Invoice> List()
        {
            return Session.Query<Invoice>();
        }

        public IQueryable<Invoice> FindList(System.Linq.Expressions.Expression<System.Func<Invoice, bool>> predicate)
        {
            return Session.Query<Invoice>().Where(predicate);
        }

        public IQueryable<Invoice> FindList(Domain.SpecificationFramework.ISpecification<Invoice> spec)
        {
            return Session.Query<Invoice>().Where(spec.SpecExpression);
        }
    }
}
