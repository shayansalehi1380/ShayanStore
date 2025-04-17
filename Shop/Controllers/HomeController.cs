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

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        #region Required

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

        ViewBag.FirstMainCategory = firstMainCategory;
        ViewBag.SubCategory = subcategories;
        ViewBag.Categories = categories;
        ViewBag.MainCategories = mcategories;

        #endregion

        ViewBag.Products = await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x => x.ProductColors)
            .ToListAsync();
        var brand = await _unitOfWork.GenericRepository<Brand>()
            .TableNoTracking
            .ToListAsync();

        ViewBag.Brands = brand;


        return View();
    }

    public async Task<IActionResult> SubCategoryProduct(int id)
    {
        #region Required

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

        ViewBag.FirstMainCategory = firstMainCategory;
        ViewBag.SubCategory = subcategories;
        ViewBag.Categories = categories;
        ViewBag.MainCategories = mcategories;

        #endregion
ViewBag.SubCat = await _unitOfWork.GenericRepository<SubCategory>().TableNoTracking.FirstOrDefaultAsync(x=>x.Id == id);
        ViewBag.Prods = await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x => x.ProductColors)
            .Include(x=>x.CategoryDetail)
            .Where(x => x.CategoryDetail.SubCategoryId == id)
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