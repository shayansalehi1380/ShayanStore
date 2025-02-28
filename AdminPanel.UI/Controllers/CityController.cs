using Application.Interface;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AdminPanel.UI.Controllers
{
    public class CityController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult> Create(string name, int stateId)
        {
            await unitOfWork.GenericRepository<City>().AddAsync(new City
            {
                Name = name,
                StateId = stateId,
            }, CancellationToken.None);
            return Ok();
        }

        public async Task<ActionResult> GetAllCity()
        {
            ViewBag.Cities = await unitOfWork.GenericRepository<City>()
                .TableNoTracking
                .Include(x => x.State)
                .ToListAsync();

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
            return Ok(city);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(id, CancellationToken.None);
            if (city == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<City>().DeleteAsync(city, CancellationToken.None);
            return Ok(city);
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
            return Ok(city);
        }
    }
}