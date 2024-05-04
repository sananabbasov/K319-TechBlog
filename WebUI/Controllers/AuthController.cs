using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Data;
using WebUI.Dtos;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers;


public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbConext _context;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbConext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var findUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (findUser != null)
        {
            return View();
        }
        User newUser = new()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            ResetToken = Guid.NewGuid().ToString(),
            ResetTokenExpireDate = DateTime.Now
        };
        var res = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (res.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(newUser);
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var findUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if (findUser == null)
        {
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(findUser, loginDto.Password, true, true);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(findUser);
    }

    public IActionResult Forgot()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Forgot(string email)
    {


        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        if (user == null)
        {
            ViewBag.UserNotFound = $"User not found by {email} email address.";
            return View();
        }
        // string resetToken = Guid.NewGuid().ToString();
        // DateTime dateTime = DateTime.Now.AddMinutes(10);
        // user.ResetToken = resetToken;
        // user.ResetTokenExpireDate = dateTime;
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // _context.Users.Update(user);
        // _context.SaveChanges();
        bool res = EmailHelper.ResetPassword(email, token);

        if (res)
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    public IActionResult Reset(string token, string email)
    {
        ResetPasswordDto resetPasswordDto = new()
        {
            Token = token,
            Email = email,
        };

        return View(resetPasswordDto);
    }

    [HttpPost]
    public async Task<IActionResult> Reset(ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordDto);
        }

        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user == null)
        {
            return View(resetPasswordDto);
        }


        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

        if (resetPassResult.Succeeded)
        {
            return RedirectToAction("Login");
        }

        return View(resetPasswordDto);
    }
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

}
