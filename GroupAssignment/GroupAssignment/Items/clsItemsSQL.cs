using System;
using System.Data;
using System.Reflection;

namespace GroupAssignment.Items
{
    public class clsItemsSQL
    {
        /// <summary>
        /// Association with the class that directly opens the database
        /// </summary>
        ConnectDB db;

        /// <summary>
        /// Constructs the clsItemsSQL class
        /// </summary>
        public clsItemsSQL()
        {
            try
            {
                db = new ConnectDB();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds an Item to the database
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="cost"></param>
        /// <returns>Number of rows affected or -1 if the pKey is already used</returns>
        public int AddItem(string pKey, string ItemDesc, double cost)
        {
            try
            {
                string result = db.ExecuteScalarSQL("SELECT * FROM ItemDesc WHERE ItemCode = '" + pKey + "';");
                //if item doesn't exist
                if (result == "")
                {
                    return db.ExecuteNonQuery("INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES('" + pKey + "','" + ItemDesc + "','" + cost.ToString() + "');");
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes an ItemCode pKey from the database
        /// </summary>
        /// <param name="pKey"></param>
        /// <returns>InvoiceNum if item is in invoice, 1 if >0 rows affected or 0 if 0 or less rows affected</returns>
        public int DeleteItem(string pKey)
        {
            try
            {
                string invoiceNum = db.ExecuteScalarSQL("select InvoiceNum from LineItems where ItemCode = '" + pKey + "';");
                //If item is in an invoice then return the invoice number
                if (invoiceNum != "")
                {
                    return Int32.Parse(invoiceNum);
                }
                if (db.ExecuteNonQuery("DELETE FROM ItemDesc WHERE ItemCode = '" + pKey + "';") > 0)
                {
                    return 1;
                }
                else return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an Item
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="cost"></param>
        /// <returns>Number of rows affected or -1 if pKey doesn't exist</returns>
        public int UpdateItem(string pKey, string ItemDesc, double cost)
        {
            try
            {

                string result = db.ExecuteScalarSQL("SELECT * FROM ItemDesc WHERE ItemCode = '" + pKey + "';");
                //if item is valid
                if (result != "")
                {
                    return db.ExecuteNonQuery("UPDATE ItemDesc SET ItemDesc = '" + ItemDesc + "', Cost = '" + cost.ToString() + "' WHERE ItemCode='" + pKey + "';");
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all Items in the table
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>Dataset of all columns in ItemDesc</returns>
        public DataSet DisplayItemDescTable(ref int rows)
        {
            try
            {
                return db.ExecuteSQLStatement("SELECT * FROM ItemDesc ORDER BY ItemCode", ref rows);
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

