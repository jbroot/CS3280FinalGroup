using GroupAssignment.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// Links Search's SQL
        /// </summary>
        clsSearchSQL dbLink;
        /// <summary>
        /// List of invoices
        /// </summary>
        List<Invoice> invoiceList;
        /// <summary>
        /// Length of invoiceList
        /// </summary>
        int invoiceLength;

        /// <summary>
        /// Constructs the clsSearchLogic class
        /// </summary>
        public clsSearchLogic()
        {
            try
            {
                dbLink = new clsSearchSQL();
                invoiceList = GetInvoices(ref invoiceLength);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all invoices
        /// </summary>
        /// <param name="rowsAffected"></param>
        /// <returns>a list of all invoices</returns>
        public List<Invoice> GetInvoices(ref int rowsAffected)
        {
            try
            {
                List<Invoice> newInvoiceList = new List<Invoice>();
                DataSet myData = dbLink.DisplayInvoicesTable(ref rowsAffected);

                for (int i = 0; i < rowsAffected; i++)
                {
                    Invoice newInvoice = new Invoice((int)myData.Tables[0].Rows[i][0],
                        (DateTime)myData.Tables[0].Rows[i][1]);
                    newInvoiceList.Add(newInvoice);
                }
                return newInvoiceList;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
