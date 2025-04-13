using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
    public class UserPanelController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Panel()
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
            return View();
        }
        public async Task<IActionResult> UserInfo()
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
            return View();
        }
        public async Task<IActionResult> Orders()
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
            return View();
        }

        public async Task<IActionResult> Wallet()
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
            return View();
        }

        public async Task<IActionResult> Logout()
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
            return RedirectToAction("Index","Home");
        }
    }
}
