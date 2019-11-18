using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface IPostExtraPaymentServices
    {
        void Create(PostExtraPayment postExtraPayment);
        void Update(PostExtraPayment postExtraPayment);

        PostExtraPayment GetById(int id);
        PostExtraPayment FindBy(Expression<Func<PostExtraPayment, bool>> predicate);
        PostExtraPayment FindBy(ISpecification<PostExtraPayment> spec);

        IQueryable<PostExtraPayment> List();
        IQueryable<PostExtraPayment> FindList(Expression<Func<PostExtraPayment, bool>> predicate);
        IQueryable<PostExtraPayment> FindList(ISpecification<PostExtraPayment> spec);
    }
}
