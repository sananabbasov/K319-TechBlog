using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebUI.Data;

namespace WebUI.Controllers;

public class ArticleController : Controller
{
   private readonly AppDbConext _context;

    public ArticleController(AppDbConext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Detail(int id)
    {
        var article = _context.Articles.Include(x=>x.User).FirstOrDefault(x=>x.Id == id);
        return View(article);
    }
}
