using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.UI.Models;
using Domain.Entity.Users;

namespace AdminPanel.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // if (!User.Identity!.IsAuthenticated)
        // {
        //     return RedirectToAction("AdminDashboard", "Admin");
        // }

        return View();
    }

    public IActionResult Privacy()
    {
        ViewBag.ASGar = "";
        ViewBag.User = new User();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}