using System.Collections.Generic;
using System.Globalization;

namespace BillVisualizerWorker.Models
{
    public class BillData
    {
        public BillData(List<string> data)
        {
            Name = data[0];
            Amount = ToDouble(data[1]);
            Unit = data[2];
            UnitPrice = ToDouble(data[3]);
            Cost = ToDouble(data[4]);
        }
      
        public string Name { get; protected set; }
        public double Amount { get; protected set; }
        public string Unit { get; protected set; }
        public double UnitPrice { get; protected set; }
        public double Cost { get; protected set; }

        private static double ToDouble(string data)
        {
            data = data.Replace(',', '.');
            double value;
            double.TryParse(data, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        
            return value;
        }
    }
}