using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace GroupAssignment.Main
{

    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {


        /// <summary>
        /// Reference to the clsMainLogic for easier usage throughout the class
        /// </summary>
        clsMainLogic logic { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                logic = new clsMainLogic();
                dataGridLineItems.Items.Clear();
                dataGridLineItems.ItemsSource = logic.lineItems;
                comboBoxItemSelection.ItemsSource = logic.itemList;
                setScreenControls(false);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);

            }
        }

        /// <summary>
        /// Click event handler for showing the edit items screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logic.displayItemScreen();
                comboBoxItemSelection.ItemsSource = logic.itemList;
                refreshInvoiceDisplay();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);

            }
        }

        /// <summary>
        /// refreshes the current invoice to update the page
        /// </summary>
        private void refreshInvoiceDisplay()
        {
            setScreenControls(true);
            //logic.displaySearchScreen();

            if (logic.currentInvoice != null)
            {
                labelInvoiceNumberData.Content = logic.currentInvoice.InvoiceNum.ToString();
                datePickerInvoiceDate.Text = logic.currentInvoice.InvoiceDate.ToShortDateString();
                textBoxTotal.Text = logic.getTotal().ToString();
                logic.refreshCurrentInvoice();
                dataGridLineItems.ItemsSource = logic.lineItems;
                dataGridLineItems.Items.Refresh();
                textBoxTotal.Text = logic.getTotal().ToString();
            }
            setScreenControls(false);
        }

        /// <summary>
        /// Click ent handler for showing the search screen and getting results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                setScreenControls(true);
                logic.displaySearchScreen();

                if (logic.currentInvoice != null)
                {
                    refreshInvoiceDisplay();
                }
                dataGridLineItems.ItemsSource = logic.lineItems;
                setScreenControls(false);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// detects the change of sleection of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItemSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                textBoxPrice.Text = "";
                if (comboBoxItemSelection.SelectedItem != null)
                {
                    textBoxPrice.Text = ((Items.Item)comboBoxItemSelection.SelectedItem).ItemCost.ToString();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Event handler for new button to creates a new invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                createInvoice();
                buttonEdit.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Creates a call to get a new invoice
        /// </summary>
        private void createInvoice()
        {
            try
            {
                clearScreen(!buttonAdd.IsEnabled);
                setScreenControls(true);
                logic.createInvoice();
                labelInvoiceNumberData.Content = "TBD";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Click handler for the edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setScreenControls(true);
                buttonEdit.IsEnabled = false;                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private Boolean hasGridErrors()
        {
            try
            {
                var errors = (from c in (from object i in dataGridLineItems.ItemsSource select dataGridLineItems.ItemContainerGenerator.ContainerFromItem(i)) where c != null select Validation.GetHasError(c)).FirstOrDefault(x => x);
                if (errors)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Verifies and saves the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (hasGridErrors())
                {
                    MessageBox.Show("There is a problem with one or more line items. Please correct the error before saving.", "Data Error", MessageBoxButton.OK);
                    return;
                }

                //check date
                DateTime invoiceDate;
                if (datePickerInvoiceDate.SelectedDate == null)
                {
                    MessageBox.Show("Please set an invoice date", "Set Date", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    invoiceDate = (DateTime)datePickerInvoiceDate.SelectedDate;
                    logic.currentInvoice.InvoiceDate = invoiceDate;
                }

                //check line items
                if (logic.lineItems.Count == 0)
                {
                    MessageBox.Show("Cannot save a blank invoice. Please add items to the invoice.", "No Line Items", MessageBoxButton.OK);
                    return;
                }

                logic.saveInvoice(); 
                labelInvoiceNumberData.Content = logic.currentInvoice.InvoiceNum.ToString();
                setScreenControls(false);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Clears the current invoice information from the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearScreen(false);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Clears the screen controls for a new invoice
        /// </summary>
        private void clearScreen(Boolean skipPrompt)
        {
            try
            {
                if (skipPrompt || MessageBox.Show("Are you sure you want to clear the current invoice information and line items?", "Clear Invoice", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    labelInvoiceNumberData.Content = "";
                    datePickerInvoiceDate.Text = "";
                    comboBoxItemSelection.Text = "";
                    textBoxPrice.Text = "";

                    textBoxTotal.Text = "";
                    logic.currentInvoice = null;
                    logic.lineItems.Clear(); 
                    dataGridLineItems.Items.Refresh();
                }
                setScreenControls(false);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sets the controls for editing enabled/disabled
        /// </summary>
        /// <param name="isEnabled"></param>
        private void setScreenControls(Boolean isEnabled)
        {
            try
            {
                datePickerInvoiceDate.IsEnabled = isEnabled;
                comboBoxItemSelection.IsEnabled = isEnabled;
                buttonAdd.IsEnabled = isEnabled;
                datePickerInvoiceDate.IsEnabled = isEnabled;
                buttonSave.IsEnabled = isEnabled;
                dataGridLineItems.IsEnabled = isEnabled;

                buttonEdit.IsEnabled = ((logic.currentInvoice == null) ? false : true);

                menuItems.IsEnabled = !buttonAdd.IsEnabled;
                buttonDelete.IsEnabled = ((logic.currentInvoice != null && logic.currentInvoice.InvoiceNum != 0) ? true : false);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// add button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                addItem();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// adds the currently selected item to the invoice
        /// </summary>
        private void addItem()
        {
            try
            {
                if (comboBoxItemSelection.SelectedItem == null)
                {
                    MessageBox.Show("Please select and item to add", "Cannot add item", MessageBoxButton.OK);
                    return;
                }

                logic.addLineItem((Items.Item)comboBoxItemSelection.SelectedItem);
                dataGridLineItems.Items.Refresh();
                comboBoxItemSelection.SelectedIndex = -1;
                textBoxTotal.Text = logic.getTotal().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// creates the grid column headers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridLineItems_AutoGeneratedColumns(object sender, EventArgs e)
        {
            try
            {
                dataGridLineItems.Columns[0].Header = "Code";
                dataGridLineItems.Columns[1].Header = "Description";
                dataGridLineItems.Columns[2].Header = "Cost";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// deletes the current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //will prompt to delete the current invoice
                if (logic.currentInvoice != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete the current invoice?", "Delete Invoice", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        //delete lines
                        logic.deleteInvoice();
                        logic.currentInvoice = null;
                        clearScreen(true);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// confirms that the user wants to delete the row from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridLineItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null)
                {
                    DataGridRow dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                    if (e.Key == Key.Delete && !dgr.IsEditing)
                    {
                        // User is attempting to delete the row
                        var result = MessageBox.Show("Are you sure you want to delete the current item(s)?", "Delete row", MessageBoxButton.OKCancel);
                        e.Handled = (result == MessageBoxResult.Cancel);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// updates the total when the selection changes (like when a row is deleted)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridLineItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                textBoxTotal.Text = logic.getTotal().ToString();

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
