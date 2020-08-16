using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

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

        /// <summary>
        /// Deserialize json to provided entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity the json to be deserialized.</typeparam>
        public static async Task<TEntity> DeSerializeJsonAsync<TEntity>(string filePath)
            where TEntity : class
        {
            using var fs = File.OpenRead(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return await JsonSerializer.DeserializeAsync<TEntity>(fs, options);
        }

        /// <summary> Make a json snapshot of provided object. </summary>
        public static async Task CreateJsonSnapshotAsync<TEnity>(TEnity data, string filePath)
            where TEnity : class
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                
            };

            string json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}