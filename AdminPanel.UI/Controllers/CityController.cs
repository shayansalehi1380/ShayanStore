﻿using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.UI.Controllers
{
    public class CityController(IUnitOfWork unitOfWork, UserManager<User> userManager) : Controller
    {
        public async Task<ActionResult> Create(string name, int stateId)
        {
            await unitOfWork.GenericRepository<City>().AddAsync(new City
            {
                Name = name,
                StateId = stateId,
            }, CancellationToken.None);
            return RedirectToAction("GetAllCity", new { tabs = 2 });

        }

        public async Task<ActionResult> GetAllCity(int tabs = 1)
        {
            ViewBag.Cities = await unitOfWork.GenericRepository<City>()
                .TableNoTracking
                .Include(x => x.State)
                .ToListAsync();
            ViewBag.State = await unitOfWork.GenericRepository<State>().TableNoTracking.Include(x => x.Cities)
                .ToListAsync();
            ViewBag.selectTab = tabs;
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
            return RedirectToAction("GetAllCity", new { tabs = 2 });
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
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(id, CancellationToken.None);
            if (city == null)
            {
                return NotFound();
            }

            city.IsDelete = true;
            await unitOfWork.GenericRepository<City>().UpdateAsync(city, CancellationToken.None);
            return RedirectToAction("GetAllCity", new { tabs = 2 });

        }
    }
}