using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers;

public class ProductController(IUnitOfWork _unitOfWork) : Controller
{
    public async Task<IActionResult> ProductPage(int id)
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

        var prod = await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x => x.ProductColors).ThenInclude(x => x.Color)
            .Include(x => x.ProductColors).ThenInclude(x => x.Guarantee)
            .Include(x => x.CategoryDetail).ThenInclude(x => x!.SubCategory)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.MainCategory)
            .Include(x => x.Brand)
            .Include(x => x.ImageGalleries)
            .Include(x => x.Offer)
            .Include(x => x.ProductFeatures)
            .ThenInclude(x => x.FeatureDetails)
            .FirstOrDefaultAsync(x => x.Id == id);
        ViewBag.Prod = prod;
        return View();
    }
[HttpGet]
    public async Task<List<Product>> SearchProds(string keyword)
    {
        return await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Where(x => x.FaTitle.Contains(keyword))
            .Take(6)
            .ToListAsync();
    }
}