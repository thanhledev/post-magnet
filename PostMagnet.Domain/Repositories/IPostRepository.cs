using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Domain.Repositories
{
    public interface IPostRepository
    {
        /* CRUD Repository Methods */
        void Create(Post post);
        void Update(Post post);
        void Delete(int id);

        Post GetById(int id);
        Post FindBy(Expression<Func<Post, bool>> predicate);
        Post FindBy(ISpecification<Post> spec);

        IQueryable<Post> List();
        IQueryable<Post> FindList(Expression<Func<Post, bool>> predicate);
        IQueryable<Post> FindList(ISpecification<Post> spec);
    }
}
