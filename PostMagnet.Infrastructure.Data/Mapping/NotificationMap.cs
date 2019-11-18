using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class NotificationMap : ClassMap<Notification>
    {
        public NotificationMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("notification_id");
            Map(x => x.Created, "notification_created").CustomSqlType("Datetime");
            Map(x => x.Type, "notification_type").CustomType(typeof (NotificationType));
            Map(x => x.Content, "notification_content");
            Map(x => x.IsRead, "notification_is_read");
            References(x => x.Receiver, "notification_employee_id");
            Table("pm_notifications");
            BatchSize(1000);
        }
    }
}
