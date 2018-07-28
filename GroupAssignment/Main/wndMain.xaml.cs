using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace GroupAssignment.Main
{
   
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        ObservableCollection<LineItem> lineItems;
        Invoice invoice { get; set; }
        clsMainLogic logic { get; set; }
        clsMainSQL dataaccess { get; set; }

        List<Items.Item> items { get; set; }

        public wndMain()
        {
            InitializeComponent();
            logic = new clsMainLogic();
            lineItems = new ObservableCollection<LineItem>();
            dataGridLineItems.Items.Clear();
            dataGridLineItems.ItemsSource = lineItems;
                
            dataaccess = new clsMainSQL();
            //items = logic.
            //comboBoxItemSelection.ItemsSource = logic.;

            //List<Flight> Flights = fa.getFlights();
            //comboBoxFlights.ItemsSource = Flights;


        }

        private void menuItems_Click(object sender, RoutedEventArgs e)
        {
            logic.displayItemScreen();
        }
        private void menuSearch_Click(object sender, RoutedEventArgs e)
        {
            logic.displaySearchScreen();
        }

        private void comboBoxItemSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                textBoxPassengerSeat.Text = "";
                labelAircraft.Content = ((Flight)comboBoxFlights.SelectedItem).Flight_Number;
                populatePassengers((Flight)comboBoxFlights.SelectedItem);
                if (sender == comboBoxFlights)
                {
                    ComboBox cb = (ComboBox)sender;
                    if (cb.SelectedItem != null)
                    {
                        comboBoxPassenger.IsEnabled = true;
                        buttonAddPassenger.IsEnabled = true;
                        if (cb.SelectedValue.ToString().Contains("767"))
                        {
                            draw767();
                        }
                        if (cb.SelectedValue.ToString().Contains("380"))
                        {
                            draw380();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }*/
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            createInvoice();
        }

        private void createInvoice()
        {
            clearScreen();
            logic.createInvoice();

            labelInvoiceNumberData.Content = "TBD";
            
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            setScreenControls(false);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            saveInvoice();
        }

        private void saveInvoice()
        {
            //determine if existing or new and save or update
        }
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clearScreen();
        }

        private void clearScreen()
        {
            labelInvoiceNumberData.Content = "TBD";
            datePickerInvoiceDate.Text = "";
            comboBoxItemSelection.Text = "";
            textBoxPrice.Text = "";

            textBoxTotal.Text = "";
            lineItems.Clear(); // = new ObservableCollection<LineItem>();
            
            //dataGridLineItems.Items.Clear();
            dataGridLineItems.Items.Refresh();


        }

        private void setScreenControls(Boolean isEnabled)
        {
            labelInvoiceNumberData.IsEnabled = isEnabled;
            datePickerInvoiceDate.IsEnabled = isEnabled;
            comboBoxItemSelection.IsEnabled = isEnabled;
            buttonAdd.IsEnabled = isEnabled;
            datePickerInvoiceDate.IsEnabled = isEnabled;
            buttonSave.IsEnabled = isEnabled;
            dataGridLineItems.IsEnabled = isEnabled;


        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            addItem();
        }
        
        private void addItem()
        {

            //just mock code for testing, needs to be replaced with the real stuff
            Items.Item i = new Items.Item("a", "Something good", 59.99);
            //i.ItemCode = "a";
            //i.ItemCost = 59.99;
            //i.ItemDesc = "something good";
                


            LineItem li = new LineItem();
            li.InvoiceNum = 1;
            li.item = i;
            //liItemCode = "a";
            li.LineItemNum = 2;
            lineItems.Add(li);
            //String[] s = new string[] { "a", "b", "c" };
           // dataGridLineItems.Items.Add(s);
            dataGridLineItems.Items.Refresh();
        }

        private void dataGridLineItems_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridLineItems.Columns[0].Header = "Code";
            dataGridLineItems.Columns[1].Header = "Description";
            dataGridLineItems.Columns[2].Header = "Cost";

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            //will prompt to delete the current invoice

        }

        private void buttonTestSql_Click(object sender, RoutedEventArgs e)
        {
            clsMainSQL sql = new clsMainSQL();

            String a = sql.deleteAllLineItemByInvoiceId(44);
            String b = sql.deleteInvoice(100);
            String c = sql.deleteLineItemById(55);


            String d = sql.insertInvoice(new DateTime(2018, 8, 1));
            String f = sql.insertLineItem(5019, 1, "A");

            String r = sql.selectInvoiceByNumber(5000);
            String g = sql.selectLineItemsByInvoiceNumber(5000);

            String h = sql.updateInvoice(5000, new DateTime(2018, 8,1));
            String i = sql.updateLineItems(5019, 1, "C");

            

            ConnectDB db = new ConnectDB();
            db.ExecuteNonQuery(d);
            db.ExecuteNonQuery(f);
            int row1 = 0;
            DataSet r1 = db.ExecuteSQLStatement(r, ref row1);
            int row2 = 0;
            DataSet r2 = db.ExecuteSQLStatement(g, ref row2);

            db.ExecuteNonQuery(h);
            db.ExecuteNonQuery(i);

            String blah = "";




        }
    }
}
