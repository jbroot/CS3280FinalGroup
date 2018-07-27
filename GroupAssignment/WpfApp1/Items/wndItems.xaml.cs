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
        clsItemsLogic myLogic;
        public wndItems()
        {
            InitializeComponent();
            myLogic = new clsItemsLogic();

            gui();

            test();
        }

        void gui()
        {
            int rowsAffected = 0;
            DataSet myData = myLogic.DisplayItemDescTable(ref rowsAffected);
            ItemDescTableDataGrid.DataContext = myData;
            
        }


        /// <summary>
        /// Test Logic class. Delete before final project
        /// </summary>
        void test()
        {
            myLogic.AddItem("AA", "Test", 100.3553);
            myLogic.UpdateItem("AA", "TestUpdate", 50);
            myLogic.DeleteItem("AA");

            if (myLogic.AddItem("A", "-1", 60) != -1) throw new Exception("-1 didn't return from used pKey");
            myLogic.AddItem("AA", "TestOveride", 56);
            myLogic.DeleteItem("TestOveride", 56);
        }
        
    }
}
