using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        clsSearchLogic search;
        List<Main.Invoice> invoices;

        //clsSearchSQL searchSQL;
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
    //for troubleshooting only
    //test();
}
        /*
        /// <summary>
        /// Method to help troubleshoot. Delete before final submission
        /// </ummary>
        void test()
        {
            int rowsAffected = 0;
            DateTime date = new DateTime(2018, 4, 23);
            searchSQL.Search(5000, date, ref rowsAffected);
        }*/

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

        private void comboBoxInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //populate datagrid
            //dataGridInvoices.ItemsSource = ;
            try
            {
                if (comboBoxInvoices.SelectedItem != null)
                {
                    search.findInvoice(((Main.Invoice)comboBoxInvoices.SelectedItem).InvoiceNum);

                    dataGridInvoices.Items.Refresh();
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
