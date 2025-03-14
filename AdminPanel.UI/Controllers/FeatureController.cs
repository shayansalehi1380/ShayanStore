using Application.Interface;
using AutoMapper.Features;
using Domain.Entity.Products.Categories;
using Domain.Entity.Products.Features;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class FeatureController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<Feature>>> GetAllFeature(string? searchFeature, string? searchDetailsFeature, int tabs = 1)
        {
            ViewBag.selectTab = tabs;

            IQueryable<Feature> queryFeature = unitOfWork.GenericRepository<Feature>()
                .TableNoTracking
                .Include(x => x.Details);

            if (!string.IsNullOrEmpty(searchFeature))
            {
                queryFeature = queryFeature.Where(x => x.Title.Contains(searchFeature));
            }

            ViewBag.Features = await queryFeature.ToListAsync();

            IQueryable<FeatureDetails> queryDetailsFeature = unitOfWork.GenericRepository<FeatureDetails>()
                .TableNoTracking
                .Include(x => x.Feature);

            if (!string.IsNullOrEmpty(searchDetailsFeature))
            {
                queryDetailsFeature = queryDetailsFeature.Where(x => x.Title.Contains(searchDetailsFeature));
            }

            ViewBag.FeatureDetails = await queryDetailsFeature.ToListAsync();

            return RedirectToAction("ManageFeature", "Admin");
        }

        public async Task<ActionResult> Create(string name, int priority)
        {
            await unitOfWork.GenericRepository<Feature>().AddAsync(new Feature
            {
                Title = name,
                Priority = priority
            }, CancellationToken.None);

            return RedirectToAction("ManageFeature", "Admin", new { tabs = 1 });
        }

        public async Task<ActionResult> Update(int id, string name, int priority)
        {
            var feature = await unitOfWork.GenericRepository<Feature>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (feature == null)
            {
                return NotFound();
            }

            feature.Title = name;
            feature.Priority = priority;

            await unitOfWork.GenericRepository<Feature>().UpdateAsync(feature, CancellationToken.None);
            return RedirectToAction("ManageFeature", "Admin", new { tabs = 1 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var feature =
                await unitOfWork.GenericRepository<Feature>().GetByIdAsync(id, CancellationToken.None);
            if (feature == null)
            {
                return NotFound();
            }

            feature.IsDelete = true;

            await unitOfWork.GenericRepository<Feature>().UpdateAsync(feature, CancellationToken.None);
            return RedirectToAction("ManageFeature", "Admin", new { tabs = 1 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var feature =
                await unitOfWork.GenericRepository<Feature>().GetByIdAsync(id, CancellationToken.None);
            if (feature == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Feature>().DeleteAsync(feature, CancellationToken.None);
            return RedirectToAction("ManageFeature", "Admin", new { tabs = 1 });
        }
    }
}
