using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    class clsMainLogic
    {

        //new invoice

        //edit invoide

        //search

        //update items
        Invoice currentInvoice;
        

        

       
        public void displayItemScreen()
        {
            Items.wndItems itemWindow = new Items.wndItems();
            itemWindow.ShowDialog();

            


        }

        public void displaySearchScreen()
        {
            Search.wndSearch searchWindow = new Search.wndSearch();
            searchWindow.ShowDialog();
        }

        public Invoice createInvoice()
        {
            currentInvoice = new Invoice();
            return currentInvoice;
        }

        public List<Items.Item> populateItemList()
        {
            List<Items.Item> items = new List<Items.Item>();
            return items;

            /*Items.clsItemsSQL itemSql = new Items.clsItemsSQL();

            int i = 0;

            DataSet itemData = itemSql.DisplayItemDescTable(ref i);
            foreach (DataTable dt in itemData.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Items.Item item = new Items.Item();
                    item.ItemDesc = row[0].ToString();
                    item.ItemCost = (double)row[1];
                    item.ItemCode = row[2].ToString();

                    items.Add(item);


                   /* long passId = -1;
                    int seatNum = -1;
                    if (long.TryParse(row[0].ToString(), out passId))
                    {
                        if (int.TryParse(row[3].ToString(), out seatNum))
                        {
                            Passengers.Add(new Passenger(passId, row[1].ToString(), row[2].ToString(), seatNum));
                        }
                    }*/
                /*}
            }*/

           




        }

        public Boolean validateInvoice()
        {
            //validate all the screen entered pieces of data to see if they are valid
            validateDateTime();
            validateLineItems();
            return false;
        }

        private Boolean validateDateTime()
        {
            return false;
        }

        private Boolean validateLineItems()
        {
            //foreach
            //validateLineItem(li);
            return false;
        }

        private Boolean validateLineItem(LineItem lineItem)
        {
            return false;
        }



    }
}
