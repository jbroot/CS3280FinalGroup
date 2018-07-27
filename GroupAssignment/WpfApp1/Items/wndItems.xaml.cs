using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        clsItemsSQL myDBLibrary;
        public wndItems()
        {
            InitializeComponent();
            myDBLibrary = new clsItemsSQL();

            gui();

            test();
        }

        void gui()
        {
            int rowsAffected = 0;
            DataSet myData = myDBLibrary.DisplayItemDescTable(ref rowsAffected);
            ItemDescTableDataGrid.DataContext = myData;
            
        }


        /// <summary>
        /// Test Logic class. Delete before final project
        /// </summary>
        void test()
        {
            myDBLibrary.AddItem("AA", "Test", 100.3553);
            myDBLibrary.UpdateItem("AA", "TestUpdate", 50);
            myDBLibrary.DeleteItem("AA");

            if (myDBLibrary.AddItem("A", "-1", 60) != -1) throw new Exception("-1 didn't return from used pKey");
            myDBLibrary.AddItem("AA", "TestOveride", 56);
            myDBLibrary.DeleteItem("TestOveride", 56);
        }
        
    }
}
