using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class PostExtraPaymentMap : ClassMap<PostExtraPayment>
    {
        public PostExtraPaymentMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("extra_payment_id");
            Map(x => x.Amount, "extra_payment_amount");
            Map(x => x.Note, "extra_payment_note").Nullable();
            References(x => x.Post, "post_id");
            Table("pm_post_extra_payments");
        }
    }
}
