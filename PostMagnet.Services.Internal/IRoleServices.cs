using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface IRoleServices
    {
        void Create(Role role);
        void Update(Role role);

        Role GetById(int id);
        Role FindBy(Expression<Func<Role, bool>> predicate);
        Role FindBy(ISpecification<Role> spec);

        IQueryable<Role> List();
        IQueryable<Role> FindList(Expression<Func<Role, bool>> predicate);
        IQueryable<Role> FindList(ISpecification<Role> spec);
    }
}
