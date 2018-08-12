using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupAssignment.Main;

namespace GroupAssignment.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// Associates with the class that opens the database
        /// </summary>
        ConnectDB db;

        /// <summary>
        /// Constructs clsSearchSQL
        /// </summary>
        public clsSearchSQL()
        {
            try
            {
                db = new ConnectDB();
            }
            catch (Exception ex) { 
            
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Searches for invoiceNum and date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="date"></param>
        /// <param name="rowsAffected"></param>
        /// <returns>Dataset[InvoiceNum][InvoiceDate][totalCost]</returns>
        public DataSet Search(int invoiceNum, DateTime date, ref int rowsAffected)
        {
            try
            {
                return db.ExecuteSQLStatement("SELECT Invoices.InvoiceNum, Invoices.InvoiceDate, Sum(ItemDesc.Cost) AS SumOfCost " +
                    "FROM Invoices INNER JOIN(ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode) ON Invoices.InvoiceNum = LineItems.InvoiceNum " +
                    "WHERE Invoices.InvoiceNum = " + invoiceNum +
                    " AND InvoiceDate = #" + date.ToShortDateString() +
                    "# GROUP BY Invoices.InvoiceNum, Invoices.InvoiceDate" + ";", ref rowsAffected);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public DataSet SearchByInvoice(int invoiceNum, ref int rowsAffected)
        {
            try
            {
                return db.ExecuteSQLStatement("SELECT Invoices.InvoiceNum, Invoices.InvoiceDate, Sum(ItemDesc.Cost) AS SumOfCost " +
                    "FROM Invoices INNER JOIN(ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode) ON Invoices.InvoiceNum = LineItems.InvoiceNum " +
                    "WHERE Invoices.InvoiceNum = " + invoiceNum +
                    " GROUP BY Invoices.InvoiceNum, Invoices.InvoiceDate" + ";", ref rowsAffected);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Displays all Invoices with the total cost as a third column
        /// </summary>
        /// <param name="rowsAffected"></param>
        /// <returns>Dataset</returns>
        public DataSet DisplayInvoicesTotalCostTable(ref int rowsAffected)
        {
            try
            {
                return db.ExecuteSQLStatement("SELECT Invoices.InvoiceNum, Invoices.InvoiceDate, Sum(ItemDesc.Cost) AS SumOfCost " +
                    "FROM Invoices INNER JOIN(ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode) ON Invoices.InvoiceNum = LineItems.InvoiceNum " +
                    "GROUP BY Invoices.InvoiceNum, Invoices.InvoiceDate" + ";", ref rowsAffected);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all rows in the Invoices Table
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>all rows in the Invoices Table</returns>
        public DataSet DisplayInvoicesTable(ref int rows)
        {
            try
            {
                return db.ExecuteSQLStatement("SELECT * FROM Invoices ORDER BY InvoiceNum", ref rows);
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string MaxInvoiceNum()
        {
            try
            {
                return db.ExecuteScalarSQL("SELECT MAX(InvoiceNum) FROM Invoices");
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
