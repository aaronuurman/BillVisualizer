using System;
using System.IO;
using BillVisualizer.Services;
using FluentAssertions;
using Xunit;

namespace UnitTests.Services
{
    public class PdfToExcelConverterTests : IDisposable
    {
        private readonly string _tempDataDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        public PdfToExcelConverterTests()
        {
            Directory.CreateDirectory(_tempDataDir);
            FileHandler.CopyDirectory(ResourcesFixture.ResourcesDir, Path.Combine(_tempDataDir, ResourcesFixture.ResourcesDir));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Convert_ArgumentNullException_EmptyFilePath(string filePath)
        {
            // Arrange
            var service = new PdfToExcelConverter();
            
            // Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.Convert(filePath));
        }
        
        [Fact]
        public async void Convert_Success_ExcelFileCreated()
        {
            // Arrange
            var filePath = Path.Combine(_tempDataDir, ResourcesFixture.BillPdfPath);
            var service = new PdfToExcelConverter();
            var excelFilePath = Path.Combine(_tempDataDir, ResourcesFixture.ResourcesDir, "arve.xls");
            
            // Act
            var result = await service.Convert(filePath);

            // Assert
            result.Should().Be(excelFilePath);
            File.Exists(excelFilePath).Should().BeTrue();
        }

        public void Dispose()
        {
            Directory.Delete(_tempDataDir, true);
        }
    }
}