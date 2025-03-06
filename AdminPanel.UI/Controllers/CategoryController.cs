using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<Category>>> GetAllCategory(string? search, int tabs = 2)
        {
            ViewBag.selectTab = tabs;
            IQueryable<Category> query = unitOfWork.GenericRepository<Category>().TableNoTracking;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            ViewBag.Categories = await query.ToListAsync();

            return View("GetAllMainCategory", "MainCategory");
        }

        public async Task<ActionResult> Create(string name)
        {
            await unitOfWork.GenericRepository<Category>().AddAsync(new Category
            {
                Name = name,
            }, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", "MainCategory", new { tabs = 2 });
        }

        public async Task<ActionResult> Update(int id, string name)
        {
            var category = await unitOfWork.GenericRepository<Category>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = name;

            await unitOfWork.GenericRepository<Category>().UpdateAsync(category, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", "MainCategory", new { tabs = 2 });
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
            return RedirectToAction("GetAllMainCategory", "MainCategory", new { tabs = 2 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var category = await unitOfWork.GenericRepository<Category>().GetByIdAsync(id, CancellationToken.None);
            if (category == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Category>().DeleteAsync(category, CancellationToken.None);
            return RedirectToAction("GetAllMainCategory", "MainCategory", new { tabs = 2 });
        }
    }
}