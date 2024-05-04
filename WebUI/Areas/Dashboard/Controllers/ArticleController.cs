using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class ArticleController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly AppDbConext _context;
    private readonly IWebHostEnvironment _env;

    public ArticleController(IHttpContextAccessor httpContext, AppDbConext context, IWebHostEnvironment env)
    {
        _httpContext = httpContext;
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var articles = _context.Articles.Where(x=>x.UserId == userId).Include(x => x.Category).Include(x => x.User).ToList();
        return View(articles);
    }


    public IActionResult Create()
    {
        var categorList = _context.Categories.ToList();
        ViewData["Categories"] = categorList;
        return View();
    }


    [HttpPost]
    public IActionResult Create(Article article, IFormFile photo)
    {

        try
        {
            article.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var path = "/uploads/" + Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            using (var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            article.PhotoUrl = path;
            article.CreatedDate = DateTime.Now;
            article.UpdateDate = DateTime.Now;
            _context.Add(article);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        catch (Exception e)
        {

            return View();
        }

    }

    public IActionResult Update(int id)
    {
        var article = _context.Articles.FirstOrDefault(x => x.Id == id);
        var categorList = _context.Categories.ToList();
        ViewData["Categories"] = categorList;
        return View(article);
    }


    [HttpPost]
    public IActionResult Update(Article article, IFormFile photo)
    {

        try
        {
            article.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (photo != null)
            {
                var path = "/uploads/" + Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                using (var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                article.PhotoUrl = path;
            }
            article.UpdateDate = DateTime.Now;
            _context.Articles.Update(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View();
        }
    }


    public IActionResult Delete()
    {
        return View();
    }

}
