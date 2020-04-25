using Microsoft.Extensions.DependencyInjection;

namespace BillVisualizer.Services
{
    /// <summary>Extension method to configure services module.</summary>
    public static class ServicesRegistrator
    {
        ///<summary>Configure PDF reader feature for DI.</summary>
        public static IServiceCollection AddServicesModule(this IServiceCollection services)
        {
            services.AddSingleton<IPdfToExcelConverter, PdfToExcelConverter>();
            services.AddSingleton<IExcelToDataSetConverter, ExcelToDataSetConverter>();
            services.AddSingleton<IDataSetParser, DataSetParser>();
            return services;
        }
    }
}