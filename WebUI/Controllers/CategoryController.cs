using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Controllers;

public class CategoryController : Controller
{
    private readonly AppDbConext _context;

    public CategoryController(AppDbConext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categoires = _context.Categories.ToList();
        return View(categoires);
    }

    [HttpPost]
    public IActionResult Index(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}
