using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class PostExtraPaymentServices : IPostExtraPaymentServices
    {
        // Repositories will be injected
        IPostExtraPaymentRepository _postExtraPaymentRepository;

        public PostExtraPaymentServices(IPostExtraPaymentRepository postExtraPaymentRepository)
        {
            _postExtraPaymentRepository = postExtraPaymentRepository;
        }

        public void Create(PostExtraPayment postExtraPayment)
        {
            _postExtraPaymentRepository.Create(postExtraPayment);
        }

        public void Update(PostExtraPayment postExtraPayment)
        {
            _postExtraPaymentRepository.Update(postExtraPayment);
        }

        public PostExtraPayment GetById(int id)
        {
            return _postExtraPaymentRepository.GetById(id);
        }

        public PostExtraPayment FindBy(System.Linq.Expressions.Expression<System.Func<PostExtraPayment, bool>> predicate)
        {
            return _postExtraPaymentRepository.FindBy(predicate);
        }

        public PostExtraPayment FindBy(ISpecification<PostExtraPayment> spec)
        {
            return _postExtraPaymentRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<PostExtraPayment> List()
        {
            return _postExtraPaymentRepository.List();
        }

        public System.Linq.IQueryable<PostExtraPayment> FindList(System.Linq.Expressions.Expression<System.Func<PostExtraPayment, bool>> predicate)
        {
            return _postExtraPaymentRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<PostExtraPayment> FindList(ISpecification<PostExtraPayment> spec)
        {
            return _postExtraPaymentRepository.FindList(spec);
        }
    }
}