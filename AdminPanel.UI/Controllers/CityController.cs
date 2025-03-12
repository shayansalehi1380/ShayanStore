using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Application.Cities.v1.Commands.SoftDeleteCity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.UI.Controllers
{
    public class CityController(IUnitOfWork unitOfWork,IMediator mediator) : Controller
    {
        public async Task<ActionResult> Create(string name, int stateId)
        {
            await unitOfWork.GenericRepository<City>().AddAsync(new City
            {
                Name = name,
                StateId = stateId,
            }, CancellationToken.None);
            return RedirectToAction("ManageProvince","Admin", new { tabs = 2 });
        }

        public async Task<ActionResult> GetAllCity(string? searchCity, string? searchState, int tabs = 1)
        {
            IQueryable<City> queryCity = unitOfWork.GenericRepository<City>()
                .TableNoTracking
                .Include(x => x.State)
                .AsSplitQuery();

            IQueryable<State> queryState = unitOfWork.GenericRepository<State>().TableNoTracking
                .Include(x => x.Cities)
                .AsSplitQuery();

            ViewBag.selectTab = tabs;

            if (!string.IsNullOrEmpty(searchCity))
            {
                queryCity = queryCity.Where(x => x.Name.Contains(searchCity) || x.State.Name.Contains(searchCity));
            }

            if (!string.IsNullOrEmpty(searchState))
            {
                queryState = queryState.Where(x => x.Name.Contains(searchState));
            }

            ViewBag.Cities = await queryCity.ToListAsync();
            ViewBag.State = await queryState.ToListAsync();
            return View();
        }

        public async Task<ActionResult> Update(int id, string name, int stateId)
        {
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(id, CancellationToken.None);
            if (city == null)
            {
                return NotFound();
            }

            city.Name = name;
            city.StateId = stateId;

            await unitOfWork.GenericRepository<City>().UpdateAsync(city, CancellationToken.None);
            return RedirectToAction("ManageProvince", "Admin",new { tabs = 2 });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(id, CancellationToken.None);
            if (city == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<City>().DeleteAsync(city, CancellationToken.None);
            return RedirectToAction("GetAllCity", new { tabs = 2 });
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
           var result =  await mediator.Send(new SoftDeleteCityCommand
            {
                Id = id
            },CancellationToken.None);
            return RedirectToAction("GetAllCity", new { tabs = 2 });
        }
    }
}