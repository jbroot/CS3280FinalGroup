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

        /// <summary>
        /// default constructor
        /// </summary>
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
            try { 
                String query = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceId + ";";
                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(query, ref rows);
                if (rows == 1)
                {
                    int i = 0;
                    DateTime iDate;
                    DateTime.TryParse(data.Tables[0].Rows[0][1].ToString(), out iDate);

                    Int32.TryParse(data.Tables[0].Rows[0][0].ToString(), out i);

                    if (i != 0 && iDate != null)
                    {
                            Invoice inv = new Invoice(i, iDate);
                            inv.LineItems = selectLineItemsByInvoiceNumber(i);
                            return inv;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Sql statment used to select line items by an invoice by number
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL Select statement</returns>
        //public List<LineItem> selectLineItemsByInvoiceNumber(Int32 invoiceId)
        public List<LineItem> selectLineItemsByInvoiceNumber(Int32 invoiceId)
        {
            List<LineItem> lines = new List<LineItem>();
            try { 
                String query = "SELECT LineItems.*, ItemDesc.* FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode WHERE InvoiceNum = " + invoiceId + " order by LineItemNum;";

                //line number 1
                //item code 2
                //desc 4
                //cost 5
                int rows = -1;
                DataSet data = db.ExecuteSQLStatement(query, ref rows);
                for (int i = 0; i < rows; i++)
                {
                    double price = 0.0;
                    double.TryParse(data.Tables[0].Rows[i][5].ToString(), out price);
                    Items.Item item = new Items.Item(data.Tables[0].Rows[i][3].ToString(), data.Tables[0].Rows[i][4].ToString(), price);



                    LineItem li = new LineItem();
                    li.InvoiceNum = (int)data.Tables[0].Rows[i][0];
                    li.LineItemNum = (int)data.Tables[0].Rows[i][1];
                    li.item = item;

                    lines.Add(li);
                 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return lines;

        }

        /// <summary>
        /// Sql statment used to delete an invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL delete statement</returns>
        public int deleteInvoice(int invoiceId)
        {
            try { 
                String query = "DELETE FROM Invoices where InvoiceNum = " + invoiceId;
                return db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Sql statment to delete all line items by invoice id'
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>SQL delete statement</returns>
        public int deleteAllLineItemByInvoiceId(int invoiceId)
        {
            try { 
                String query = "DELETE FROM LineItems where InvoiceNum = " + invoiceId;
                return db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Sql statement to update and existing invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>SQL Update statement</returns>
        public int updateInvoice(Int32 invoiceId, DateTime invoiceDate)
        {
            try { 
                String query = "UPDATE Invoices SET InvoiceDate = #" + invoiceDate.ToShortDateString() + "# WHERE InvoiceNum = " + invoiceId;
                return db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// QL statmeent to inserts a new invoice
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns>sql insert statement</returns>
        public int insertInvoice(DateTime invoiceDate)
        {
            try
            {
                String query = "INSERT INTO Invoices (InvoiceDate) VALUES (#" + invoiceDate.ToShortDateString() + "#)";
                return db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try { 
            String query = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineNum + ",'" + itemCode + "')";
            return db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns true if the item is found on an invoice
        /// </summary>
        /// <returns>Boolean indicating if there the item exists on an invoice</returns>
        public Boolean isItemOnInvoice(String itemCode)
        {
            try { 
                String query = "SELECT * FROM LineItems WHERE ItemCode = " + itemCode;
                int rows = 0;
                db.ExecuteSQLStatement(query, ref rows);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Returns the max invoice number
        /// </summary>
        /// <returns></returns>
        public int maxInvoiceNumber()
        {
            try
            {
                string query = "SELECT MAX(InvoiceNum) FROM Invoices";
                int rows = 0;
                DataSet ds = db.ExecuteSQLStatement(query, ref rows);
                int result = -1;
                int.TryParse(ds.Tables[0].Rows[0].ToString(), out result);
                return result;
            }
            
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
