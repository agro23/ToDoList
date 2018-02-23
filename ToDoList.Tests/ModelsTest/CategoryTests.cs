using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Models.Tests
{
  [TestClass]
    public class CategoryTests : IDisposable
    {
        public void Dispose()
        {
            Item.DeleteAll();
        }
        public CategoryTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=to_do_test;";
        }


  [TestMethod]
      public void GetItems_RetrievesAllItemsWithCategory_ItemList()
      {
        Category testCategory = new Category("Household chores");
        testCategory.Save();

        Item firstItem = new Item("Mow the lawn", testCategory.GetId());
        firstItem.Save();
        Item secondItem = new Item("Do the dishes", testCategory.GetId());
        secondItem.Save();


        List<Item> testItemList = new List<Item> {firstItem, secondItem};
        List<Item> resultItemList = testCategory.GetItems();

        CollectionAssert.AreEqual(testItemList, resultItemList);
      }
  }

}
