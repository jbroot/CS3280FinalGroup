using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment
{
    public class clsItemsLogic
    {
        clsItemsSQL dbLink;
        public clsItemsLogic()
        {
            dbLink = new clsItemsSQL();
        }

        public List<Item> getItems()
        {
            List < Item > ItemList = new List<Item>();
            int rowsAffected = 0;
            DataSet myData = dbLink.DisplayItemDescTable(ref rowsAffected);

            /*for (int i = 0; i < rowsAffected; i++)
            {
                ItemList.Add(new Item{
                    ItemCode = (string) myData.Tables[0].Rows[i][0],
                    ItemDesc = (string) myData.Tables[0].Rows[i][1],
                    ItemCost = decimal.ToDouble( (decimal) myData.Tables[0].Rows[i][2]) }
                );
            }*/
            return ItemList;
        }

    }
}
