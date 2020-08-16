using System.IO;

namespace UnitTests
{
    /// <summary>Paths to resources available for using in tests.</summary>
    public static class ResourcesFixture
    {
        /// <summary>Resources directory name.</summary>
        public const string ResourcesDir = "Resources";

        /// <summary>Path to pdf invoice.</summary>
        public static string InvoicePdfPath => Path.Combine(ResourcesDir, "invoice.pdf");

        /// <summary>Path to excel invoice.</summary>
        public static string InvoiceXlsPath => Path.Combine(ResourcesDir, "invoice.xls");

        /// <summary>Path to json snapshot of invoice.</summary>
        public static string InvoiceJsonPath => Path.Combine(ResourcesDir, "invoice.json");

        /// <summary>Path to corrupted pdf bill.</summary>
        public static string CorruptedPdfPath => Path.Combine(ResourcesDir, "corrupted-invoice.pdf");

        /// <summary>Path to image file.</summary>
        public static string ImagePath => Path.Combine(ResourcesDir, "notpdf.jpg");

        /// <summary>Path to invoice properties json.</summary>
        public static string InvoicePropertiesPath => Path.Combine(ResourcesDir, "invoiceproperties.json");
    }
}