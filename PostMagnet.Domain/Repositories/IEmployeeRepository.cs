using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        /* CRUD Repository Methods */
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(int id);

        Employee GetById(int id);
        Employee FindBy(Expression<Func<Employee, bool>> predicate);
        Employee FindBy(ISpecification<Employee> spec);

        IQueryable<Employee> List();
        IQueryable<Employee> FindList(Expression<Func<Employee, bool>> predicate);
        IQueryable<Employee> FindList(ISpecification<Employee> spec);
    }
}
