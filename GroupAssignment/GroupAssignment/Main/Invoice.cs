using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Invoice object
    /// </summary>
    class Invoice
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public Int32 InvoiceNum { get; set; }
        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// List of LineItems associated to Invoice
        /// </summary>
        public List<LineItem> LineItems {get; set; }

        /// <summary>
        /// Default constructor for a new invoice
        /// </summary>
        public Invoice()
        {
            try
            {
                LineItems = new List<LineItem>();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Constructor for a new invoice that you can pass the parameters into
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        public Invoice(Int32 invoiceNum, DateTime invoiceDate)
        {
            try
            {
                this.InvoiceNum = invoiceNum;
                this.InvoiceDate = invoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// To string method used by the combo boxes to display the object
        /// </summary>
        /// <returns>String of the object</returns>
        override
        public String ToString()
        {
            try
            {
                return InvoiceNum.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
