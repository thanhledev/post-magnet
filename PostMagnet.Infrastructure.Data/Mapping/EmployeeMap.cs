using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("employee_id");
            References(x => x.Role, "employee_role_id");
            Map(x => x.Username, "employee_username");
            Map(x => x.Password, "employee_password");
            Map(x => x.Email, "employee_email").Nullable();
            Map(x => x.Phone, "employee_phone").Nullable();
            Map(x => x.Name, "employee_name");
            Map(x => x.Rate, "employee_rate");
            Map(x => x.IsActive, "employee_is_active");
            HasMany(x => x.Posts).KeyColumn("employee_id").Inverse().Cascade.All();
            HasMany(x => x.Invoices).KeyColumn("employee_id").Inverse().Cascade.All();
            HasMany(x => x.OwnEmployees).KeyColumn("employee_creator_id");
            References(x => x.Creator).Column("employee_creator_id");
            Table("pm_employees");
            BatchSize(25);
        }
    }
}
