using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.DiscountCodes;
using Domain.Entity.Products.Guaranties;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class DiscountCodeController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<DiscountCode>>> GetAllDiscountCode(string searchDiscountCode, int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<DiscountCode> queryDiscountCode = unitOfWork.GenericRepository<DiscountCode>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchDiscountCode))
            {
                queryDiscountCode = queryDiscountCode.Where(x => x.Title.Contains(searchDiscountCode));
            }

            ViewBag.DiscountCode = await queryDiscountCode.ToListAsync();
            return RedirectToAction("ManageDiscountCode", "Admin");
        }

        public async Task<ActionResult<DiscountCode>> Create(string name, string code, int amount, int usagelimit, DateTime startdate, DateTime enddate)
        {
            await unitOfWork.GenericRepository<DiscountCode>().AddAsync(new DiscountCode
            {
                Title = name,
                Code = code,
                Amount = amount,
                UsageLimit = usagelimit,
                StartDate = startdate,
                EndDate = enddate,
            }, CancellationToken.None);
            return RedirectToAction("ManageDiscountCode", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<DiscountCode>> Update(int id, string name, int amount, int usagelimit, string code, DateTime startdate, DateTime enddate)
        {
            var discountCode = await unitOfWork.GenericRepository<DiscountCode>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (discountCode == null)
            {
                return NotFound();
            }

            discountCode.Title = name;
            discountCode.Code = code;
            discountCode.Amount = amount;
            discountCode.UsageLimit = usagelimit;
            discountCode.StartDate = startdate;
            discountCode.EndDate = enddate;

            await unitOfWork.GenericRepository<DiscountCode>().UpdateAsync(discountCode, CancellationToken.None);
            return RedirectToAction("ManageDiscountCode", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<DiscountCode>> SoftDelete(int id)
        {
            var discountCode = await unitOfWork.GenericRepository<DiscountCode>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (discountCode == null)
            {
                return NotFound();
            }

            discountCode.IsDelete = true;

            await unitOfWork.GenericRepository<DiscountCode>().UpdateAsync(discountCode, CancellationToken.None);
            return RedirectToAction("ManageDiscountCode", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<DiscountCode>> Delete(int id)
        {
            var discountCode = await unitOfWork.GenericRepository<DiscountCode>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (discountCode == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<DiscountCode>().DeleteAsync(discountCode, CancellationToken.None);
            return RedirectToAction("ManageDiscountCode", "Admin", new { tabs = 1 });
        }
    }
}
