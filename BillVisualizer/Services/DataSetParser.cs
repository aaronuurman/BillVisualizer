using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BillVisualizer.Models;
using BillVisualizerWorker.Models;

namespace BillVisualizer.Services
{
    /// <summary>Service to parse fields from DataSet.</summary>
    public interface IDataSetParser
    {
        /// <summary>Parse search fields from DataSet.</summary>
        /// <param name="dataSet">Set of data.</param>
        /// <param name="invoiceProperties">Property names to search from DataSet.</param>
        /// <returns><see cref="InvoiceData"/></returns>
        /// <exception cref="Exception">Is thrown if fails to parse DataSet column.</exception>
        Task<InvoiceData> ParseAsync(DataSet dataSet, InvoiceProperties invoiceProperties);
    }

    ///<inheritdoc /> 
    public class DataSetParser : IDataSetParser
    {
        ///<inheritdoc /> 
        public async Task<InvoiceData> ParseAsync(DataSet dataSet, InvoiceProperties invoiceProperties)
        {
            var result = new InvoiceData(
                await ParseStringProperty(dataSet, invoiceProperties.InvoiceNr),
                await ParseDateTimeProperty(dataSet, invoiceProperties.Date, invoiceProperties.DateFormat),
                await ParseDoubleProperty(dataSet, invoiceProperties.Total)
            );

            result.SetUtilityRows(await ParseUtilityRows(dataSet, invoiceProperties.DataRows));

            return await Task.FromResult(result);
        }

        private Task<double> ParseDoubleProperty(DataSet dataSet, string propertyName)
        {
            var i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                var columns = GetRowColumns(dataSet.Tables[0].Rows, i);

                double result;

                if (columns.Any(item => propertyName.Equals(item)))
                {
                    if (double.TryParse(columns.Last().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        return Task.FromResult(result);
                    }
                    else
                    {
                        var nextIndex = i + 1;
                        var nextRowColumns = GetRowColumns(dataSet.Tables[0].Rows, nextIndex);

                        if (double.TryParse(nextRowColumns.Last().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                        {
                            return Task.FromResult(result);
                        }
                    }
                }

                if (columns.Count > 2 && propertyName.Equals($"{columns[0]} {columns[1]}"))
                {
                    if (double.TryParse(columns.Last().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        return Task.FromResult(result);
                    }
                    else
                    {
                        var nextIndex = i + 1;
                        var nextRowColumns = GetRowColumns(dataSet.Tables[0].Rows, nextIndex);

                        if (double.TryParse(nextRowColumns.Last().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                        {
                            return Task.FromResult(result);
                        }
                    }
                }

                i++;
            }

            throw new Exception($"Could not parse property '{propertyName}'!");
        }

        private Task<DateTime> ParseDateTimeProperty(DataSet dataSet, string propertyName, string dateFormat)
        {
            var i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                var columns = GetRowColumns(dataSet.Tables[0].Rows, i);

                DateTime result;

                if (columns.Any(item => propertyName.Equals(item)))
                {
                    if (DateTime.TryParseExact(columns.Last(), dateFormat, null, DateTimeStyles.None, out result))
                    {
                        return Task.FromResult(result);
                    }
                }

                if (columns.Count > 2 && propertyName.Equals($"{columns[0]} {columns[1]}"))
                {
                    if (DateTime.TryParseExact(columns.Last(), dateFormat, null, DateTimeStyles.None, out result))
                    {
                        return Task.FromResult(result);
                    }
                }

                i++;
            }

            throw new Exception($"Could not parse property '{propertyName}'!");
        }

        private Task<string> ParseStringProperty(DataSet dataSet, string propertyName)
        {
            var i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                var columns = GetRowColumns(dataSet.Tables[0].Rows, i);

                if (columns.Any(item => propertyName.Equals(item)))
                {
                    return Task.FromResult(columns.Last());
                }

                if (columns.Count > 2 && propertyName.Equals($"{columns[0]} {columns[1]}"))
                {
                    return Task.FromResult(columns.Last());
                }

                i++;
            }

            throw new Exception($"Could not parse property '{propertyName}'!");
        }

        private Task<List<UtilityRow>> ParseUtilityRows(DataSet dataSet, IList<string> dataRows)
        {
            var match = new List<UtilityRow>();

            var i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                var columns = GetRowColumns(dataSet.Tables[0].Rows, i);

                if (columns.Any(item => dataRows.Contains(item)))
                {
                    match.Add(new UtilityRow(columns));
                }

                i++;
            }

            if (!match.Any())
            {
                throw new Exception($"Could not parse properties: '${string.Join(", ", dataRows)}'!");
            }

            return Task.FromResult(match);
        }

        private List<string> GetRowColumns(DataRowCollection rows, int index)
        {
            var result = new List<string>();
            foreach (var column in rows[index].ItemArray)
            {
                if (column is DBNull) continue;
                var value = column.ToString();

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Failed to parse data from row.");
                }

                result.Add(value);
            }

            return result;
        }
    }
}