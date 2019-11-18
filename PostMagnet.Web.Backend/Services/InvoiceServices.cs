using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        // Repositories will be injected
        IInvoiceRepository _invoiceRepository;

        public InvoiceServices(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public void Create(Invoice invoice)
        {
            _invoiceRepository.Create(invoice);
        }

        public void Update(Invoice invoice)
        {
            _invoiceRepository.Update(invoice);
        }

        public Invoice GetById(int id)
        {
            return _invoiceRepository.GetById(id);
        }

        public Invoice FindBy(System.Linq.Expressions.Expression<System.Func<Invoice, bool>> predicate)
        {
            return _invoiceRepository.FindBy(predicate);
        }

        public Invoice FindBy(ISpecification<Invoice> spec)
        {
            return _invoiceRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Invoice> List()
        {
            return _invoiceRepository.List();
        }

        public System.Linq.IQueryable<Invoice> FindList(System.Linq.Expressions.Expression<System.Func<Invoice, bool>> predicate)
        {
            return _invoiceRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Invoice> FindList(ISpecification<Invoice> spec)
        {
            return _invoiceRepository.FindList(spec);
        }
    }
}