using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class PostServices : IPostServices
    {
        // Repositories will be injected
        IPostRepository _postRepository;

        public PostServices(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void Create(Post post)
        {
            _postRepository.Create(post);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetById(id);
        }

        public Post FindBy(System.Linq.Expressions.Expression<System.Func<Post, bool>> predicate)
        {
            return _postRepository.FindBy(predicate);
        }

        public Post FindBy(ISpecification<Post> spec)
        {
            return _postRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Post> List()
        {
            return _postRepository.List();
        }

        public System.Linq.IQueryable<Post> FindList(System.Linq.Expressions.Expression<System.Func<Post, bool>> predicate)
        {
            return _postRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Post> FindList(ISpecification<Post> spec)
        {
            return _postRepository.FindList(spec);
        }
    }
}