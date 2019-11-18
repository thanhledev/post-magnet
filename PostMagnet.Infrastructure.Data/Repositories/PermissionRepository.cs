using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class PermissionRepository : IPermissonRepository
    {
        private UnitOfWork _unitOfWork;

        public PermissionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Permission permission)
        {
            Session.Save(permission);
        }

        public void Update(Permission permission)
        {
            Session.Update(permission);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Permission>(id));
        }

        public Permission GetById(int id)
        {
            return Session.Get<Permission>(id);
        }

        public Permission FindBy(System.Linq.Expressions.Expression<System.Func<Permission, bool>> predicate)
        {
            return Session.Query<Permission>().Where(predicate).FirstOrDefault();
        }

        public Permission FindBy(Domain.SpecificationFramework.ISpecification<Permission> spec)
        {
            return Session.Query<Permission>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Permission> List()
        {
            return Session.Query<Permission>();
        }

        public IQueryable<Permission> FindList(System.Linq.Expressions.Expression<System.Func<Permission, bool>> predicate)
        {
            return Session.Query<Permission>().Where(predicate);
        }

        public IQueryable<Permission> FindList(Domain.SpecificationFramework.ISpecification<Permission> spec)
        {
            return Session.Query<Permission>().Where(spec.SpecExpression);
        }
    }
}
