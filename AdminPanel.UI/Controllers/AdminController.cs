using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.ApiResult;
using Application.Interface;
using Application.Users.v1.Commands.LoginAdmin;
using Application.Users.v1.Commands.LogOutAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Users;
using Domain.Entity.BasicInfo;
using Domain.Entity.Products.Categories;
using Domain.Entity.Users;
using Domain.Entity.Products.Features;
using Domain.Entity.Products.Guaranties;
using Domain.Entity.Products.Colors;
using Domain.Entity.Products.Brands;

namespace AdminPanel.UI.Controllers
{
    public class AdminController(
        UserManager<User> userManager,
        IMediator mediator,
        RoleManager<Role> roleManager,
        IUnitOfWork unitOfWork)
        : Controller
    {
        public IActionResult AdminLogin()
        {
            return View();
        }

        public async Task CreateAdmin()
        {
            var user = new User
            {
                UserName = "admin",
                Email = "shayansalehi617@gmail.com",
                CityId = 1,
                ImageName = "admin.png",
                InsertDate = DateTime.UtcNow,
                Sheba = string.Empty,
                NationalCode = string.Empty,
                Name = "admin",
                PhoneNumber = "09015538133",
                ConcurrencyStamp = string.Empty,
                SecurityStamp = string.Empty,
                Family = string.Empty,
            };
            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, "09011155");
            if (!roleManager.Roles.Any(x => x.Name == "Admin"))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginAdminCommand request)
        {
            var result = await mediator.Send(new LoginAdminCommand
            {
                Username = request.Username,
                Password = request.Password
            });
            if (!string.IsNullOrEmpty(result))
            {
                return RedirectToAction("AdminDashboard");
            }

            return RedirectToAction("AdminLogin");
        }

        public async Task LogOut()
        {
            await mediator.Send(new LogOutAdminCommand());
        }

        [Authorize("Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        // _________________________________________________________________________________________________________________________________________________________________

        public async Task<ActionResult> ManageProvince(string? searchCity, string? searchState, int tabs = 1)
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

            ViewBag.Cities = await queryCity.OrderByDescending(x => x.Id).ToListAsync();
            ViewBag.State = await queryState.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }


        public async Task<IActionResult> ManageUser(string? search)
        {
            var users = new List<UserDto>();
            var query = userManager.Users.AsTracking()
                .Include(c => c.City).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.PhoneNumber.Contains(search) || x.Name.Contains(search) || x.UserName.Contains(search));
            }

            var entityUsers = await query.OrderByDescending(x => x.Id).ToListAsync();
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            ViewBag.Cities = await unitOfWork.GenericRepository<City>().TableNoTracking.ToListAsync();

            foreach (var i in entityUsers)
            {
                users.Add(new UserDto
                {
                    City = i.City,
                    Email = i.Email,
                    Family = i.Family,
                    InsertDate = i.InsertDate,
                    Name = i.Name,
                    PhoneNumber = i.PhoneNumber,
                    Roles = await userManager.GetRolesAsync(i),
                    UserName = i.UserName,
                    Id = i.Id
                });
            }

            ViewBag.Users = users;

            return View();
        }


        public async Task<ActionResult<List<MainCategory>>> ManageCategory(string? searchMainCategory,
            string? searchCategory, string? searchSubCategory, int tabs = 1)
        {
            ViewBag.selectTab = tabs;

            IQueryable<MainCategory> queryMainCategory = unitOfWork.GenericRepository<MainCategory>()
                .TableNoTracking
                .Include(x => x.Categories);

            if (!string.IsNullOrEmpty(searchMainCategory))
            {
                queryMainCategory = queryMainCategory.Where(x => x.Title.Contains(searchMainCategory));
            }

            ViewBag.MainCategories = await queryMainCategory.OrderByDescending(x => x.Id).ToListAsync();


            IQueryable<Category> queryCategory = unitOfWork.GenericRepository<Category>()
                .TableNoTracking
                .Include(x => x.MainCategory);

            if (!string.IsNullOrEmpty(searchCategory))
            {
                queryCategory = queryCategory.Where(x => x.Name.Contains(searchCategory));
            }

            ViewBag.Categories = await queryCategory.OrderByDescending(x => x.Id).ToListAsync();


            IQueryable<SubCategory> querySubCategory = unitOfWork.GenericRepository<SubCategory>()
                .TableNoTracking
                .Include(x => x.Category);

            if (!string.IsNullOrEmpty(searchSubCategory))
            {
                querySubCategory = querySubCategory.Where(x => x.Title.Contains(searchSubCategory));
            }

            ViewBag.SubCategories = await querySubCategory.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult> ManageFeature(string? searchFeature, string? searchFeatureDetails,
            FunctionStatus status = FunctionStatus.None, int tabs = 1)
        {
            ViewBag.selectTab = tabs;
            ViewBag.Status = status;
            IQueryable<Feature> queryFeature = unitOfWork.GenericRepository<Feature>()
                .TableNoTracking
                .Include(x => x.Details);

            if (!string.IsNullOrEmpty(searchFeature))
            {
                queryFeature = queryFeature.Where(x => x.Title.Contains(searchFeature));
            }

            ViewBag.Features = await queryFeature.OrderByDescending(x => x.Id).ToListAsync();

            IQueryable<FeatureDetails> queryDetailsFeature = unitOfWork.GenericRepository<FeatureDetails>()
                .TableNoTracking
                .Include(x => x.Feature);

            if (!string.IsNullOrEmpty(searchFeatureDetails))
            {
                queryDetailsFeature = queryDetailsFeature.Where(x => x.Title.Contains(searchFeatureDetails));
            }

            ViewBag.DetailsFeatures = await queryDetailsFeature.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult<List<Guarantee>>> ManageGuarantee(string? searchGuarantee, int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {
            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<Guarantee> queryGuarantee = unitOfWork.GenericRepository<Guarantee>()
                .TableNoTracking
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchGuarantee))
            {
                queryGuarantee = queryGuarantee.Where(x => x.Title.Contains(searchGuarantee));
            }

            ViewBag.Guarantee = await queryGuarantee.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult<List<Color>>> ManageColor(string searchColor, int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<Color> queryColor = unitOfWork.GenericRepository<Color>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchColor))
            {
                queryColor = queryColor.Where(x => x.Title.Contains(searchColor));
            }

            ViewBag.Color = await queryColor.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult<List<Color>>> ManageBrand(string searchBrand, int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {
            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<Brand> queryBrand = unitOfWork.GenericRepository<Brand>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchBrand))
            {
                queryBrand = queryBrand.Where(x => x.Title.Contains(searchBrand));
            }

            ViewBag.Brand = await queryBrand.OrderByDescending(x => x.Id).ToListAsync();

            return View();
        }

        public async Task<ActionResult> UploadImage()
        {
            return View();
        }
    }
}