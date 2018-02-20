using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Models.Tests
{
  [TestClass]
  public class ProjectTest
  {
    [TestMethod]
    public void Test_JustATest_String()
    {
      Assert.AreEqual("this is a string from the model", ToDoListModel.GetString());
    }
  }
}
