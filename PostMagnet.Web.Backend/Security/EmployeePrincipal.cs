using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace PostMagnet.Web.Backend.Security
{
    public class EmployeePrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return role.IndexOf(EmployeeRole, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public bool IsInPrivilege(string privilege)
        {
            return Privileges.Any(privilege.Contains);
        }

        public EmployeePrincipal(string username, List<string> privileges)
        {
            Identity = new GenericIdentity(username);
            Username = username;
            Privileges = privileges;
        }

        public EmployeePrincipal(string username)
            : this(username, new List<string>())
        {

        }

        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmployeeRole { get; set; }
        public List<string> Privileges { get; set; }
    }

    public class EmployeePrincipalSerializeModel
    {
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmployeeRole { get; set; }
    }
}