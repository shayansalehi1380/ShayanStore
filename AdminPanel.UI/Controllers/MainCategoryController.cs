using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class MainCategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<MainCategory>>> GetAllMainCategory(string? search, int tabs = 1)
        {
            ViewBag.selectTab = tabs;
            IQueryable<MainCategory> query = unitOfWork.GenericRepository<MainCategory>().TableNoTracking;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search));
            }
            ViewBag.MainCategories = await query.ToListAsync();

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
            return RedirectToAction("GetAllMainCategory", new { tabs = 1 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var maincategory = await unitOfWork.GenericRepository<MainCategory>().GetByIdAsync(id, CancellationToken.None);
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
            var maincategory = await unitOfWork.GenericRepository<MainCategory>().GetByIdAsync(id, CancellationToken.None);
            if (maincategory == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<MainCategory>().DeleteAsync(maincategory, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", new { tabs = 1 });
        }
    }
}
