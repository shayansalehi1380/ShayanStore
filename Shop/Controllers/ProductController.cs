using Application.Interface;
using Domain.Entity.Products;
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
        return View();
    }
}