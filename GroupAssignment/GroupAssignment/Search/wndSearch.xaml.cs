using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GroupAssignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchLogic myLogic;

        clsSearchSQL searchSQL;
        public wndSearch()
        {
            InitializeComponent();
            myLogic = new clsSearchLogic();
            searchSQL = new clsSearchSQL();
            //for troubleshooting only
            test();
        }

        void test()
        {
            int rowsAffected = 0;
            DateTime date = new DateTime(2018, 4, 23);
            searchSQL.Search(5000, date, ref rowsAffected);
        }
    }
}
