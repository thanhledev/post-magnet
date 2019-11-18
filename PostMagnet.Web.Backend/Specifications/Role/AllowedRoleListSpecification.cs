using System;
using System.Collections.Generic;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class AllowedRoleListSpecification : SpecificationBase<Role>
    {
        private List<EmployeePrivilege> _allowedPrivileges;

        public AllowedRoleListSpecification(List<EmployeePrivilege> allowedPrivileges)
        {
            _allowedPrivileges = allowedPrivileges;
        }

        public override System.Linq.Expressions.Expression<Func<Role, bool>> SpecExpression
        {
            get { return r => _allowedPrivileges.Contains((EmployeePrivilege)r.Id); }
        }
    }
}