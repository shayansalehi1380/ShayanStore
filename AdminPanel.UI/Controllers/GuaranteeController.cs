using Application.Common.ApiResult;
using Application.Guarantees.v1.Commands.CreateGuarantee;
using Application.Guarantees.v1.Commands.DeleteGuarantee;
using Application.Guarantees.v1.Commands.SoftDeleteGuarantee;
using Application.Guarantees.v1.Commands.UpdateGuarantee;
using Application.Interface;
using Domain.Entity.Products.Guaranties;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class GuaranteeController(IMediator mediator) : Controller
    {

        public async Task<ActionResult<Guarantee>> Create(string name)
        {
            var result = await mediator.Send(new CreateGuaranteeCommand
            {
                Name = name
            }, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> Update(int id, string name)
        {
            var result = await mediator.Send(new UpdateGuaranteeCommand
            {
                Id = id,
                Name = name
            }, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> SoftDelete(int id)
        {
            var result = await mediator.Send(new SoftDeleteGuaranteeCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Guarantee>> Delete(int id)
        {
            var result = await mediator.Send(new DeleteGuaranteeCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1 });
        }
    }
}
