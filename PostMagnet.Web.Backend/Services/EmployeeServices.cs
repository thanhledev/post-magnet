using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;
using PostMagnet.Web.Backend.Helpers;

namespace PostMagnet.Web.Backend.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        // Repositories will be injected
        IEmployeeRepository _employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public Employee FindBy(System.Linq.Expressions.Expression<System.Func<Employee, bool>> predicate)
        {
            return _employeeRepository.FindBy(predicate);
        }

        public Employee FindBy(ISpecification<Employee> spec)
        {
            return _employeeRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Employee> List()
        {
            return _employeeRepository.List();
        }

        public System.Linq.IQueryable<Employee> FindList(System.Linq.Expressions.Expression<System.Func<Employee, bool>> predicate)
        {
            return _employeeRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Employee> FindList(ISpecification<Employee> spec)
        {
            return _employeeRepository.FindList(spec);
        }

        public Employee Authenticate(ISpecification<Employee> spec, string password)
        {
            Employee validatedEmployee = _employeeRepository.FindBy(spec);
            if (validatedEmployee != null)
            {
                if (HashingManager.ValidatePassword(password, validatedEmployee.Password))
                    return validatedEmployee;
                return null;
            }
            return null;
        }

        public bool CheckAvailability(ISpecification<Employee> spec)
        {
            Employee existedEmployee = _employeeRepository.FindBy(spec);

            return existedEmployee == null;
        }
    }
}