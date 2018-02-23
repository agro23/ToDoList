using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{

  public class Category
  {
    private int _id;
    private string _name;

    public Category(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId() {return _id;}
    public string GetName() {return _name;}

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Category))
        return false;
      Category newCategory = (Category) otherItem;
      return _name == newCategory._name && _id == newCategory._id;
    }
    public override int GetHashCode()
    {
        return (_name + _id).GetHashCode();
    }

    public void AddItem(Item item)
    {

    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO categories (name) VALUES (@Name);";

      cmd.Parameters.Add(new MySqlParameter("@Name", _name));

      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static List<Category> GetAll()
    {
      List<Category> allCategoreis = new List<Category>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category category = new Category(categoryName, categoryId);
        allCategoreis.Add(category);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();

      return allCategoreis;
    }

    public static void Clear()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM categories; ALTER TABLE categories AUTO_INCREMENT = 1;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static Category Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CategoryId = 0;
      string CategoryName = "";

      if (rdr.Read())
      {
        CategoryId = rdr.GetInt32(0);
        CategoryName = rdr.GetString(1);
      }
      Category newCategory = new Category(CategoryName, CategoryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCategory;
    }

    public static void Remove(int id)
    {

    }



    public List<Item> GetItems()
            {
                List<Item> allCategoryItems = new List<Item> {};
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM items WHERE category_id = @category_id;";

                MySqlParameter categoryId = new MySqlParameter();
                categoryId.ParameterName = "@category_id";
                categoryId.Value = this._id;
                cmd.Parameters.Add(categoryId);


                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                while(rdr.Read())
                {
                  int itemId = rdr.GetInt32(0);
                  string itemDescription = rdr.GetString(1);
                  DateTime itemDate = rdr.GetDateTime(2); // *******
                  int itemCategoryId = rdr.GetInt32(3);
                  Item newItem = new Item(itemDescription, itemDate, itemCategoryId, itemId);
                  allCategoryItems.Add(newItem);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return allCategoryItems;
            }


  }
}
