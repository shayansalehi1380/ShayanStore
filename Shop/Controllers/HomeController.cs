using System.Diagnostics;
using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
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

        var mcategories = await _unitOfWork.GenericRepository<MainCategory>()
            .TableNoTracking
            .Include(m => m.Categories)
            .ThenInclude(c => c.SubCategories)
            .ToListAsync();

        var categories = await _unitOfWork.GenericRepository<Category>()
            .TableNoTracking
            .Include(x => x.MainCategory)
            .ToListAsync();

        var subcategories = await _unitOfWork.GenericRepository<SubCategory>()
            .TableNoTracking
            .Include(x => x.Category)
            .ToListAsync();

        var firstMainCategory = categories
            .FirstOrDefault(c => c.MainCategory != null)?.MainCategory;

        var brand = await _unitOfWork.GenericRepository<Brand>()
            .TableNoTracking
            .ToListAsync();

        ViewBag.Brands = brand;

        ViewBag.FirstMainCategory = firstMainCategory;
        ViewBag.SubCategory = subcategories;
        ViewBag.Categories = categories;
        ViewBag.MainCategories = mcategories;

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