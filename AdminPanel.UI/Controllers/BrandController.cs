using Application.Brands.v1.Commands.CreateBrand;
using Application.Brands.v1.Commands.DeleteBrand;
using Application.Brands.v1.Commands.SoftDeleteBrand;
using Application.Brands.v1.Commands.UpdateBrand;
using Application.Common;
using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AdminPanel.UI.Controllers
{
    public class BrandController(IMediator mediator) : Controller
    {

        public async Task<ActionResult<Brand>> Create(string name, IFormFile image)
        {
            var result = await mediator.Send(new CreateBrandCommand
            {
                Name = name,
                Image = image
            }, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> Update(int id, string name, IFormFile image)
        {
            var result = await mediator.Send(new UpdateBrandCommand
            {
                Id = id,
                Name = name,
                Image = image
            }, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> SoftDelete(int id)
        {
            var result = await mediator.Send(new SoftDeleteBrandCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var result = await mediator.Send(new DeleteBrandCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}