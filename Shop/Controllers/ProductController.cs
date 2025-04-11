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
      
        ViewBag.Prods = await _unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x => x.ProductColors)
            .Include(x=>x.CategoryDetail)
            .Where(x => x.Id == id)
            .ToListAsync();

        ViewBag.CurrentProduct = await _unitOfWork.GenericRepository<Product>()
            .TableNoTracking
            .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color) // فقط برای روابط موجودیت‌ها
            .Include(p => p.ProductFeatures)
                .ThenInclude(pf => pf.FeatureDetails)
            .Include(p => p.Brand)
            .Include(p => p.CategoryDetail)
            .FirstOrDefaultAsync(p => p.Id == id);

        ViewBag.CurrentBrand = await _unitOfWork.GenericRepository<Brand>()
            .TableNoTracking
            .FirstOrDefaultAsync(b => b.Id == id);

        return View();
    }
}