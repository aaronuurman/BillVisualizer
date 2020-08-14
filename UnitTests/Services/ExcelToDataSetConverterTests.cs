using System;
using System.IO;
using BillVisualizer.Services;
using FluentAssertions;
using Xunit;

namespace UnitTests.Services
{
    public class ExcelToDataSetConverterTests : ResourcesUnitTest
    {
        private static string ExcelFilePath;
        
        public ExcelToDataSetConverterTests() {
            new PdfToExcelConverter().Convert(Path.Combine(TempDataDir, ResourcesFixture.BillPdfPath));
            ExcelFilePath = Path.Combine(TempDataDir, ResourcesFixture.ResourcesDir, "bill-33666222.xls");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Convert_ArgumentNullException_EmptyFilePath(string filePath)
        {
            // Arrange
            var service = new ExcelToDataSetConverter();
            
            // Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.Convert(filePath));
        }

        [Fact]
        public async void Convert_Success_DataSetCreated()
        {
            // Arrange
            var service = new ExcelToDataSetConverter();
        
            // Act
            var result = await service.Convert(ExcelFilePath);

            // Assert
            result.Should().NotBeNull();
            result.Tables[0].Rows.Count.Should().Be(73);
        }
        
    }
}