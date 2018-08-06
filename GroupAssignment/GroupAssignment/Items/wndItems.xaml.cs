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
        /// <summary>
        /// Associates with the class that makes SQL statements
        /// </summary>
        clsItemsSQL myDBLibrary;
        /// <summary>
        /// Associates with the business logic class
        /// </summary>
        clsItemsLogic myLogic;

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

                //for troubleshooting only
                //test();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
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
                    ErrorItemCode.Content = "That Item Code has already been taken";
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
                    ErrorItemCode.Content = "That Item Code is not valid";
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
                    ErrorItemCode.Content = "That Item Code is not valid";
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

        /*
        /// <summary>
        /// Tests code. Delete before final project
        /// </summary>
        void test()
        {
            //Test SQL
            myDBLibrary.AddItem("AA", "Test", 100.35);
            myDBLibrary.UpdateItem("AA", "TestUpdate", 50);
            myDBLibrary.DeleteItem("AA");

            if (myDBLibrary.AddItem("A", "-1", 60) != -1) throw new Exception("-1 didn't return from used pKey");
            myDBLibrary.AddItem("AA", "TestOveride", 56);
            myDBLibrary.DeleteItem("TestOveride", 56);

            //Test getItem()
            Item testGet = myLogic.GetItem("A");

            //Test itemList
            string listCheck = "";

            foreach (var Item in myLogic.itemList)
            {
                listCheck += Item.ItemCode + "." + Item.ItemDesc + "." + Item.ItemCost.ToString() + "\n";
            }

            MessageBox.Show(listCheck);

        }*/
    }
}
