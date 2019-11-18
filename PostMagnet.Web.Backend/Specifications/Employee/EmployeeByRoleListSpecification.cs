using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class EmployeeByRoleListSpecification : SpecificationBase<Employee>
    {
        private string _roleName;

        public EmployeeByRoleListSpecification(string roleName)
        {
            _roleName = roleName;
        }

        public override System.Linq.Expressions.Expression<Func<Employee, bool>> SpecExpression
        {
            get { return e => e.Role.Name == _roleName; }
        }
    }
}