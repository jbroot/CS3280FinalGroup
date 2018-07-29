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

                gui();
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

    }
}
