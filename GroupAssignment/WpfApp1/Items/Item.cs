using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment
{
    /// <summary>
    /// Item object
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Item Name and Primary Key
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Item Description
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// Item Cost
        /// </summary>
        public double ItemCost { get; set; }
    }
}
