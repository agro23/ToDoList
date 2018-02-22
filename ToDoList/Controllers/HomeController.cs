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
        List<Item> Model = new List<Item> {};
        // Model.Save;
        return View("Index", Model);
      }

      [HttpGet("/items")]
      public ActionResult ItemsIndex()
      {
          List<Item> allItems = Item.GetAll();
          Console.WriteLine("I'm in ItemsIndex()");
          return View("Index", allItems);
      }

      [HttpGet("/items/new")]
      public ActionResult CreateForm()
      {
        Console.WriteLine("I'm in CreateForm()");
          return View();
      }

      [HttpPost("/items")]
      public ActionResult Create()
      {
        Item newItem = new Item (Request.Form["new-item"]);
        Console.WriteLine("I'm in Create()");
        Console.WriteLine("The details are: " + Request.Form["new-item"]);
        newItem.Save();
        List<Item> allItems = Item.GetAll();
        // return RedirectToAction("Index");
        return View("Index", allItems);
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
        Console.WriteLine("I'm in Update()");
          Item thisItem = Item.Find(id);
          return View(thisItem);
      }

      // [HttpPost("/items/{id}/update")]
      // public ActionResult Update(int id)
      // {
      //     Item thisItem = Item.Find(id);
      //     thisItem.Edit(Request.Form["newname"]);
      //     return RedirectToAction("Index");
      // }

      [HttpPost("/items/delete")]
      public ActionResult DeleteAll()
      {
        Console.WriteLine("I'm in CreateForm()");
          Item.DeleteAll();
          return View();
      }





  }

  // public class ItemsController : Controller
  //   {
  //
  //     [HttpGet("/")]
  //     public ActionResult Index()
  //     {
  //       // List<Item> Model = new List<Item> {};
  //       // Model.Save();
  //       return View();
  //     }

      // [Route("/")]
      // public ActionResult Index()
      // // {
      // //     List<Item> allItems = Item.GetAll();
      // //     return View(allItems);
      // // }
      // {
      //   return View();
      // }

        //
        // [HttpGet("/items")]
        // public ActionResult ItemsIndex()
        // {
        //     List<Item> allItems = Item.GetAll();
        //     return View("Index", allItems);
        // }
        //
        // [HttpGet("/items/new")]
        // public ActionResult CreateForm()
        // {
        //     return View();
        // }
        // [HttpPost("/items")]
        // public ActionResult Create()
        // {
        //   Item newItem = new Item (Request.Form["new-item"]);
        //   newItem.Save();
        //   List<Item> allItems = Item.GetAll();
        //   return RedirectToAction("Index");
        // }
        //
        // [HttpGet("/items/{id}")]
        // public ActionResult Details(int id)
        // {
        //     Item item = Item.Find(id);
        //     return View(item);
        // }
        //
        // [HttpGet("/items/{id}/update")]
        // public ActionResult UpdateForm(int id)
        // {
        //     Item thisItem = Item.Find(id);
        //     return View(thisItem);
        // }
        //
        // // [HttpPost("/items/{id}/update")]
        // // public ActionResult Update(int id)
        // // {
        // //     Item thisItem = Item.Find(id);
        // //     thisItem.Edit(Request.Form["newname"]);
        // //     return RedirectToAction("Index");
        // // }
        //
        // [HttpPost("/items/delete")]
        // public ActionResult DeleteAll()
        // {
        //     Item.DeleteAll();
        //     return View();
        // }

    // }
}
