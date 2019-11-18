using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface IPermissionServices
    {
        void Create(Permission permission);
        void Update(Permission permission);

        Permission GetById(int id);
        Permission FindBy(Expression<Func<Permission, bool>> predicate);
        Permission FindBy(ISpecification<Permission> spec);

        IQueryable<Permission> List();
        IQueryable<Permission> FindList(Expression<Func<Permission, bool>> predicate);
        IQueryable<Permission> FindList(ISpecification<Permission> spec);
    }
}
