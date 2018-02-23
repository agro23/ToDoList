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

    public DateTime GetDate() // ******
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

          // ***************************************************************
          // This is where the conversion to DateTime has to happen!
          // DateTime value = new DateTime(2017, 1, 18);
          // where 2017,1,18 is actually a split of the string as an array
          // and then written into it as DateTime tempDate = new DateTime(tempString[0], teempString[1], tempString[2]);
          // but that means that when I get the date back from the db I also have to convert it on presentation!
          //
          // string[] tempString = this._date.Split('-');
          // Console.WriteLine("The pieces of tempString are: " + tempString[0] + " " + tempString[1] + " " + tempString[2] );
          // // DateTime tempDate = DateTime(int.Parse(tempChars[0]), int.Parse(tempChars[1]), int.Parse(tempChars[2]));
          // DateTime tempDate = new DateTime(2018, 06, 07);
          // date.Value =tempDate;
          date.Value = this._date;
          // **************************************************************

          Console.WriteLine("id.Value = " + this._id);
          Console.WriteLine("description.Value = " + this._description);
          Console.WriteLine("date.Value = " + this._date); // ******
          // Console.WriteLine("date.Value = " + tempDate); // ******


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

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int itemId = 0;
            string itemDescription = "";
            DateTime itemDate = new DateTime(2000,01,01); // *******

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

            Item foundItem= new Item(itemDescription, itemDate, itemId); // ***
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
