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

    // public Item(string Description, int Id = 0)
    public Item(string Description, int Id = 0)
    {
      _id = Id;
      _description = Description;
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
      return (idEquality && descriptionEquality);
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

        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemDescription = rdr.GetString(1);
              Item newItem = new Item(itemDescription, itemId);
              allItems.Add(newItem);
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
             Console.WriteLine("Exception 1: " + ex);
           }

           cmd.CommandText = @"ALTER TABLE items AUTO_INCREMENT = 0;";
           try
           {
             cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
             Console.WriteLine("Exception 2: " + ex);
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
          cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";

          MySqlParameter description = new MySqlParameter();
          description.ParameterName = "@ItemDescription";
          description.Value = this._description;

          Console.WriteLine("id.Value = " + this._id);
          Console.WriteLine("description.Value = " + this._description);

          try
                {
                  cmd.Parameters.Add(description);
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

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int itemId = 0;
            string itemDescription = "";

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
            }

            Item foundItem= new Item(itemDescription, itemId);

            conn.Close();
            if (conn != null)
            {
               conn.Dispose();
            }

            return foundItem;
          }

//...
  }
}
