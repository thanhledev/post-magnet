using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Helpers;
using PostMagnet.Domain.Repositories;

using PostMagnet.Infrastructure.Data.Helpers;

using NHibernate;
using NHibernate.Linq;

namespace PostMagnet.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private UnitOfWork _unitOfWork;

        public EmployeeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public void Create(Employee employee)
        {
            Session.Save(employee);
        }

        public void Update(Employee employee)
        {
            Session.Update(employee);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Get<Employee>(id));
        }

        public Employee GetById(int id)
        {
            return Session.Get<Employee>(id);
        }

        public Employee FindBy(System.Linq.Expressions.Expression<System.Func<Employee, bool>> predicate)
        {
            return Session.Query<Employee>().Where(predicate).FirstOrDefault();
        }

        public Employee FindBy(Domain.SpecificationFramework.ISpecification<Employee> spec)
        {
            return Session.Query<Employee>().Where(spec.SpecExpression).FirstOrDefault();
        }

        public IQueryable<Employee> List()
        {
            return Session.Query<Employee>();
        }

        public IQueryable<Employee> FindList(System.Linq.Expressions.Expression<System.Func<Employee, bool>> predicate)
        {
            return Session.Query<Employee>().Where(predicate);
        }

        public IQueryable<Employee> FindList(Domain.SpecificationFramework.ISpecification<Employee> spec)
        {
            return Session.Query<Employee>().Where(spec.SpecExpression);
        }
    }
}
