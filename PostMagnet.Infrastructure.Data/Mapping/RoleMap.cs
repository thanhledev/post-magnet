using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("role_id");
            Map(x => x.Name, "role_name");
            Map(x => x.Description, "role_description");
            HasMany(x => x.Employees).KeyColumn("employee_id").Inverse().Cascade.All();
            HasManyToMany(x => x.Permissions)
                .Cascade.SaveUpdate()
                .Table("pm_roles_permissions")
                .ParentKeyColumn("role_id")
                .ChildKeyColumn("prm_id")
                .BatchSize(25);
            Table("pm_roles");
            BatchSize(25);
        }
    }
}
