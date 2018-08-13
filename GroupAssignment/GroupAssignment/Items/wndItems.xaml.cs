using System;
using System.Reflection;
using System.Windows;

namespace GroupAssignment.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region attributes
        /// <summary>
        /// Associates with the class that makes SQL statements
        /// </summary>
        clsItemsSQL myDBLibrary;
        /// <summary>
        /// Associates with the business logic class
        /// </summary>
        clsItemsLogic myLogic;
        #endregion

        /// <summary>
        /// Initialized the wndItems Window
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();
                myDBLibrary = new clsItemsSQL();
                myLogic = new clsItemsLogic();

                ItemDescTableDataGrid.CanUserAddRows = false;

                RefreshItemDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Refreshes the Item DataGrid
        /// </summary>
        void RefreshItemDataGrid()
        {
            try
            {
                ItemDescTableDataGrid.Items.Clear();
                //Place items in datagrid
                foreach (Item item in myLogic.itemList)
                {
                    ItemDescTableDataGrid.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds item to database, list, and datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorItemCode.Content = ErrorItemCost.Content = ErrorItemDesc.Content = "";
                if(itemCodeText.Text == "")
                {
                    ErrorItemCode.Content = "Name cannot be empty.";
                    return;
                }
                else if(itemDescText.Text == "")
                {
                    ErrorItemDesc.Content = "Item description cannot be empty.";
                    return;
                }
                string pKey = itemCodeText.Text;
                string iDesc = itemDescText.Text;
                double cost = 0;
                //if string to double failed
                if (!double.TryParse(itemCostText.Text, out cost))
                {
                    ErrorItemCost.Content = "Value must only be numbers and one decimal.";
                    return;
                }
                Item newItem = myLogic.AddItem(pKey, iDesc, cost);
                if (newItem == null)
                {
                    ErrorItemCode.Content = "That Item Name has already been taken";
                    return;
                }
                RefreshItemDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Updates item in database, list, and datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorItemCode.Content = ErrorItemCost.Content = ErrorItemDesc.Content = "";
                if (itemCodeText.Text == "")
                {
                    ErrorItemCode.Content = "Name cannot be empty.";
                    return;
                }
                else if (itemDescText.Text == "")
                {
                    ErrorItemDesc.Content = "Item description cannot be empty.";
                    return;
                }
                string pKey = itemCodeText.Text;
                string iDesc = itemDescText.Text;
                double cost = 0;
                //if string to double failed
                if (!double.TryParse(itemCostText.Text, out cost))
                {
                    ErrorItemCost.Content = "Value must only be numbers and one decimal.";
                    return;
                }
                Item newItem = myLogic.UpdateItem(pKey, iDesc, cost);
                if (newItem == null)
                {
                    ErrorItemCode.Content = "That Item Name is not valid";
                    return;
                }
                RefreshItemDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Removes item from database, list, and datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItembutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorItemCode.Content = ErrorItemCost.Content = ErrorItemDesc.Content = "";
                string pKey = itemCodeText.Text;
                int newItem = myLogic.DeleteItem(pKey);
                if (newItem == 0)
                {
                    ErrorItemCode.Content = "That Item Name is not valid.";
                    return;
                }
                else if (newItem != 1)
                {
                    ErrorItemCode.Content = "That Item Name is currently used in invoice number " + newItem.ToString() + ".";
                    return;
                }
                RefreshItemDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Steps to take when one selects a row in the datagrid list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDescTableDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (ItemDescTableDataGrid.SelectedItems.Count <= 0) return;
                Item row = (Item)ItemDescTableDataGrid.SelectedItems[0];
                itemCodeText.Text = row.ItemCode;
                itemDescText.Text = row.ItemDesc;
                itemCostText.Text = row.ItemCost.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
