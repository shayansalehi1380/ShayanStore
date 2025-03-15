using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Categories;
using Domain.Entity.Products.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class FeatureDetailsController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<FeatureDetails>>> GetAllFeatureDetails(string? searchFeatureDetails,
            int tabs = 2)
        {
            ViewBag.selectTab = tabs;

            IQueryable<FeatureDetails> queryFeatureDetails = unitOfWork.GenericRepository<FeatureDetails>()
                .TableNoTracking
                .Include(x => x.Feature)
                .AsSplitQuery();

            IQueryable<Feature> queryFeature = unitOfWork.GenericRepository<Feature>().TableNoTracking
                .Include(x => x.Details)
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchFeatureDetails))
            {
                queryFeature = queryFeature.Where(x => x.Title.Contains(searchFeatureDetails));
            }

            if (!string.IsNullOrEmpty(searchFeatureDetails))
            {
                queryFeatureDetails = queryFeatureDetails.Where(x =>
                    x.Title.Contains(searchFeatureDetails) || x.Feature.Title.Contains(searchFeatureDetails));
            }

            ViewBag.FeatureDetails = await queryFeatureDetails.ToListAsync();
            ViewBag.Features = await queryFeature.ToListAsync();

            return RedirectToAction("ManageFeature", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Create(string name, int priority, int featureId)
        {
            await unitOfWork.GenericRepository<FeatureDetails>().AddAsync(new FeatureDetails
            {
                Title = name,
                Priority = priority,
                FeatureId = featureId
            }, CancellationToken.None);

            return RedirectToAction("ManageFeature", "Admin", new { tabs = 2, status = FunctionStatus.Success });
        }

        public async Task<ActionResult> Update(int id, string name, int priority, int featureId)
        {
            var featureDetails = await unitOfWork.GenericRepository<FeatureDetails>()
                .GetByIdAsync(id, CancellationToken.None);
            if (featureDetails == null)
            {
                return NotFound();
            }

            featureDetails.Title = name;
            featureDetails.Priority = priority;
            featureDetails.FeatureId = featureId;

            await unitOfWork.GenericRepository<FeatureDetails>().UpdateAsync(featureDetails, CancellationToken.None);

            return RedirectToAction("ManageFeature", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var featureDetails = await unitOfWork.GenericRepository<FeatureDetails>()
                .GetByIdAsync(id, CancellationToken.None);
            if (featureDetails == null)
            {
                return NotFound();
            }

            featureDetails.IsDelete = true;

            await unitOfWork.GenericRepository<FeatureDetails>().UpdateAsync(featureDetails, CancellationToken.None);
            return RedirectToAction("ManageFeature", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var featureDetails = await unitOfWork.GenericRepository<FeatureDetails>()
                .GetByIdAsync(id, CancellationToken.None);
            if (featureDetails == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<FeatureDetails>().DeleteAsync(featureDetails, CancellationToken.None);
            return RedirectToAction("ManageFeature", "Admin", new { tabs = 2 });
        }
    }
}