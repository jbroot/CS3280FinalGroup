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
        /// Holds the reference to the items logic class
        /// </summary>
        Items.clsItemsLogic itemLogic;

        /// <summary>
        /// Holds the current invoice if one is being displayed
        /// </summary>
        public Invoice currentInvoice;

        /// <summary>
        /// Holds the active list of line items for the invoice
        /// </summary>
        public List<LineItem> lineItems;

        /// <summary>
        /// hols a list of line items that can be added to an invoice
        /// </summary>
        public List<Items.Item> itemList;

        /// <summary>
        /// Default constructor
        /// </summary>
        public clsMainLogic()
        {
            sql = new clsMainSQL();
            itemLogic = new Items.clsItemsLogic();
            itemList = itemLogic.itemList;
            lineItems = new List<LineItem>();
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
            if (searchWindow.ShowDialog() == true)
            {
                int invoiceid = searchWindow.InvoiceID;
                if (invoiceid != 0)
                {
                    currentInvoice = sql.selectInvoiceByNumber(invoiceid);
                    //currentInvoice.LineItems = 
                    lineItems = currentInvoice.LineItems;
                    

                    //do something to update the scrren

                    //lock the controls
                    
                }

            }
        }

        /// <summary>
        /// Deletes the currently open invoice
        /// </summary>
        public void deleteInvoice()
        {
            if (currentInvoice != null && currentInvoice.InvoiceNum != 0)
            {
                //delete lines
                sql.deleteAllLineItemByInvoiceId(currentInvoice.InvoiceNum);

                //delete invoice
                sql.deleteInvoice(currentInvoice.InvoiceNum);

                currentInvoice = null;
            }
                      
        }

        /// <summary>
        /// Will create a new invoice that can then be inserted into the database
        /// </summary>
        /// <returns>An invoice objecdt</returns>
        public Invoice createInvoice()
        {
            currentInvoice = new Invoice();
            return currentInvoice;
        }
        
        /// <summary>
        /// inserts or updates the current invoice
        /// </summary>
        public void saveInvoice()
        {
            //save invoice
            if (currentInvoice.InvoiceNum == 0)
            {
                //insert invoice
                sql.insertInvoice(currentInvoice.InvoiceDate);
                currentInvoice.InvoiceNum = sql.maxInvoiceNumber();
            }
            else
            {
                //update invoice
                sql.updateInvoice(currentInvoice.InvoiceNum, currentInvoice.InvoiceDate);
            }
            //save line items
            saveLineItems();
        }

        /// <summary>
        /// Save/update/inserts line items
        /// </summary>
        private void saveLineItems()
        {
            currentInvoice.LineItems = lineItems;
            if (currentInvoice.InvoiceNum != 0)
            {
                sql.deleteAllLineItemByInvoiceId(currentInvoice.InvoiceNum);
            }

            for (int i = 0; i < currentInvoice.LineItems.Count; i++)
            {
                sql.insertLineItem(currentInvoice.InvoiceNum, i + 1, currentInvoice.LineItems[i].item.ItemCode);
            }
            
        }
        /// <summary>
        /// Adds a line item to the list of line items for an invoice 
        /// </summary>
        /// <param name="item"></param>
        public void addLineItem(Items.Item item)
        {
            if (item != null)
            {
                LineItem li = new LineItem();
                li.item = item;

                lineItems.Add(li);
            }
        }

        /// <summary>
        /// updates the line item total
        /// </summary>
        public double getTotal()
        {
            double total = 0.0;
            for (int i = 0; i < lineItems.Count; i++)
            {
                total += lineItems[i].item.ItemCost;
            }
            return total;

        }
    }
}
