using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Class that holds all the sql statements that can be executed
    /// </summary>
    class clsMainSQL
    {
        /// <summary>
        /// References the db class
        /// </summary>
        ConnectDB db;

        public clsMainSQL()
        {
            db = new ConnectDB();
        }

        /// <summary>
        /// Sql statment used to select an invoice by number
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL Select statement</returns>
        public Invoice selectInvoiceByNumber(Int32 invoiceId)
        //public String selectInvoiceByNumber(Invoice invoice)
        {
            String query = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceId + ";";
            int rows = 0;
            DataSet data = db.ExecuteSQLStatement(query, ref rows);
            if(rows == 1)
            {
                int i = 0;
                DateTime iDate;
                DateTime.TryParse(data.Tables[0].Rows[0].ToString(), out iDate);

                Int32.TryParse(data.Tables[0].Rows[1].ToString(), out i);
                if(i != 0 && iDate != null)
                {
                    return new Invoice(i, iDate);
                }
            }

            return null;
        }






        /// <summary>
        /// Sql statment used to select line items by an invoice by number
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL Select statement</returns>
        //public List<LineItem> selectLineItemsByInvoiceNumber(Int32 invoiceId)
        public DataSet selectLineItemsByInvoiceNumber(Int32 invoiceId)
        {
            List<LineItem> lines = new List<LineItem>();
            String query = "SELECT LineItems.*, ItemDesc.* FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode WHERE InvoiceNum = " + invoiceId + " order by LineItemNum;";

            //line number 1
            //item code 2
            //desc 4
            //cost 5
            int rows = -1;
            return db.ExecuteSQLStatement(query, ref rows);

            /*
            int rows = 0;
            DataSet data = db.ExecuteSQLStatement(query, ref rows);
            if (rows > 0)
            {
                foreach (DataTable dt in data.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int linenumber = -1;
                        double cost = -1;

                        int.TryParse(row[1].ToString(), out linenumber);
                        double.TryParse(row[5].ToString(), out cost);
                        if(linenumber != -1 && cost != -1)
                        {
                            LineItem line = new LineItem();

                        }





                    }
                }
            }

            
                    
            {
                
                DateTime iDate;
                DateTime.TryParse(data.Tables[0].Rows[0].ToString(), out iDate);

                Int32.TryParse(data.Tables[0].Rows[1].ToString(), out i);
                if (i != 0 && iDate != null)
                {
                    return new Invoice(i, iDate);
                }
            }
            */


            return null;
        }

        /// <summary>
        /// Sql statment used to delete an invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL delete statement</returns>
        public String deleteInvoice(Int32 invoiceId)
        {
            String query = "DELETE * FROM Invoice where InvoiceNum = " + invoiceId;
            return query;
        }

        /// <summary>
        /// Sql statment used to delete a single line item by id
        /// </summary>
        /// <param name="LineItemNum"></param>
        /// <returns>SQL delete statement</returns>
        public int  deleteLineItemById(Int32 LineItemNum)
        {
            String query = "DELETE * FROM LineItems where LineItemNum = " + LineItemNum;
            return db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// Sql statment to delete all line items by invoice id'
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL delete statement</returns>
        public int deleteAllLineItemByInvoiceId(Int32 invoiceId)
        {
            String query = "DELETE * FROM LineItems where InvoiceId = " + invoiceId;
            return db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// Sql statement to update and existing invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>SQL Update statement</returns>
        public int updateInvoice(Int32 invoiceId, DateTime invoiceDate)
        {
            String query = "UPDATE Invoices SET InvoiceDate = #" + invoiceDate.ToShortDateString() + "# WHERE InvoiceNum = " + invoiceId;
            return db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// SQL statmeent to update line item
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="lineItemNumber"></param>
        /// <param name="itemCode"></param>
        /// <returns>Sql update statement</returns>
        public int updateLineItem(Int32 invoiceId, Int32 lineItemNumber, String itemCode) //perhaps pass just the line item
        {
            String query = "UPDATE LineItems SET ItemCode = '" + itemCode + "' WHERE InvoiceNum = " + invoiceId + " AND LineItemNum = " + lineItemNumber;
            return db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// QL statmeent to inserts a new invoice
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns>sql insert statement</returns>
        public int insertInvoice(DateTime invoiceDate)
        {
            String query = "INSERT INTO Invoices (InvoiceDate) VALUES (#" + invoiceDate.ToShortDateString() + "#)";
            return db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// QL statmeent to insert a line item
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineNum"></param>
        /// <param name="itemCode"></param>
        /// <returns>sql insert statement</returns>
        public int insertLineItem(Int32 invoiceNum, Int32 lineNum, String itemCode)
        {
            String query = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineNum + ",'" + itemCode + "')";
            return db.ExecuteNonQuery(query);

        }

        /// <summary>
        /// Returns true if the item is found on an invoice
        /// </summary>
        /// <returns></returns>
        public Boolean isItemOnInvoice(String itemCode)
        {
            String query = "SELECT * FROM LineItems WHERE ItemCode = " + itemCode;
            int rows = 0;
            db.ExecuteSQLStatement(query, ref rows);
            if(rows > 0)
            {
                return true;
            }
            return false;
        }
    }
}
