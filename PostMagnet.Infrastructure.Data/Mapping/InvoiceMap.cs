using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class InvoiceMap : ClassMap<Invoice>
    {
        public InvoiceMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("invoice_id");
            Map(x => x.Created, "invoice_created").CustomSqlType("DateTime");
            Map(x => x.Status, "invoice_status").CustomType(typeof(InvoiceStatus));
            Map(x => x.TotalAmount, "invoice_total_amount");
            Map(x => x.Note, "invoice_note").Nullable();
            Map(x => x.TextFile, "invoice_file");
            References(x => x.Contributor, "employee_id");
            Table("pm_invoices");
            BatchSize(25);
        }
    }
}
