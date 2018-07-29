using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Search
{
    class searchInvoice
    {
        public int InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        
        public searchInvoice(int invoiceNum, DateTime invoiceDate)
        {
            this.InvoiceNum = invoiceNum;
            this.InvoiceDate = invoiceDate;
        }
    }
}
