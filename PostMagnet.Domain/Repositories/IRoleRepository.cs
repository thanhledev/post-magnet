using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Domain.Repositories
{
    public interface IRoleRepository
    {
        /* CRUD Repository Methods */
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);

        Role GetById(int id);
        Role FindBy(Expression<Func<Role, bool>> predicate);
        Role FindBy(ISpecification<Role> spec);

        IQueryable<Role> List();
        IQueryable<Role> FindList(Expression<Func<Role, bool>> predicate);
        IQueryable<Role> FindList(ISpecification<Role> spec);
    }
}
