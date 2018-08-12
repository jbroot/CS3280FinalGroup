namespace GroupAssignment.Items
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
        public Item(string itCode, string itDesc, double itCost)
        {
            this.ItemCode = itCode;
            this.ItemDesc = itDesc;
            this.ItemCost = itCost;
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
