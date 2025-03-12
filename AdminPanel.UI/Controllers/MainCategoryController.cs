using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class MainCategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<MainCategory>>> GetAllMainCategory(string? searchMainCategory,string? searchCategory, int tabs = 1)
        {
            ViewBag.selectTab = tabs;

            IQueryable<MainCategory> queryMainCategory = unitOfWork.GenericRepository<MainCategory>()
                .TableNoTracking
                .Include(x => x.Categories);

            if (!string.IsNullOrEmpty(searchMainCategory))
            {
                queryMainCategory = queryMainCategory.Where(x => x.Title.Contains(searchMainCategory));
            }

            ViewBag.MainCategories = await queryMainCategory.ToListAsync();

            IQueryable<Category> queryCategory = unitOfWork.GenericRepository<Category>()
                .TableNoTracking
                .Include(x=>x.MainCategory);

            if (!string.IsNullOrEmpty(searchCategory))
            {
                queryCategory = queryCategory.Where(x => x.Name.Contains(searchCategory));
            }

            ViewBag.Categories = await queryCategory.ToListAsync();
            return View();
        }

        public async Task<ActionResult> Create(string name)
        {
            await unitOfWork.GenericRepository<MainCategory>().AddAsync(new MainCategory
            {
                Title = name,
            }, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", new { tabs = 1 });
        }

        public async Task<ActionResult> Update(int id, string name)
        {
            var maincategory = await unitOfWork.GenericRepository<MainCategory>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (maincategory == null)
            {
                return NotFound();
            }

            maincategory.Title = name;

            await unitOfWork.GenericRepository<MainCategory>().UpdateAsync(maincategory, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", "MainCategory", new { tabs = 1 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var maincategory =
                await unitOfWork.GenericRepository<MainCategory>().GetByIdAsync(id, CancellationToken.None);
            if (maincategory == null)
            {
                return NotFound();
            }

            maincategory.IsDelete = true;
            await unitOfWork.GenericRepository<MainCategory>().UpdateAsync(maincategory, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", new { tabs = 1 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var maincategory =
                await unitOfWork.GenericRepository<MainCategory>().GetByIdAsync(id, CancellationToken.None);
            if (maincategory == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<MainCategory>().DeleteAsync(maincategory, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", new { tabs = 1 });
        }
    }
}