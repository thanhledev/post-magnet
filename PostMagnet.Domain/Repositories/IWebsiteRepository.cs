using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Domain.Repositories
{
    public interface IWebsiteRepository
    {
        /* CRUD Repository Methods */
        void Create(Website website);
        void Update(Website website);
        void Delete(int id);

        Website GetById(int id);
        Website FindBy(Expression<Func<Website, bool>> predicate);
        Website FindBy(ISpecification<Website> spec);

        IQueryable<Website> List();
        IQueryable<Website> FindList(Expression<Func<Website, bool>> predicate);
        IQueryable<Website> FindList(ISpecification<Website> spec);
    }
}
