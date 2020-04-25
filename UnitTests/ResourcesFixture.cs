using System.IO;

namespace UnitTests
{
    /// <summary>Paths to resources available for using in tests.</summary>
    public static class ResourcesFixture
    {
        /// <summary>Resources directory name.</summary>
        public const string ResourcesDir = "Resources";

        /// <summary>Path to pdf bill.</summary>
        public static string BillPdfPath => Path.Combine(ResourcesDir, "bill-33666222.pdf");

        /// <summary>Path to corrupted pdf bill.</summary>
        public static string CorruptedPdfPath => Path.Combine(ResourcesDir, "corrupted-bill.pdf");

        /// <summary>Path to image file.</summary>
        public static string ImagePath => Path.Combine(ResourcesDir, "notpdf.jpg");
    }
}