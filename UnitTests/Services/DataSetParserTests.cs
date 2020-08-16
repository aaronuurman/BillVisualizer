using System.IO;
using System.Linq;
using BillVisualizer.Models;
using BillVisualizer.Services;
using FluentAssertions;
using Xunit;

namespace UnitTests.Services
{
    public class DataSetParserTests : ResourcesUnitTest
    {
        private static string ExcelFilePath;
        private static string InvoicePropertiesJson;
        private static string InvoiceSnapshotJson;
        private static ExcelToDataSetConverter Converter;

        public DataSetParserTests()
        {
            new PdfToExcelConverter().Convert(Path.Combine(TempDataDir, ResourcesFixture.InvoicePdfPath));
            ExcelFilePath = Path.Combine(TempDataDir, ResourcesFixture.InvoiceXlsPath);
            Converter = new ExcelToDataSetConverter();
            InvoicePropertiesJson = Path.Combine(TempDataDir, ResourcesFixture.InvoicePropertiesPath);
            InvoiceSnapshotJson = Path.Combine(TempDataDir, ResourcesFixture.InvoiceJsonPath);
        }

        [Fact]
        public async void Parse_Success_InvoiceProperties()
        {
            // Arrange
            var dataSet = await Converter.Convert(ExcelFilePath);
            var service = new DataSetParser();
            var invoiceProperties = await FileHandler.DeSerializeJsonAsync<InvoiceProperties>(InvoicePropertiesJson);
            var snapshot = await FileHandler.DeSerializeJsonAsync<InvoiceData>(InvoiceSnapshotJson);

            // Act
            var result = await service.ParseAsync(dataSet, invoiceProperties);

            // Assert
            result.Should().NotBeNull();
            result.UtilityRows.Count.Should().Be(invoiceProperties.DataRows.Count);
            result.InvoiceNr.Should().Be(snapshot.InvoiceNr);
            result.InvoiceForDate.Should().Be(snapshot.InvoiceForDate);
            result.Total.Should().Be(snapshot.Total);
            foreach (var row in result.UtilityRows)
            {
                var actualData = snapshot.UtilityRows.SingleOrDefault(x => x.Name.Equals(row.Name));
                row.Cost.Should().Be(actualData?.Cost);
                row.Amount.Should().Be(actualData?.Amount);
                row.Unit.Should().Be(actualData?.Unit);
                row.UnitPrice.Should().Be(actualData?.UnitPrice);
            }
        }

    }
}