using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using ExcelDataReader;

namespace BillVisualizer.Services
{
    /// <summary>Service to convert Excel to DataSet.</summary>
    public interface IExcelToDataSetConverter
    {
        /// <summary>Convert Excel to DataSet.</summary>
        /// <param name="filePath">Path to Excel file.</param>
        /// <returns>A content of an Excel as DataSet.</returns>
        /// <exception cref="ArgumentNullException">Is thrown if provided path is null or whitespace.</exception>
        Task<DataSet> Convert(string filePath);
    }
    
    ///<inheritdoc /> 
    public class ExcelToDataSetConverter : IExcelToDataSetConverter
    {
        ///<inheritdoc /> 
        public async Task<DataSet> Convert(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(filePath);
            }
            
            // This encoding is needed because .Net Core does not support such encoding.
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            await using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            
            do
            {
                while (reader.Read()){}
            } while (reader.NextResult());

            return reader.AsDataSet();
        }
    }
}