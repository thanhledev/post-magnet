using System.Collections.Generic;

namespace PostMagnet.Domain.Entities
{
    public class Permission
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual bool IsMainMenu { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public Permission()
        {
            Roles = new List<Role>();
        }
    }
}
