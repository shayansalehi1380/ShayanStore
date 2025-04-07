using System.Diagnostics;
using Application.Interface;
using Domain.Entity.Products;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers;

public class HomeController: Controller
{
private readonly IUnitOfWork _unitOfWork;

public HomeController(IUnitOfWork unitOfWork)
{
    _unitOfWork = unitOfWork;
}

public async Task<IActionResult>Index()
    {
        ViewBag.Products = await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x=>x.ProductColors)
            .ToListAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}