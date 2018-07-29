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
        /// <summary>
        /// Used to hold the selected invoice to the calling window
        /// </summary>
        public Int32 InvoiceID { get; set;  }

        public wndSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the invoice id which will be able to be used by the calling window to pull up the invoice and associated information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            InvoiceID = 0;
            this.Close();
        }
    }
}
