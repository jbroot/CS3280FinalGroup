using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    class LineItem
    {
        public Int32 InvoiceNum { get; set; }
        public Int32 LineItemNum { get; set; }

        public Items.Item item { get; set; }
        //public String ItemCode { get; set; }



    }
}
