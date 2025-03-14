using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bulkyApp.Controllers;
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    // public CategoryController(ILogger<CategoryController> logger)
    // {
    //     _logger = logger;
    // }
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }


    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();


        return View(objCategoryList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "Display order cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();


    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? category = _db.Categories.Find(id);
        Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
        Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    // {
    //     return View();
    // }
    [HttpPost]
    public IActionResult Edit(Category obj)
    {

        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();


    }



    // delete

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? category = _db.Categories.Find(id);
        Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
        Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    // {
    //     return View();
    // }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");




    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error");
    }
}
