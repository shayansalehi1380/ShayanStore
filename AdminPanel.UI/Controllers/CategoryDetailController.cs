using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class CategoryDetailController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<CategoryDetail>>> GetAllCategoryDetail(string? searchCategoryDetail, int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {
            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<CategoryDetail> queryCategoryDetail = unitOfWork.GenericRepository<CategoryDetail>()
                .TableNoTracking
            .Include(x => x.SubCategory)
            .AsSplitQuery();

            IQueryable<SubCategory> querySubCategories = unitOfWork.GenericRepository<SubCategory>().TableNoTracking
                .Include(x => x.CategoryDetails)
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchCategoryDetail))
            {
                queryCategoryDetail = queryCategoryDetail.Where(x => x.Title.Contains(searchCategoryDetail));
            }

            if (!string.IsNullOrEmpty(searchCategoryDetail))
            {
                querySubCategories = querySubCategories.Where(x => x.Title.Contains(searchCategoryDetail));
            }
            ViewBag.CategoryDetails = await queryCategoryDetail.ToListAsync();
            ViewBag.SubCategories = await querySubCategories.ToListAsync();

            return RedirectToAction("ManageCategoryDetail", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult> Create(string name, int subCategoryId)
        {
            await unitOfWork.GenericRepository<CategoryDetail>().AddAsync(new CategoryDetail
            {
                Title = name,
                SubCategoryId = subCategoryId
            }, CancellationToken.None);
            return RedirectToAction("ManageCategoryDetail", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult> Update(int id, string name, int subCategoryId)
        {
            var categorydetail = await unitOfWork.GenericRepository<CategoryDetail>().GetByIdAsync(id, CancellationToken.None);
            if (categorydetail == null)
            {
                return NotFound();
            }
            categorydetail.Title = name;
            categorydetail.SubCategoryId = subCategoryId;

            await unitOfWork.GenericRepository<CategoryDetail>().UpdateAsync(categorydetail, CancellationToken.None);
            return RedirectToAction("ManageCategoryDetail", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var categorydetail = await unitOfWork.GenericRepository<CategoryDetail>().GetByIdAsync(id, CancellationToken.None);
            if (categorydetail == null)
            {
                return NotFound();
            }

            categorydetail.IsDelete = true;

            await unitOfWork.GenericRepository<CategoryDetail>().UpdateAsync(categorydetail, CancellationToken.None);
            return RedirectToAction("ManageCategoryDetail", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var categorydetail = await unitOfWork.GenericRepository<CategoryDetail>().GetByIdAsync(id, CancellationToken.None);
            if (categorydetail == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<CategoryDetail>().DeleteAsync(categorydetail, CancellationToken.None);
            return RedirectToAction("ManageCategoryDetail", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}
