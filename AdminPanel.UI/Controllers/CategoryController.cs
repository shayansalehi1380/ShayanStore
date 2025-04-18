﻿using Application.Interface;
using Domain.Entity.Products.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<Category>>> GetAllCategory(string? searchCategory, int tabs = 2)
        {

            ViewBag.selectTab = tabs;

            IQueryable<Category> queryCategories = unitOfWork.GenericRepository<Category>()
                .TableNoTracking
                .Include(x => x.MainCategory)
                .AsSplitQuery();

            IQueryable<MainCategory> queryMainCategories = unitOfWork.GenericRepository<MainCategory>().TableNoTracking
                .Include(x => x.Categories)
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchCategory))
            {
                queryMainCategories = queryMainCategories.Where(x => x.Title.Contains(searchCategory));
            }

            if (!string.IsNullOrEmpty(searchCategory))
            {
                queryCategories = queryCategories.Where(x => x.Name.Contains(searchCategory) || x.MainCategory.Title.Contains(searchCategory));
            }
            ViewBag.Categories = await queryCategories.ToListAsync();
            ViewBag.MainCategories = await queryMainCategories.ToListAsync();

            return RedirectToAction("ManageCategory", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Create(string name, int mainCategoryId)
        {
            await unitOfWork.GenericRepository<Category>().AddAsync(new Category
            {
                Name = name,
                MainCategoryId = mainCategoryId
            }, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Update(int id, string name, int mainCategoryId)
        {
            var category = await unitOfWork.GenericRepository<Category>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = name;
            category.MainCategoryId = mainCategoryId;

            await unitOfWork.GenericRepository<Category>().UpdateAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var category = await unitOfWork.GenericRepository<Category>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            category.IsDelete = true;
            await unitOfWork.GenericRepository<Category>().UpdateAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var category = await unitOfWork.GenericRepository<Category>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Category>().DeleteAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 2 });
        }
    }
}