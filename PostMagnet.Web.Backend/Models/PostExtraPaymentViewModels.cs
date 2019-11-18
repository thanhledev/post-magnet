using System.ComponentModel.DataAnnotations;

namespace PostMagnet.Web.Backend.Models
{
    public class PostExtraPaymentListViewModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public int PostId { get; set; }
    }

    public class PostExtraPaymentDetailViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
    }

    public class PostExtraPaymentUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public int PostId { get; set; }
    }

    public class PostExtraPaymentCreateViewModel
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public int PostId { get; set; }
    }

    public class PostExtraPaymentDeletionViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
    }

    public class PostExtraPaymentInvoiceViewModel
    {
        public string Amount { get; set; }
        public string Note { get; set; }
    }
}