using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    class clsMainSQL
    {/*
        ConnectDB db;

        public clsMainSQL()
        {
            db = new ConnectDB();
        }*/

        public String selectInvoiceByNumber(Int32 invoiceId)
        //public String selectInvoiceByNumber(Invoice invoice)
        {
            String query = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceId + ";";
            return query;
        }

        public String selectLineItemsByInvoiceNumber(Int32 invoiceId)
        {
            String query = "SELECT ItemDesc.*, LineItems.* FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode WHERE InvoiceNum = " + invoiceId + " order by LineItemNum;";
            return query;
        }


        public String deleteInvoice(Int32 invoiceId)
        {
            String query = "DELETE * FROM Invoice where InvoiceNum = " + invoiceId;
            return query;
        }

        public String deleteLineItemById(Int32 LineItemNum)
        {
            String query = "DELETE * FROM LineItems where LineItemNum = " + LineItemNum;
            return query;
        }

        public String deleteAllLineItemByInvoiceId(Int32 invoiceId)
        {
            String query = "DELETE * FROM LineItems where InvoiceId = " + invoiceId;
            return query;
        }


        public String updateInvoice(Int32 invoiceId, DateTime invoiceDate)
        {
            String query = "UPDATE Invoices SET InvoiceDate = '" + invoiceDate.ToShortDateString() + "' WHERE InvoiceNum = " + invoiceId;
            return query;
        }


        public String updateLineItems(Int32 invoiceId, Int32 lineItemNumber, String itemCode) //perhaps pass just the line item
        {
            String query = "UPDATE LineItems SET ItemCode = '" + itemCode + "' WHERE InvoiceNum = " + invoiceId + " AND LineItemNum = " + lineItemNumber;
            return query;
        }

        public String insertInvoice(DateTime invoiceDate)
        {
            String query = "INSERT INTO Invoices (InvoiceDate) VALUES ('" + invoiceDate.ToShortDateString() + "')";
            return query;
        }

        public String insertLineItem(Int32 invoiceNum, Int32 lineNum, String itemCode)
        {
            String query = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineNum + ",'" + itemCode + "')";
            return query;

        }




       
        
        /*
        public void updateInvoice(Invoice invoice, List<LineItem> lineItems)
        {


            //select invoice, and update it
            //select all line items, then match to line items and delete removed, add new, update existing
            ////really just delete the old, and insert the new
            
        }


    */






        
        //save invoice
        
        //save line items

        //get line items

        //


    }
}
