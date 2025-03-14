using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class SubCategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<SubCategory>>> GetAllSubCategory(string? searchSubCategory, int tabs = 2)
        {
            ViewBag.selectTab = tabs;

            IQueryable<SubCategory> querySubCategories = unitOfWork.GenericRepository<SubCategory>()
                .TableNoTracking
            .Include(x => x.Category)
            .AsSplitQuery();

            IQueryable<Category> queryCategories = unitOfWork.GenericRepository<Category>().TableNoTracking
                .Include(x => x.SubCategories)
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchSubCategory))
            {
                queryCategories = queryCategories.Where(x => x.Name.Contains(searchSubCategory));
            }

            if (!string.IsNullOrEmpty(searchSubCategory))
            {
                querySubCategories = querySubCategories.Where(x => x.Title.Contains(searchSubCategory) || x.Category.Name.Contains(searchSubCategory));
            }
            ViewBag.SubCategories = await querySubCategories.ToListAsync();
            ViewBag.Categories = await queryCategories.ToListAsync();

            return RedirectToAction("ManageCategory", "Admin", new { tabs = 3 });
        }

        public async Task<ActionResult> Create(string name, int mainCategoryId)
        {
            await unitOfWork.GenericRepository<SubCategory>().AddAsync(new SubCategory
            {
                Title = name,
                CategoryId = mainCategoryId
            }, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 3 });
        }

        public async Task<ActionResult> Update(int id, string name, int mainCategoryId)
        {
            var category = await unitOfWork.GenericRepository<SubCategory>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }
            category.Title = name;
            category.CategoryId = mainCategoryId;

            await unitOfWork.GenericRepository<SubCategory>().UpdateAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 3 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var category = await unitOfWork.GenericRepository<SubCategory>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            category.IsDelete = true;
            await unitOfWork.GenericRepository<SubCategory>().UpdateAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 3 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var category = await unitOfWork.GenericRepository<SubCategory>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<SubCategory>().DeleteAsync(category, CancellationToken.None);
            return RedirectToAction("ManageCategory", "Admin", new { tabs = 3 });
        }
    }
}
