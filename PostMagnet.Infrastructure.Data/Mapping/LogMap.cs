using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class LogMap : ClassMap<Log>
    {
        public LogMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("log_id");
            Map(x => x.Created, "log_created").CustomSqlType("Datetime");
            Map(x => x.Content, "log_content");
            Table("pm_logs");
            BatchSize(5000);
        }
    }
}
