using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace GroupAssignment
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
        /// A list of all items in the ItemDesc table
        /// </summary>
        List<Item> itemList;
        /// <summary>
        /// Length of list
        /// </summary>
        int listLength = 0;

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
                itemList = myLogic.getItems(ref listLength);

                ItemDescTableDataGrid.CanUserAddRows = false;

                gui();

                //for troubleshooting only
                //test();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Helps initialize the GUI
        /// </summary>
        void gui()
        {
            
        }

        
        /// <summary>
        /// Test SQL class. Delete before final project
        /// </summary>
        void test()
        {
            myDBLibrary.AddItem("AA", "Test", 100.35);
            myDBLibrary.UpdateItem("AA", "TestUpdate", 50);
            myDBLibrary.DeleteItem("AA");

            if (myDBLibrary.AddItem("A", "-1", 60) != -1) throw new Exception("-1 didn't return from used pKey");
            myDBLibrary.AddItem("AA", "TestOveride", 56);
            myDBLibrary.DeleteItem("TestOveride", 56);

            string listCheck = "";

            foreach (var Item in itemList)
            {
                listCheck += Item.ItemCode + "." + Item.ItemDesc + "." + Item.ItemCost.ToString() + "\n";
            }

            MessageBox.Show(listCheck);

        }
        
    }
}
