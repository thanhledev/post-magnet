using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("message_id");
            Map(x => x.Code, "message_code");
            Map(x => x.Sent, "message_sent").CustomSqlType("Datetime");
            Map(x => x.Content, "message_content");
            Map(x => x.IsRead, "message_is_read");
            References(x => x.Author, "message_author_id");
            References(x => x.Recipient, "message_recipient_id");
            Table("pm_messages");
            BatchSize(1000);
        }
    }
}
