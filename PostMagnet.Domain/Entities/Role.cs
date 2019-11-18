using System.Collections.Generic;

namespace PostMagnet.Domain.Entities
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Employee> Employees { get; set; }
        public virtual IList<Permission> Permissions { get; set; }

        public Role()
        {
            Employees = new List<Employee>();
            Permissions = new List<Permission>();
        }
    }
}
