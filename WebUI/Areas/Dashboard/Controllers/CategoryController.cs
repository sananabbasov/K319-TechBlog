using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Data;

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


    public IActionResult Create()
    {
        return View();
    }
}
