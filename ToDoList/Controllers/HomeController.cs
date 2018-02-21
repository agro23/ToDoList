using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList;

namespace ToDoList.Controllers
{
  // public class HomeController : Controller
  // {
  //
    // [HttpGet("/")]
    // public ActionResult Index()
    // {
    //   List<Item> Model = new List<Item> {};
    //   // Model.Save;
    //   return View("Index", Model);
    // }
  //
  //   [HttpPost("/add")]
  //   public ActionResult AddItem()
  //   {
  //     Item newItem = new Item (Request.Form["new-item"]);
  //     newItem.Save();
  //     List<Item> allItems = Item.GetAll();
  //     return View("Index", allItems);
  //     // return View("Index");
  //
  //     // List<Item> Model = new List<Item> {};
  //     // // Model.Save;
  //     // return View("Index", Model);
  //
  //   }
  //
  // }
  public class ItemsController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        List<Item> Model = new List<Item> {};
        // Model.Save;
        return View("Index", Model);
      }

        [HttpGet("/items")]
        public ActionResult NewIndex()
        {
            List<Item> allItems = Item.GetAll();
            return View("Index", allItems);
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/items")]
        public ActionResult Create()
        {
          Item newItem = new Item (Request.Form["new-item"]);
          newItem.Save();
          List<Item> allItems = Item.GetAll();
          return RedirectToAction("Index");
        }

        [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
            Item item = Item.Find(id);
            return View(item);
        }

        [HttpGet("/items/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
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
            Item.DeleteAll();
            return View();
        }

    }
}
