using System;
using System.IO;

namespace UnitTests
{
    /// <summary>Base class for tests that use files.</summary>
    public abstract class ResourcesUnitTest : IDisposable
    {
        /// <inheritdoc cref="ResourcesUnitTest"/>
        protected ResourcesUnitTest()
        {
            Directory.CreateDirectory(TempDataDir);
            FileHandler.CopyDirectory(ResourcesFixture.ResourcesDir, Path.Combine(TempDataDir, ResourcesFixture.ResourcesDir));
        }
        
        /// <summary>Temporary directory where files will be stored for test.</summary>
        protected readonly string TempDataDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        /// <summary>Delete temporary files.</summary>
        public void Dispose()
        {
            Directory.Delete(TempDataDir, true);
        }
    }
}