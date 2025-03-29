using Application.Interface;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Application.Cities.v1.Commands.SoftDeleteCity;
using Domain.Entity.BasicInfo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Cities.v1.Commands.DeleteCity;
using Application.Cities.v1.Commands.UpdateCity;
using Application.Cities.v1.Commands.CreateCity;

namespace AdminPanel.UI.Controllers
{
    public class CityController(IMediator mediator) : Controller
    {
        public async Task<ActionResult> Create(string name, int stateId)
        {
            var result = await mediator.Send(new CreateCityCommand
            {
                Name = name,
                StateId = stateId
            }, CancellationToken.None);
            return RedirectToAction("ManageProvince","Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> Update(int id, string name, int stateId)
        {
            var result = await mediator.Send(new UpdateCityCommand
            {
                Id = id,
                Name = name,
                StateId = stateId
            },CancellationToken.None);
            return RedirectToAction("ManageProvince", "Admin",new { tabs = 2 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCityCommand
            {
                Id = id
            }, CancellationToken.None);
            return RedirectToAction("ManageProvince", "Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
           var result =  await mediator.Send(new SoftDeleteCityCommand
            {
                Id = id
            },CancellationToken.None);
            return RedirectToAction("ManageProvince", "Admin", new { tabs = 2 });
        }
    }
}