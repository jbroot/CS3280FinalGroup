using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Line Item
    /// </summary>
    class LineItem
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public Int32 InvoiceNum { get; set; }

        /// <summary>
        /// LineItemNumber
        /// </summary>
        public Int32 LineItemNum { get; set; }

        /// <summary>
        /// Item object associated to LineItem
        /// </summary>
        public Items.Item item { get; set; }
        //public String ItemCode { get; set; }
    }
}
