using BillVisualizer.Infrastructure.Command;
using Microsoft.Extensions.DependencyInjection;

namespace BillVisualizer.Features.PdfReader
{
  ///<summary>Extension method to configure PDF reader services.</summary>
  public static class PdfReaderFeatureRegistrator
  {
    ///<summary>Configure PDF reader feature for DI.</summary>
    public static IServiceCollection AddPdfReaderModule(this IServiceCollection services)
    {
        services.AddTransient(typeof(ICommandHandler<BillVisualize.Features.PdfReader.PdfReader.Command>), typeof(BillVisualize.Features.PdfReader.PdfReader.Handler));
        return services;
    }
  }
}