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

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Item> Model = new List<Item> {};
      // Model.Save;
      return View("Index", Model);
    }

    [HttpPost("/add")]
    public ActionResult AddItem()
    {
      Item newItem = new Item (Request.Form["new-item"]);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }

  }
}
