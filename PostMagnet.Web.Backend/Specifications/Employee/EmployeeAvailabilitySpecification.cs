using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class EmployeeAvailabilitySpecification : SpecificationBase<Employee>
    {
        private string _username;

        public EmployeeAvailabilitySpecification(string username)
        {
            _username = username;
        }

        public override System.Linq.Expressions.Expression<Func<Employee, bool>> SpecExpression
        {
            get { return e => e.Username == _username; }
        }
    }
}