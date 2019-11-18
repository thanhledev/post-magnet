using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("prm_id");
            Map(x => x.Name, "prm_name");
            Map(x => x.Controller, "prm_controller_name");
            Map(x => x.Action, "prm_action_name");
            Map(x => x.IsMainMenu, "prm_is_main_menu");
            HasManyToMany(x => x.Roles).Cascade.AllDeleteOrphan()
                .Table("pm_roles_permissions")
                .ParentKeyColumn("prm_id")
                .ChildKeyColumn("role_id")
                .Inverse().BatchSize(25);
            Table("pm_permissions");
            BatchSize(25);
        }
    }
}
