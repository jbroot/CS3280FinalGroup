using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    class Invoice
    {
        public Int32 InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<LineItem> LineItems {get; set; }

        public Invoice()
        {
            LineItems = new List<LineItem>();

        }
    }
}
