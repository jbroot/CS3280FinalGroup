using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Invoice object
    /// </summary>
    class Invoice
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public Int32 InvoiceNum { get; set; }
        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// List of LineItems associated to Invoice
        /// </summary>
        public List<LineItem> LineItems {get; set; }

        /// <summary>
        /// Default constructor for a new invoice
        /// </summary>
        public Invoice()
        {
            LineItems = new List<LineItem>();

        }

        /// <summary>
        /// Constructor for a new invoice that you can pass the parameters into
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        public Invoice(Int32 invoiceNum, DateTime invoiceDate)
        {
            this.InvoiceNum = invoiceNum;
            this.InvoiceDate = invoiceDate;
        }
    }
}
