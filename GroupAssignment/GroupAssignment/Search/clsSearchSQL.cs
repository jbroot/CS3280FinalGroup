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

        public clsSearchSQL()
        {
            try
            {
                db = new ConnectDB();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        public void InvoiceSearch(int invoiceNum, DateTime date)
        {

        }

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

    }
}
