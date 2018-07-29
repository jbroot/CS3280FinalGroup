using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Holds the reference to clsMainSQL for retrieving sql statements
        /// </summary>
        clsMainSQL sql;

        /// <summary>
        /// Holds the variable indicating whether the screen is editable or not
        /// </summary>
        public Boolean isEditable { get; set; }

        /// <summary>
        /// Holds the current invoice if one is being displayed
        /// </summary>
        public Invoice currentInvoice;

        /// <summary>
        /// Holds the active list of line items for the screen
        /// </summary>
        public ObservableCollection<LineItem> lineItems;

        /// <summary>
        /// Default constructor
        /// </summary>
        public clsMainLogic()
        {
            sql = new clsMainSQL();
        }
       /// <summary>
       /// Displays the item windows to manage ite,s
       /// </summary>
        public void displayItemScreen()
        {
            Items.wndItems itemWindow = new Items.wndItems();
            itemWindow.ShowDialog();
            
        }

        /// <summary>
        /// Displays the Search Screen
        /// </summary>
        public void displaySearchScreen()
        {
            Search.wndSearch searchWindow = new Search.wndSearch();
            searchWindow.ShowDialog();
        }

        /// <summary>
        /// Will create a new invoice that can then be inserted into the database
        /// </summary>
        /// <returns></returns>
        public Invoice createInvoice()
        {
            currentInvoice = new Invoice();
            return currentInvoice;
        }

        /// <summary>
        /// Populates the list holding the available items
        /// </summary>
        /// <returns></returns>
        public List<Items.Item> populateItemList()
        {
            List<Items.Item> items = new List<Items.Item>();
            return items;

            /*Items.clsItemsSQL itemSql = new Items.clsItemsSQL();

            int i = 0;

            DataSet itemData = itemSql.DisplayItemDescTable(ref i);
            foreach (DataTable dt in itemData.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Items.Item item = new Items.Item();
                    item.ItemDesc = row[0].ToString();
                    item.ItemCost = (double)row[1];
                    item.ItemCode = row[2].ToString();

                    items.Add(item);


                   /* long passId = -1;
                    int seatNum = -1;
                    if (long.TryParse(row[0].ToString(), out passId))
                    {
                        if (int.TryParse(row[3].ToString(), out seatNum))
                        {
                            Passengers.Add(new Passenger(passId, row[1].ToString(), row[2].ToString(), seatNum));
                        }
                    }*/
                /*}
            }*/

           




        }

        /// <summary>
        /// Validates that all the data elements for an invoice are there before saving
        /// </summary>
        /// <returns></returns>
        public Boolean validateInvoice()
        {
            //validate all the screen entered pieces of data to see if they are valid
            validateDateTime();
            validateLineItems();
            return false;
        }

        /// <summary>
        /// Validates the date for the invoice
        /// </summary>
        /// <returns></returns>
        private Boolean validateDateTime()
        {
            return false;
        }

        /// <summary>
        /// Validates the LineItems for the invoice
        /// </summary>
        /// <returns></returns>
        private Boolean validateLineItems()
        {
            //foreach
            //validateLineItem(li);
            return false;
        }

        /// <summary>
        /// Validates individual line items before saving
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        private Boolean validateLineItem(LineItem lineItem)
        {
            return false;
        }

        /// <summary>
        /// inserts or updates the current invoice
        /// </summary>
        private void saveInvoice()
        {
        
        }
        /// <summary>
        /// Save/update/inserts line items
        /// </summary>
        private void saveLineItems()
        {

        }
        /// <summary>
        /// Adds a line item to the list of line items for an invoice
        /// </summary>
        private void addLineItem()
        {

        }

        /// <summary>
        /// updates the line item total
        /// </summary>
        private void updateTotal()
        {

        }





    }
}
