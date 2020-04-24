using System.Threading.Tasks;
using BillVisualizer.Infrastructure.Command;
using BillVisualizer.Services;

namespace BillVisualize.Features.PdfReader
{
  ///<summary>PDF Reader feature.</summary>
  public class PdfReader
  {
    /// <summary>Command for Pdf reader handler.</summary>
    public class Command : ICommand
    {
      /// <inheritdoc cref="Command"/>
      public Command(string filePath)
      {
        FilePath = filePath;
      }

      ///<summary>Path to PDF file.</summary>
      public string FilePath { get; set; }
    }

    ///<summary>Handler for Pdf Reader.</summary>
    public class Handler : ICommandHandler<Command>
    {
      private readonly IDataSetParser _parser;
      private readonly IPdfToExcelConverter _pdfToExcelConverter;
      private readonly IExcelToDataSetConverter _excelToDataSetConverter;

      public Handler(IDataSetParser parser,
        IPdfToExcelConverter pdfToExcelConverter,
        IExcelToDataSetConverter excelToDataSetConverter)
      {
        _parser = parser;
        _pdfToExcelConverter = pdfToExcelConverter;
        _excelToDataSetConverter = excelToDataSetConverter;
      }
      
      /// <inheritdoc />
      public async Task Handle(Command command)
      {
        var excelPath = await _pdfToExcelConverter.Convert(command.FilePath);
        var dataSet = await _excelToDataSetConverter.Convert(excelPath);
        var data = await _parser.Parse(dataSet);
      }
    }
  }
}
