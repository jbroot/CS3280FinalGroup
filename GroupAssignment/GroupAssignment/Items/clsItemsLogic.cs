using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GroupAssignment.Items
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
                itemList = GetItems(ref listLength);
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
        public List<Item> GetItems(ref int rowsAffected)
        {
            try
            {
                List<Item> ItemList = new List<Item>();
                DataSet myData = dbLink.DisplayItemDescTable(ref rowsAffected);

                for (int i = 0; i < rowsAffected; i++)
                {
                    Item newItem = new Item((string)myData.Tables[0].Rows[i][0],
                        (string)myData.Tables[0].Rows[i][1],
                        decimal.ToDouble((decimal)myData.Tables[0].Rows[i][2]));
                    ItemList.Add(newItem);
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
        public Item GetItem(string ItemCode)
        {
            try
            {
                foreach (Item item in itemList)
                {
                    if (item.ItemCode == ItemCode) return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Adds item to list and to database
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="cost"></param>
        /// <returns>null if pKey is already used or the new Item if successful</returns>
        public Item AddItem(string pKey, string ItemDesc, double cost)
        {
            try
            {
                if (dbLink.AddItem(pKey, ItemDesc, cost) == -1) return null;
                Item newItem = new Item(pKey, ItemDesc, cost);
                itemList.Add(newItem);
                ++listLength;
                return newItem;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes item from list and from database
        /// </summary>
        /// <param name="pKey"></param>
        public int DeleteItem(string pKey)
        {
            try
            {
                int delReturn = dbLink.DeleteItem(pKey);
                if (delReturn == 0 || delReturn == -1) return delReturn;
                
                foreach (Item item in itemList)
                {
                    if (item.ItemCode == pKey)
                    {
                        itemList.Remove(item);
                        --listLength;
                        return 1;
                    }
                }
                throw new Exception("Item deleted from database but not linked list");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Updates itemList and database
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="cost"></param>
        /// <returns>Null if pKey isn't found or Item if pKey is found</returns>
        public Item UpdateItem(string pKey, string ItemDesc, double cost)
        {
            try
            {
                if (dbLink.UpdateItem(pKey, ItemDesc, cost) == -1) return null;


                foreach (Item item in itemList)
                {
                    if (item.ItemCode == pKey)
                    {
                        item.ItemDesc = ItemDesc;
                        item.ItemCost = cost;
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
