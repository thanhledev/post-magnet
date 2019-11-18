using System.Web.Hosting;

namespace PostMagnet.Web.Backend.Helpers
{
    public static class ConfigurationService
    {
        public static string InvoiceFilePath
        {
            get { return "/Content/invoices/"; }
        }

        public static string InvoiceTextFileFath
        {
            get { return HostingEnvironment.MapPath("~/Content/invoices/"); }
        }
    }
}