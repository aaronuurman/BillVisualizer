using System.IO;

namespace UnitTests
{
    /// <summary>File handler for tests.</summary>
    public static class FileHandler
    {
        /// <summary>Copies directory contents from <paramref name="srcDir"/> to <paramref name="destDir"/>.</summary>
        /// <param name="srcDir">Path to source directory.</param>
        /// <param name="destDir">Path to destination directory.</param>
        public static void CopyDirectory(string srcDir, string destDir)
        {
            var sourceDirectory = new DirectoryInfo(srcDir);

            if (!sourceDirectory.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + srcDir);
            }

            if (Directory.Exists(destDir))
            {
                Directory.Delete(destDir, true);
            }
            
            Directory.CreateDirectory(destDir);

            var dirs = sourceDirectory.GetDirectories();
            var files = sourceDirectory.GetFiles();
            foreach (var file in files)
            {
                var tmpPath = Path.Combine(destDir, file.Name);
                file.CopyTo(tmpPath, false);
            }
            
            foreach (var subDir in dirs)
            {
                var tmpPath = Path.Combine(destDir, subDir.Name);
                CopyDirectory(subDir.FullName, tmpPath);
            }
        }
    }
}