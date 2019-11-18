using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class EmployeeActiveListSpecification : SpecificationBase<Employee>
    {
        private bool? _isActive;

        public EmployeeActiveListSpecification(bool? isActive)
        {
            _isActive = isActive;
        }

        public override System.Linq.Expressions.Expression<Func<Employee, bool>> SpecExpression
        {
            get
            {
                if (_isActive.HasValue)
                    return e => e.IsActive == _isActive.Value;
                return e => true;
            }
        }
    }
}