using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.BasicInfo;
using Domain.Entity.Products.Guaranties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class ShippingOptionController(IUnitOfWork unitOfWork) : Controller
    {

        public async Task<ActionResult<ShippingOption>> Create(string name, string price)
        {
            await unitOfWork.GenericRepository<ShippingOption>().AddAsync(new ShippingOption
            {
                Title = name,
                Price = price
            }, CancellationToken.None);
            return RedirectToAction("ManageShippingOption", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<ShippingOption>> Update(int id, string name, string price)
        {
            var shippingOption = await unitOfWork.GenericRepository<ShippingOption>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (shippingOption == null)
            {
                return NotFound();
            }

            shippingOption.Title = name;
            shippingOption.Price = price;

            await unitOfWork.GenericRepository<ShippingOption>().UpdateAsync(shippingOption, CancellationToken.None);
            return RedirectToAction("ManageShippingOption", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<ShippingOption>> SoftDelete(int id)
        {
            var shippingOption = await unitOfWork.GenericRepository<ShippingOption>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (shippingOption == null)
            {
                return NotFound();
            }

            shippingOption.IsDelete = true;

            await unitOfWork.GenericRepository<ShippingOption>().UpdateAsync(shippingOption, CancellationToken.None);
            return RedirectToAction("ManageShippingOption", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<ShippingOption>> Delete(int id)
        {
            var shippingOption = await unitOfWork.GenericRepository<ShippingOption>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (shippingOption == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<ShippingOption>().DeleteAsync(shippingOption, CancellationToken.None);
            return RedirectToAction("ManageShippingOption", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}
