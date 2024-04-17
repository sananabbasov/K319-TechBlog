using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{

    private readonly AppDbConext _context;

    public HomeController(AppDbConext context)
    {
 
        _context = context;
    }


    public IActionResult Index()
    {
   
        var articles = _context.Articles.ToList();
        return View(articles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

}
