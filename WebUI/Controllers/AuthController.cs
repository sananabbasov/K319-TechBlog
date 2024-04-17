using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers;


public class AuthController : Controller
{

    public IActionResult Register()
    {
        return View();
    }


    public IActionResult Login()
    {
        return View();
    }



    public IActionResult Forgot()
    {
        return View();
    }

}
