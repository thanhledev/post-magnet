using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface IInvoiceServices
    {
        void Create(Invoice invoice);
        void Update(Invoice invoice);

        Invoice GetById(int id);
        Invoice FindBy(Expression<Func<Invoice, bool>> predicate);
        Invoice FindBy(ISpecification<Invoice> spec);

        IQueryable<Invoice> List();
        IQueryable<Invoice> FindList(Expression<Func<Invoice, bool>> predicate);
        IQueryable<Invoice> FindList(ISpecification<Invoice> spec);
    }
}
