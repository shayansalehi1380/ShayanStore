using Application.Interface;
using Application.States.v1.Commands.CreateState;
using Application.States.v1.Commands.DeleteState;
using Application.States.v1.Commands.SoftDeleteState;
using Application.States.v1.Commands.UpdateState;
using Domain.Entity.BasicInfo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdminPanel.UI.Controllers;

public class StateController(IMediator mediator) : Controller
{
    public async Task<ActionResult> Create(string name)
    {
        var result = await mediator.Send(new CreateStateCommand
        {
            Name = name
        }, CancellationToken.None);
        return RedirectToAction("ManageProvince","Admin",new { tabs = 1 });
    }

    public async Task<ActionResult> Update(int id, string name)
    {
        var result = await mediator.Send(new UpdateStateCommand
        {
            Id = id,
            Name = name
        }, CancellationToken.None);
        return RedirectToAction("ManageProvince", "Admin", new { tabs = 1 });
    }

    public async Task<ActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteStateCommand
        {
            Id = id
        }, CancellationToken.None);
        return RedirectToAction("ManageProvince", "Admin", new { tabs = 1 });
    }

    public async Task<ActionResult> SoftDelete(int id)
    {
        var result = await mediator.Send(new SoftDeleteStateCommand
        {
            Id = id
        },CancellationToken.None);
        return RedirectToAction("ManageProvince", "Admin", new { tabs = 1 });
    }
}