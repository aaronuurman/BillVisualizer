using System;
using System.IO;
using SautinSoft;
using System.Threading.Tasks;

namespace BillVisualizer.Services
{
    /// <summary>Service to convert Pdf to Excel.</summary>
    public interface IPdfToExcelConverter
    {
        /// <summary>Convert for Pdf to Excel.</summary>
        /// <param name="filePath">Path to Pdf file.</param>
        /// <returns>Path to excel file.</returns>
        /// <exception cref="ArgumentNullException">Is thrown if provided path is null or whitespace.</exception>
        /// <exception cref="Exception">Is thrown if service fails to read Pdf file.</exception>
        Task<string> Convert(string filePath);
    }
    
    ///<inheritdoc /> 
    public class PdfToExcelConverter : IPdfToExcelConverter
    {
        private PdfFocus Converter { get; set; }

        ///<inheritdoc cref="IPdfToExcelConverter"/> 
        public PdfToExcelConverter()
        {
            Converter = new PdfFocus();
        }
        
        ///<inheritdoc /> 
        public Task<string> Convert(string filePath)
        {
            // Todo: Maybe Fluent validation would make sense here.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(filePath);
            }
            
            if (!".pdf".Equals(Path.GetExtension(filePath)))
            {
                throw new FileFormatException($"File '{filePath}' is not a PDF file.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            Converter.OpenPdf(filePath);
                
            if (Converter.PageCount < 1)
            {
                throw new Exception($"Failed to read any page from PDF file {filePath}");
            }

            var excelFilePath = Path.Combine(Path.GetDirectoryName(filePath),
                $"{Path.GetFileNameWithoutExtension(filePath)}.xls");
            Converter.ToExcel(excelFilePath);

            return Task.FromResult(excelFilePath);
        }
    }
}