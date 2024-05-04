using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.ViewComponents;

public class DashboardAuthViewComponent : ViewComponent
{
    private readonly AppDbConext _context;
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<User> _userManager;
    public DashboardAuthViewComponent(AppDbConext context, IHttpContextAccessor httpContext, UserManager<User> userManager)
    {
        _context = context;
        _httpContext = httpContext;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            var  userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByIdAsync(userId);
            return  View(user);
        }
        return View(null);
    }
}
