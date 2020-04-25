using System;
using System.IO;
using BillVisualizer.Services;
using FluentAssertions;
using Xunit;

namespace UnitTests.Services
{
    public class PdfToExcelConverterTests : ResourcesUnitTest
    {
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
        public async void Convert_Exception_PdfCorrupted()
        {
            // Arrange
            var service = new PdfToExcelConverter();
            var filePath = Path.Combine(TempDataDir, ResourcesFixture.CorruptedPdfPath);

            // Act && Assert
            await Assert.ThrowsAsync<Exception>(() => service.Convert(filePath));
        }
        
        [Fact]
        public async void Convert_FileFormatException_NonPdfFile()
        {
            // Arrange
            var service = new PdfToExcelConverter();
            var filePath = Path.Combine(TempDataDir, ResourcesFixture.ImagePath);

            // Act && Assert
            await Assert.ThrowsAsync<FileFormatException>(() => service.Convert(filePath));
        }
        
        [Fact]
        public async void Convert_FileNotFoundException_FileDoesNotExists()
        {
            // Arrange
            var service = new PdfToExcelConverter();
            var filePath = Path.Combine(TempDataDir, ResourcesFixture.ResourcesDir, "nonexistence.pdf");

            // Act && Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => service.Convert(filePath));
        }
        
        [Fact]
        public async void Convert_Success_ExcelFileCreated()
        {
            // Arrange
            var service = new PdfToExcelConverter();
            var filePath = Path.Combine(TempDataDir, ResourcesFixture.BillPdfPath);
            var excelFilePath = Path.Combine(TempDataDir, ResourcesFixture.ResourcesDir, "bill-33666222.xls");
            
            // Act
            var result = await service.Convert(filePath);

            // Assert
            result.Should().Be(excelFilePath);
            File.Exists(excelFilePath).Should().BeTrue();
        }
    }
}