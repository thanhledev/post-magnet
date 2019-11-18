using System;

namespace PostMagnet.Domain.Entities
{
    public class Invoice
    {
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual InvoiceStatus Status { get; set; }
        public virtual int TotalAmount { get; set; }
        public virtual string Note { get; set; }
        public virtual string TextFile { get; set; }
        public virtual Employee Contributor { get; set; }
    }
}
