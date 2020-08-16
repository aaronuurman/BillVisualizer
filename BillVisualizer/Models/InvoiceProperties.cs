using System.Collections.Generic;

/// <summary>
/// Set of properties to find from PDF.
/// Property names are defined in invoiceProperties.json.
/// </summary>
public class InvoiceProperties
{
    /// <summary>Property name of the number of invoice on PDF.</summary>
    public string InvoiceNr { get; set; }

    /// <summary>Property name of the date when invoice was sent on PDF.</summary>
    public string Date { get; set; }

    /// <summary>The format of the date on PDF.</summary>
    public string DateFormat {get; set;}

    /// <summary>Property name of the grand total to pay on PDF.</summary>
    public string Total { get; set; }

    /// <summary>
    /// Property names of the services on PDF.
    /// Total is a sum of all these services.
    /// </summary>
    public IList<string> DataRows { get; set; }
}