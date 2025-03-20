using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Guaranties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class GuaranteeController(IUnitOfWork unitOfWork) : Controller
    {

        public async Task<ActionResult<List<Guarantee>>> GetAllGuarantee(string searchGuarantee, int tabs = 1)
        {

            ViewBag.selectTab = tabs;

            IQueryable<Guarantee> queryGuarantee = unitOfWork.GenericRepository<Guarantee>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchGuarantee))
            {
                queryGuarantee = queryGuarantee.Where(x => x.Title.Contains(searchGuarantee));
            }

            ViewBag.Guarantee = await queryGuarantee.ToListAsync();
            return View("ManageGuarantee", "Admin");

        }

        public async Task<ActionResult<Guarantee>> Create(string name)
        {
            await unitOfWork.GenericRepository<Guarantee>().AddAsync(new Guarantee
            {
                Title = name
            }, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> Update(int id, string name)
        {
            var guarantee = await unitOfWork.GenericRepository<Guarantee>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (guarantee == null)
            {
                return NotFound();
            }

            guarantee.Title = name;

            await unitOfWork.GenericRepository<Guarantee>().UpdateAsync(guarantee, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> SoftDelete(int id)
        {
            var guarantee = await unitOfWork.GenericRepository<Guarantee>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (guarantee == null)
            {
                return NotFound();
            }

            guarantee.IsDelete = true;

            await unitOfWork.GenericRepository<Guarantee>().UpdateAsync(guarantee, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> Delete(int id)
        {
            var guarantee = await unitOfWork.GenericRepository<Guarantee>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (guarantee == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Guarantee>().DeleteAsync(guarantee, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1 });
        }
    }
}
