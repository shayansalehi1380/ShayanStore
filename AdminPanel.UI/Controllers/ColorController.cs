using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Colors;
using Domain.Entity.Products.Guaranties;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class ColorController(IUnitOfWork unitOfWork): Controller
    {
        public async Task<ActionResult<List<Color>>> GetAllColor(string searchColor, int tabs = 1)
        {

            ViewBag.selectTab = tabs;

            IQueryable<Color> queryColor = unitOfWork.GenericRepository<Color>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchColor))
            {
                queryColor = queryColor.Where(x => x.Title.Contains(searchColor) || x.Code.Contains(searchColor));
            }

            ViewBag.Color = await queryColor.ToListAsync();
            return View("ManageColor", "Admin");
        }

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
            var color = await unitOfWork.GenericRepository<Color>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (color == null)
            {
                return NotFound();
            }

            color.Title = name;
            color.Code = codecolor;

            await unitOfWork.GenericRepository<Color>().UpdateAsync(color, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Color>> SoftDelete(int id)
        {
            var color = await unitOfWork.GenericRepository<Color>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (color == null)
            {
                return NotFound();
            }

            color.IsDelete = true;

            await unitOfWork.GenericRepository<Color>().UpdateAsync(color, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Color>> Delete(int id)
        {
            var color = await unitOfWork.GenericRepository<Color>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (color == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Color>().DeleteAsync(color, CancellationToken.None);
            return RedirectToAction("ManageColor", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}
