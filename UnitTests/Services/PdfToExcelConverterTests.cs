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
        private string TemporaryBillPdfPath => Path.Combine(_tempDataDir, ResourcesFixture.BillPdfPath);

        public PdfToExcelConverterTests()
        {
            Directory.CreateDirectory(_tempDataDir);
            FileHandler.CopyDirectory(ResourcesFixture.BillPdfPath, TemporaryBillPdfPath);
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
        public void Convert_Success_ExcelFileCreated()
        {
            // Arrange
            var filePath = TemporaryBillPdfPath;
            var service = new PdfToExcelConverter();
            var excelFilePath = Path.Combine(_tempDataDir, ResourcesFixture.ResourcesDir, "arve.xls");
            
            // Act
            var result = service.Convert(filePath);

            // Assert
            result.Should().Be(excelFilePath);
        }

        public void Dispose()
        {
            Directory.Delete(_tempDataDir, true);
        }
    }
}