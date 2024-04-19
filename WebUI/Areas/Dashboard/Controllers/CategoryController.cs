using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class CategoryController : Controller
{

    private readonly AppDbConext _context;

    public CategoryController(AppDbConext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    public IActionResult Update(int Id)
    {
        var category = _context.Categories.FirstOrDefault(x=>x.Id == Id);
        return View(category);
    }


    [HttpPost]
    public IActionResult Update(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    public IActionResult Delete()
    {
        return View();
    }
}
