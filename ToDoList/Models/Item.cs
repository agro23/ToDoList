using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private int _id;
    private string _description;
    private DateTime _date; // *********

    // public Item(string Description, int Id = 0)
    public Item(string Description, DateTime Date, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _date = Date; // ******
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
      Item newItem = (Item) otherItem;
      bool idEquality = (this.GetId() == newItem.GetId());
      bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
      bool dateEquality = (this.GetDate() == newItem.GetDate()); // *******
      return (idEquality && descriptionEquality && dateEquality); // ******
      }
    }

    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    //...GETTERS AND SETTERS WILL GO HERE...

    public int GetId()
    {
      return _id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public DateTime GetDate()
    {
      return _date;
    }

        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            // cmd.CommandText = @"SELECT * FROM items;";
            cmd.CommandText = @"SELECT * FROM items ORDER BY date ASC;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemDescription = rdr.GetString(1);
              DateTime itemDate = rdr.GetDateTime(2).Date; // is it okay to get a DATE from a db as String?
              Item newItem = new Item(itemDescription, itemDate.Date, itemId);
              allItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

        public static List<Item> GetAllDesc()
        {
            List<Item> allItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            // cmd.CommandText = @"SELECT * FROM items;";


            cmd.CommandText = @"SELECT * FROM items ORDER BY date DESC;";

            try
            {
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemDescription = rdr.GetString(1);
              DateTime itemDate = rdr.GetDateTime(2).Date; // is it okay to get a DATE from a db as String?
              Item newItem = new Item(itemDescription, itemDate.Date, itemId);
              allItems.Add(newItem);
            }
            }
            catch (Exception ex)
            {
              Console.WriteLine("Exception Descending: " + ex);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }


        public static void DeleteAll()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM items;";
           try
           {
             cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
             Console.WriteLine("Exception 10: " + ex);
           }

           cmd.CommandText = @"ALTER TABLE items AUTO_INCREMENT = 0;";
           try
           {
             cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
             Console.WriteLine("Exception 11: " + ex);
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          // cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";
          cmd.CommandText = @"INSERT INTO `items` (`description`, `date`) VALUES (@ItemDescription, @ItemDate);";

          MySqlParameter description = new MySqlParameter();
          description.ParameterName = "@ItemDescription";
          description.Value = this._description;

          MySqlParameter date = new MySqlParameter();
          date.ParameterName = "@ItemDate";
          date.Value = this._date;
          Console.WriteLine("id.Value = " + this._id);
          Console.WriteLine("description.Value = " + this._description);
          Console.WriteLine("date.Value = " + this._date); // ******

          try
                {
                  cmd.Parameters.Add(description);
                  cmd.Parameters.Add(date);
                }
                catch (Exception ex)
                {
                  Console.WriteLine("Exception 1: " + ex);
                }

          try
                {
                  cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                  Console.WriteLine("Exception 2: " + ex);
                }

          try
                {
                  _id = (int) cmd.LastInsertedId;
                }
                catch (Exception ex)
                {
                  Console.WriteLine("Exception 3: " + ex);
                }

          Console.WriteLine("_id = " + this._id);

         conn.Close();
         if (conn != null)
         {
             conn.Dispose();
         }
        }

        public static Item Find(int id)
        {
            // Item foundItem= new Item("testDescription");
            // return foundItem;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            // try
            // {
            //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
            // }
            //   catch (Exception ex)
            // {
            //   Console.WriteLine("Find() Exception: " + ex);
            // }

            Console.WriteLine("Got this far in Find!");
            int itemId = 0;
            string itemDescription = "";
            DateTime itemDate = new DateTime(2000,01,01); // *******

            try
            {
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
              while (rdr.Read())
              {
                // if (!rdr.IsDBNull(0))
                // {
                //   itemId = rdr.GetInt32(0);
                // } else
                // {
                //   itemId = 0;
                // }
                // if (!rdr.IsDBNull(1))
                // {
                //   itemDescription = rdr.GetString(1);
                // } else
                // {
                //   itemDescription = "";
                // }
                 itemId = rdr.GetInt32(0);
                 itemDescription = rdr.GetString(1);
                 itemDate = rdr.GetDateTime(2); // *******
              }
            }
            catch (Exception ex)
            {
              Console.WriteLine("Find() Exception: " + ex);
            }

            Console.WriteLine("itemDescription = " + itemDescription);
            Console.WriteLine("itemDate = " + itemDate);
            Console.WriteLine("itemId = " + itemId);
            Item foundItem= new Item(itemDescription, itemDate, itemId);

            conn.Close();
            if (conn != null)
            {
               conn.Dispose();
            }

            return foundItem;
          }


          public void Edit(string newDescription)
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _description = newDescription;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
          }

          public void Delete(int Id)
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            // cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
            cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = Id;
            cmd.Parameters.Add(thisId);
            //
            // MySqlParameter description = new MySqlParameter();
            // description.ParameterName = "@newDescription";
            // description.Value = newDescription;
            // cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            // _description = newDescription;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
          }

//...
  }
}
