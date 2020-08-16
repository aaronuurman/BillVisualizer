using System;
using System.Collections.Generic;
using BillVisualizerWorker.Models;

namespace BillVisualizer.Models
{
    public class InvoiceData
    {
        /* 
        *   TODO: Once dotnet core 5 is out then make property setters protected.
        *   Currently Json serializer does not support a deserialisation to protected properties.
        */
        public InvoiceData() { }
        public InvoiceData(string invoiceNr, DateTime invoiceForDate, double total)
        {
            InvoiceNr = invoiceNr;
            InvoiceForDate = invoiceForDate;
            Total = total;
        }

        /// <summary>The number of invoice.</summary>
        public string InvoiceNr { get; set; }

        /// <summary>The date when invoice was sent.</summary>
        public DateTime InvoiceForDate { get; set; }

        /// <summary>The grand total to pay.</summary>
        public double Total { get; set; }

        /// <summary>
        /// The services on invoice.
        /// Total is a sum of all these services.
        /// </summary>
        public IList<UtilityRow> UtilityRows { get; set; }

        public void SetUtilityRows(IList<UtilityRow> utilityRows)
        {
            UtilityRows = utilityRows;
        }
    }
}