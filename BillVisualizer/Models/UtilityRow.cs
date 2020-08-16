using System.Collections.Generic;
using System.Globalization;

namespace BillVisualizerWorker.Models
{
    public class UtilityRow
    {
        /* 
        *   TODO: Once dotnet core 5 is out then make property setters protected.
        *   Currently Json serializer does not support a deserialisation to protected properties.
        */
        public UtilityRow() { }
        public UtilityRow(List<string> data)
        {
            Name = data[0];
            Amount = ToDouble(data[1]);
            Unit = data[2];
            UnitPrice = ToDouble(data[3]);
            Cost = ToDouble(data[4]);
        }

        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double Cost { get; set; }

        private static double ToDouble(string data)
        {
            data = data.Replace(',', '.');
            double value;
            double.TryParse(data, NumberStyles.Any, CultureInfo.InvariantCulture, out value);

            return value;
        }
    }
}