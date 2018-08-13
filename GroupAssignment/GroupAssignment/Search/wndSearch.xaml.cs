using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace GroupAssignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Used to hold the selected invoice to the calling window
        /// </summary>
        public Int32 InvoiceID { get; set;  }

        /// <summary>
        /// Business logic for the search folder
        /// </summary>
        clsSearchLogic search;

        /// <summary>
        /// List of invoices
        /// </summary>
        List<Main.Invoice> invoices;

        /// <summary>
        /// Initializes Search Window
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                search = new clsSearchLogic();
                //searchSQL = new clsSearchSQL();

                int rows = 0;
                //invoices = new List<Main.Invoice>();
                comboBoxInvoices.ItemsSource = search.invoiceList;
                //dataGridInvoices.ItemsSource = invoices;
                invoices = search.GetInvoices(ref rows);
            }
            
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Sets the invoice id which will be able to be used by the calling window to pull up the invoice and associated information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceID = 0;
                if (comboBoxInvoices.SelectedItem != null)
                {
                    InvoiceID = ((Main.Invoice)comboBoxInvoices.SelectedItem).InvoiceNum;
                }

                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            try { 
                InvoiceID = 0;
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Actions to take when invoices combobox selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //populate datagrid
            //dataGridInvoices.ItemsSource = ;
            try
            {
                if (comboBoxInvoices.SelectedItem != null)
                {
                    
                    //search.invoiceList = new List<Main.Invoice>();
                    //dataGridInvoices.ItemsSource = search.invoiceList;
                    search.findInvoice(((Main.Invoice)comboBoxInvoices.SelectedItem).InvoiceNum);
                    
                    //dataGridInvoices.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles all errors for this class
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }

        }
    }
}
