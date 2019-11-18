using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private UnitOfWork _unitOfWork;

        public RoleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Role role)
        {
            Session.Save(role);
        }

        public void Update(Role role)
        {
            Session.Update(role);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Role>(id));
        }

        public Role GetById(int id)
        {
            return Session.Get<Role>(id);
        }

        public Role FindBy(System.Linq.Expressions.Expression<System.Func<Role, bool>> predicate)
        {
            return Session.Query<Role>().Where(predicate).FirstOrDefault();
        }

        public Role FindBy(Domain.SpecificationFramework.ISpecification<Role> spec)
        {
            return Session.Query<Role>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Role> List()
        {
            return Session.Query<Role>();
        }

        public IQueryable<Role> FindList(System.Linq.Expressions.Expression<System.Func<Role, bool>> predicate)
        {
            return Session.Query<Role>().Where(predicate);
        }

        public IQueryable<Role> FindList(Domain.SpecificationFramework.ISpecification<Role> spec)
        {
            return Session.Query<Role>().Where(spec.SpecExpression);
        }
    }
}
