using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BillVisualizerWorker.Models;

namespace BillVisualizer.Services
{
    /// <summary>Service to parse fields from DataSet.</summary>
    public interface IDataSetParser
    {
        /// <summary>Parse search fields from DataSet.</summary>
        /// <param name="dataSet">Set of data.</param>
        /// <returns>List of <see cref="BillData"/>.</returns>
        /// <exception cref="Exception">Is thrown if fails to parse DataSet column.</exception>
        Task<List<BillData>> Parse(DataSet dataSet);
    }
    
    ///<inheritdoc /> 
    public class DataSetParser : IDataSetParser
    {
        // Todo: Later read these values from configuration.
        private readonly List<string> _searchFields = new List<string>
        {
            "Elekter (päevatariifiga)",
            "Elekter (öötariifiga)",
            "Üldelekter",
            "Ampri- ja kuutasu",
            "Vesi",
            "Vee soojendamine",
            "Üldvesi",
            "Soojusenergia",
            "Prügivedu",
            "Haldus-hooldusteenus",
            "Hooldusfond",
            "Juhatuse tasud"
        };
        
        ///<inheritdoc /> 
        public Task<List<BillData>> Parse(DataSet dataSet)
        {
            var match = new List<BillData>();
            
            var i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                var trimmedRow =  new List<string>();
                foreach (var column in dataSet.Tables[0].Rows[i].ItemArray)
                {
                    if (column is DBNull) continue;
                    var value = column.ToString();

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Failed to parse data from row.");
                    }
            
                    trimmedRow.Add(value);
                }

                if (trimmedRow.Any(item => _searchFields.Contains(item)))
                {
                    match.Add(new BillData(trimmedRow));
                }
          
                i++;
            }

            return Task.FromResult(match);
        }
    }
}