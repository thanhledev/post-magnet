using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class EmployeeListByCreatorSpecification : SpecificationBase<Employee>
    {
        private string _creatorUsername;

        public EmployeeListByCreatorSpecification(string creatorUsername)
        {
            _creatorUsername = creatorUsername;
        }

        public override System.Linq.Expressions.Expression<Func<Employee, bool>> SpecExpression
        {
            get { return e => e.Creator.Username == _creatorUsername; }
        }
    }
}