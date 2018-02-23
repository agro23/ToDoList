using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList;

namespace ToDoList.Controllers
{
  public class HomeController : Controller
  {

    // [HttpPost("/add")]
    // public ActionResult AddItem()
    // {
    //   Item newItem = new Item (Request.Form["new-item"]);
    //   var x = Request.Form["new-item"];
    //   Console.WriteLine(newItem +  "::"  + x);
    //   newItem.Save();
    //   List<Item> allItems = Item.GetAll();
    //   Console.WriteLine("This is the new description being sent: " +newItem.GetDescription());
    //   Console.WriteLine("This is the new id being sent: " +newItem.GetId());
    //
    //   return View("Index", allItems);
    //   // return View("Index");
    //
    //   // List<Item> Model = new List<Item> {};
    //   // // Model.Save;
    //   // return View("Index", Model);

    // }


    // [Route("/")]
    // public ActionResult Index()
    // // {
    // //     List<Item> allItems = Item.GetAll();
    // //     return View(allItems);
    // // }
    // {
    //   return View();
    // }

      [HttpGet("/")]
      public ActionResult Index()
      {
      //   List<Item> Model = new List<Item> {};
      //   // Model.Save;
      //   return View("Index", Model);
      List<Item> allItems = Item.GetAll();
      Console.WriteLine("I'm in ItemsIndex()");
      return View("Index", allItems);
      }

      [HttpGet("/items")]
      public ActionResult ItemsIndex()
      {
          List<Item> allItems = Item.GetAll();
          Console.WriteLine("I'm in ItemsIndex()");
          return View("Index", allItems);
      }

      [HttpGet("/date/asc")]
      public ActionResult ToggleDateIndexAsc()
      {
        List<Item> allItems = Item.GetAll();
        Console.WriteLine("Toggle Positive");
        return View("IndexByAsc", allItems);
      }

      [HttpGet("/date/desc")]
      public ActionResult ToggleDateIndexDesc()
      {
        List<Item> allItems = Item.GetAllDesc();
        Console.WriteLine("Toggle Negative");
        return View("IndexByDesc", allItems);
      }

      [HttpGet("/items/new")]
      public ActionResult CreateForm()
      {
        Console.WriteLine("I'm in CreateForm()");
          // return View("Index");
          return View();
      }

      [HttpPost("/items")]
      public ActionResult Create()
      {
        string x = Request.Form["new-item-date"];
        string[] x1 = x.Split('-');
        DateTime x2 = new DateTime(int.Parse(x1[0]), int.Parse(x1[1]), int.Parse(x1[2])).Date;
        Item newItem = new Item (Request.Form["new-item"], x2); // *****
        Console.WriteLine("I'm in Create()");
        Console.WriteLine("The details are: " + Request.Form["new-item"] + " AND " + Request.Form["new-item-date"]); // *****
        //
        newItem.Save();
        List<Item> allItems = Item.GetAll();
        // return RedirectToAction("Index");
        return View("Index", allItems);
        // return View();

      }

      [HttpGet("/items/{id}")]
      public ActionResult Details(int id)
      {
        Console.WriteLine("I'm in Find()");
          Item item = Item.Find(id);
          return View(item);
      }

      [HttpGet("/items/{id}/update")]
      public ActionResult UpdateForm(int id)
      {
        Console.WriteLine("I'm in Update() and ID is " + id);
          Item item = Item.Find(id);
          return View("Update", item);
      }

      [HttpPost("/items/{id}/update")]
      public ActionResult Update(int id)
      {
          Item thisItem = Item.Find(id);
          thisItem.Edit(Request.Form["new-name"]);
          return RedirectToAction("Index");
      }

      [HttpGet("/items/{id}/delete")]
      public ActionResult Delete(int id)
      {
          Item thisItem = Item.Find(id);
          thisItem.Delete(thisItem.GetId());
          return RedirectToAction("Index");
      }


      [HttpPost("/items/delete")]
      public ActionResult DeleteAll()
      {
        Console.WriteLine("I'm in DeleteALL()");
          Item.DeleteAll();
          return View();
      }

  }

}
