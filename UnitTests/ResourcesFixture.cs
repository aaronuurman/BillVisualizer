using System.IO;

namespace UnitTests
{
    public static class ResourcesFixture
    {
        /// <summary>Resources directory name.</summary>
        public const string ResourcesDir = "Resources";

        /// <summary>Path to pdf bill.</summary>
        public static string BillPdfPath => Path.Combine(ResourcesDir, "arve.pdf");
    }
}