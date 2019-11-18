using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private UnitOfWork _unitOfWork;

        public PostRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Post post)
        {
            Session.Save(post);
        }

        public void Update(Post post)
        {
            Session.Update(post);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Post>(id));
        }

        public Post GetById(int id)
        {
            return Session.Get<Post>(id);
        }

        public Post FindBy(System.Linq.Expressions.Expression<System.Func<Post, bool>> predicate)
        {
            return Session.Query<Post>().Where(predicate).FirstOrDefault();
        }

        public Post FindBy(Domain.SpecificationFramework.ISpecification<Post> spec)
        {
            return Session.Query<Post>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Post> List()
        {
            return Session.Query<Post>();
        }

        public IQueryable<Post> FindList(System.Linq.Expressions.Expression<System.Func<Post, bool>> predicate)
        {
            return Session.Query<Post>().Where(predicate);
        }

        public IQueryable<Post> FindList(Domain.SpecificationFramework.ISpecification<Post> spec)
        {
            return Session.Query<Post>().Where(spec.SpecExpression);
        }
    }
}
