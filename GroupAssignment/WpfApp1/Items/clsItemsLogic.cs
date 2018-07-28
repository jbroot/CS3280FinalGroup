using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GroupAssignment
{
    public class clsItemsLogic
    {
        /// <summary>
        /// Associates with the class that makes SQL statemnts
        /// </summary>
        clsItemsSQL dbLink;
        /// <summary>
        /// A list of all items in the ItemDesc table
        /// </summary>
        public List<Item> itemList;
        /// <summary>
        /// Length of list
        /// </summary>
        public int listLength = 0;
        /// <summary>
        /// Default constructor for the clsItemsLogic class
        /// </summary>
        public clsItemsLogic()
        {
            try
            {
                dbLink = new clsItemsSQL();
                itemList = getItems(ref listLength)
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a dataset from the table then returns a List<Item>
        /// </summary>
        /// <returns>List of Items from the dataset</returns>
        public List<Item> getItems(ref int rowsAffected)
        {
            try
            {
                List<Item> ItemList = new List<Item>();
                DataSet myData = dbLink.DisplayItemDescTable(ref rowsAffected);

                for (int i = 0; i < rowsAffected; i++)
                {
                    ItemList.Add(new Item
                    {
                        ItemCode = (string)myData.Tables[0].Rows[i][0],
                        ItemDesc = (string)myData.Tables[0].Rows[i][1],
                        ItemCost = decimal.ToDouble((decimal)myData.Tables[0].Rows[i][2])
                    }
                    );
                }
                return ItemList;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Finds Item in Itemlist
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns>returns Item if found, else returns null</returns>
        public Item getItem(string ItemCode)
        {
            foreach (var item in itemList)
            {
                if (item.ItemCode == ItemCode) return item;
            }
            return null;
        }
    }
}
