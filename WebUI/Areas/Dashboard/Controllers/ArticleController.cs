using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebUI.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class ArticleController : Controller
{

    public IActionResult Index()
    {

        return View();
    }


    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Update()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }

}
