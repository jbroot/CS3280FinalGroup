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

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        wndItems itemWindow;
        public wndMain()
        {
            InitializeComponent();
            itemWindow = new wndItems();
        }

        private void ItemsWindow_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.ShowDialog();
        }
    }
}
