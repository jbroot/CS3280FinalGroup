﻿using GroupAssignment.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

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
        public List<Invoice> invoiceList;
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
        /// Finds an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void findInvoice(int invoiceNum)
        {
            try { 
                invoiceList = new List<Invoice>();
                int rows = 0;
                DataSet ds = dbLink.SearchByInvoice(invoiceNum, ref rows);
                for (int i = 0; i < rows; i++)
                {
                    Invoice newInvoice = new Invoice((int)ds.Tables[0].Rows[i][0], (DateTime)ds.Tables[0].Rows[i][1]);
                    invoiceList.Add(newInvoice);
                }
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
                    Invoice newInvoice = new Invoice((int)myData.Tables[0].Rows[i][0], (DateTime)myData.Tables[0].Rows[i][1]);
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
