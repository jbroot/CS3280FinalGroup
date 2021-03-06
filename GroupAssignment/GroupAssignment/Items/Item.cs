﻿namespace GroupAssignment.Items
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

        /// <summary>
        /// Constructor for Item
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        public Item(string itemCode, string itemDesc, double itemCost)
        {
            this.ItemCode = itemCode;
            this.ItemDesc = itemDesc;
            this.ItemCost = itemCost;
        }

        /// <summary>
        /// Overrides the string method to return the item description for picklists
        /// </summary>
        /// <returns>String description of the item</returns>
        override
        public string ToString()
        {
            return this.ItemDesc;
        }
    }
}
