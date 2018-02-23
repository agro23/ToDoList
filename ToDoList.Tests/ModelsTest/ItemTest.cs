using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Models.Tests
{
  [TestClass]
    public class ItemTests : IDisposable
    {
        public void Dispose()
        {
            Item.DeleteAll();
        }
        public ItemTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=to_do_test;";
        }

        [TestMethod]
            public void GetAll_DatabaseEmptyAtFirst_0()
            {
              //Arrange, Act
              int result = Item.GetAll().Count;

              //Assert
              Assert.AreEqual(0, result);
            }

        // [TestMethod]
        //     public void Save_SavesToDatabase_ItemList()
        //     {
        //       //Arrange
        //       Item testItem = new Item("Mow the lawn");
        //
        //       //Act
        //       int result = Item.GetAll().Count;
        //
        //       //Assert
        //       Assert.AreEqual(0, result);
        //     }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
          // Arrange, Act
          DateTime fred = new DateTime(2000, 01, 01);
          Item firstItem = new Item("Mow the lawn", fred, 1);
          Item secondItem = new Item("Mow the lawn", fred, 1);

          // Assert
          Assert.AreEqual(firstItem, secondItem);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ItemList()
        {
          //Arrange
          DateTime fred = new DateTime(2000, 01, 01);
          Item testItem = new Item("Mow the lawn", fred, 1);

          //Act
          testItem.Save();
          List<Item> result = Item.GetAll();
          List<Item> testList = new List<Item>{testItem};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          DateTime fred = new DateTime(2000, 01, 01);
          Item testItem = new Item("Mow the lawn", fred, 1);

          //Act
          testItem.Save();
          Item savedItem = Item.GetAll()[0];

          int result = savedItem.GetId();
          int testId = testItem.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item()
        {
          //Arrange
          DateTime fred = new DateTime(2000, 01, 01);
          Item testItem = new Item("Mow the lawn", fred, 1);
          testItem.Save();

          //Act
          Item foundItem = Item.Find(testItem.GetId());

          //Assert
          Assert.AreEqual(testItem, foundItem);
        }

        [TestMethod]
        public void Edit_UpdatesItemInDatabase_String()
        {
          //Arrange

          string firstDescription = "Walk the Dog";
          DateTime fred = new DateTime(2000, 01, 01);
          Item testItem = new Item(firstDescription, fred, 1, 1);
          testItem.Save();
          string secondDescription = "Mow the lawn";

          //Act
          testItem.Edit(secondDescription);

          string result = Item.Find(testItem.GetId()).GetDescription();

          //Assert
          Assert.AreEqual(secondDescription , result);
        }

    }

//***************************************************

// 
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System;
// using ToDoList.Models;
//
// namespace ToDoList.Tests
// {
//
//     [TestClass]
//     public class ItemTests : IDisposable
//     {
//         public ItemTests()
//         {
//             DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=todo_test;";
//         }
//         public void Dispose()
//         {
//           Item.DeleteAll();
//           Category.DeleteAll();
//         }
//
//         [TestMethod]
//         public void Equals_OverrideTrueForSameDescription_Item()
//         {
//           //Arrange, Act
//           Item firstItem = new Item("Mow the lawn", 1);
//           Item secondItem = new Item("Mow the lawn", 1);
//
//           //Assert
//           Assert.AreEqual(firstItem, secondItem);
//         }
//
//         [TestMethod]
//         public void Save_SavesItemToDatabase_ItemList()
//         {
//           //Arrange
//           Item testItem = new Item("Mow the lawn", 1);
//           testItem.Save();
//
//           //Act
//           List<Item> result = Item.GetAll();
//           List<Item> testList = new List<Item>{testItem};
//
//           //Assert
//           CollectionAssert.AreEqual(testList, result);
//         }
//        [TestMethod]
//         public void Save_DatabaseAssignsIdToObject_Id()
//         {
//           //Arrange
//           Item testItem = new Item("Mow the lawn", 1);
//           testItem.Save();
//
//           //Act
//           Item savedItem = Item.GetAll()[0];
//
//           int result = savedItem.GetId();
//           int testId = testItem.GetId();
//
//           //Assert
//           Assert.AreEqual(testId, result);
//         }
//
//         [TestMethod]
//         public void Find_FindsItemInDatabase_Item()
//         {
//           //Arrange
//           Item testItem = new Item("Mow the lawn", 1);
//           testItem.Save();
//
//           //Act
//           Item foundItem = Item.Find(testItem.GetId());
//
//           //Assert
//           Assert.AreEqual(testItem, foundItem);
//         }
//     }
// }

  // [TestClass]
  // public class ProjectTest
  // {
  //   [TestMethod]
  //   public void Test_JustATest_String()
  //   {
  //     Assert.AreEqual("this is a string from the model", ToDoListModel.GetString());
  //   }
  // }
}
