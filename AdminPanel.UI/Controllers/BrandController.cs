using Application.Common;
using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Brands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AdminPanel.UI.Controllers
{
    public class BrandController(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : Controller
    {
        private Upload upload = new Upload(environment);

        public async Task<ActionResult<List<Brand>>> GetAllBrand(string searchBrand, int tabs = 1)
        {
            IQueryable<Brand> queryBrand = unitOfWork.GenericRepository<Brand>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchBrand))
            {
                queryBrand = queryBrand.Where(x => x.Title.Contains(searchBrand));
            }

            ViewBag.Brand = await queryBrand.OrderByDescending(x => x.Id).ToListAsync();

            return View("ManageBrand", "Admin");
        }

        public async Task<ActionResult<Brand>> Create(string name, IFormFile image)
        {
            if (image != null)
            {
                return BadRequest("لطفا یک تصویر آپلود کنید.");
            }

            await unitOfWork.GenericRepository<Brand>().AddAsync(new Brand
            {
                Title = name,
                ImageUri = upload.UploadFile(image, "Brands")
            }, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> Update(int id, string name, IFormFile image)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                .FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (brand == null)
            {
                return NotFound();
            }

            brand.Title = name;
            brand.ImageUri = image != null ? upload.UploadFile(image, "Brands") : brand.ImageUri;

            await unitOfWork.GenericRepository<Brand>().UpdateAsync(brand, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> SoftDelete(int id)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                .FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (brand == null)
            {
                return NotFound();
            }

            brand.IsDelete = true;

            await unitOfWork.GenericRepository<Brand>().UpdateAsync(brand, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                .FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);

            if (brand == null)
            {
                return NotFound();
            }

            await unitOfWork.GenericRepository<Brand>().DeleteAsync(brand, CancellationToken.None);
            return RedirectToAction("ManageBrand", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}