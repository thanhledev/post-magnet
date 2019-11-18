namespace PostMagnet.Domain.Entities
{
    public class PostExtraPayment
    {
        public virtual int Id { get; set; }
        public virtual int Amount { get; set; }
        public virtual string Note { get; set; }
        public virtual Post Post { get; set; }
    }
}
