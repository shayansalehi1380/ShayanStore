using Application.Colors.v1.Commands.DeleteColor;
using Application.Colors.v1.Commands.SoftDeleteColor;
using Application.Colors.v1.Commands.UpdateColor;
using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Colors;
using Domain.Entity.Products.Guaranties;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class ColorController(IUnitOfWork unitOfWork, IMediator mediator): Controller
    {

        public async Task<ActionResult<Color>> Create(string name, string codecolor)
        {
            await unitOfWork.GenericRepository<Color>().AddAsync(new Color
            {
                Title = name,
                Code = codecolor
            }, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Color>> Update(int id, string name, string codecolor)
        {
            var result = await mediator.Send(new UpdateColorCommand
            {
                Id = id,
                Name = name,
                CodeColor = codecolor
            }, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Color>> SoftDelete(int id)
        {
            var result = await mediator.Send(new SoftDeleteColorCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Color>> Delete(int id)
        {
            var result = await mediator.Send(new DeleteColorCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}
