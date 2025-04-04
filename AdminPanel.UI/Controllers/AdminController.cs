using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.ApiResult;
using Application.Common.Utilities;
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
using Domain.Entity.DiscountCodes;
using Domain.Entity.Products;

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
        public async Task<IActionResult> ManageProducts(string? search)
        {
            ViewData["Title"] = "پنل مدیریت | محصولات";

            var query = unitOfWork.GenericRepository<Product>().TableNoTracking
                .Include(x => x.Brand)
                .Include(x => x.CategoryDetail).ThenInclude(q => q!.SubCategory)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .AsSplitQuery();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.FaTitle.Contains(search) || x.EnTitle.Contains(search) || x.UniqueCode.Contains(search));
            }

            var products = await query.ToListAsync();
            ViewBag.Products = products;
            return View();
        }

        public async Task<IActionResult> AddProduct()
        {
            ViewData["Title"] = "پنل مدیریت | افزودن کالا";

            ViewBag.Colors = await unitOfWork.GenericRepository<Color>().TableNoTracking.ToListAsync();
            ViewBag.Guarantees = await unitOfWork.GenericRepository<Guarantee>().TableNoTracking.ToListAsync();
            ViewBag.Brands = await unitOfWork.GenericRepository<Brand>().TableNoTracking.ToListAsync();
            ViewBag.SubCat = await unitOfWork.GenericRepository<SubCategory>().TableNoTracking.ToListAsync();
            return View();
        }


        public async Task<ActionResult> ManageProvince(string? searchCity, string? searchState, int tabs = 1)
        {
            ViewData["Title"] = "پنل مدیریت | شهر و اُستان";

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
            ViewData["Title"] = "پنل مدیریت | کاربران";

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
            ViewData["Title"] = "پنل مدیریت | دسته بندی";

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
            ViewData["Title"] = "پنل مدیریت | ویژگی";

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

        public async Task<ActionResult<List<Guarantee>>> ManageGuarantee(string? searchGuarantee, int tabs = 1,
            FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | گارانتی";

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

        public async Task<ActionResult<List<Color>>> ManageColor(string searchColor, int tabs = 1,
            FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | رنگ";

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

        public async Task<ActionResult<List<Color>>> ManageBrand(string searchBrand, int tabs = 1,
            FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | برند";

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

        public async Task<ActionResult<List<ShippingOption>>> ManageShippingOption(string searchShippingOption,
            int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | روش ارسال";

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<ShippingOption> queryShippingOption = unitOfWork.GenericRepository<ShippingOption>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchShippingOption))
            {
                queryShippingOption = queryShippingOption.Where(x => x.Title.Contains(searchShippingOption));
            }

            ViewBag.ShippingOption = await queryShippingOption.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult<List<CategoryDetail>>> ManageCategoryDetail(string? searchCategoryDetail,
            int tabs = 1, FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | زیرگروه محصولات";

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<CategoryDetail> queryCategoryDetail = unitOfWork.GenericRepository<CategoryDetail>()
                .TableNoTracking
                .Include(x => x.SubCategory)
                .AsSplitQuery();

            IQueryable<SubCategory> querySubCategories = unitOfWork.GenericRepository<SubCategory>().TableNoTracking
                .Include(x => x.CategoryDetails)
                .AsSplitQuery();

            if (!string.IsNullOrEmpty(searchCategoryDetail))
            {
                queryCategoryDetail = queryCategoryDetail.Where(x => x.Title.Contains(searchCategoryDetail));
            }

            if (!string.IsNullOrEmpty(searchCategoryDetail))
            {
                querySubCategories = querySubCategories.Where(x => x.Title.Contains(searchCategoryDetail));
            }

            ViewBag.CategoryDetails = await queryCategoryDetail.OrderByDescending(x => x.Id).ToListAsync();
            ViewBag.SubCategories = await querySubCategories.OrderByDescending(x => x.Id).ToListAsync();

            return View();
        }

        public async Task<ActionResult<List<Wallet>>> ManageWallet(string searchWallet, int tabs = 1,
            FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | کیف پول";

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<Wallet> queryhWallet = unitOfWork.GenericRepository<Wallet>()
                .TableNoTracking
                .Include(w => w.User);

            if (!string.IsNullOrEmpty(searchWallet))
            {
                queryhWallet = queryhWallet.Where(x =>
                    x.User.UserName.Contains(searchWallet) ||
                    x.User.Name.Contains(searchWallet) ||
                    x.User.Family.Contains(searchWallet));
            }


            ViewBag.Wallet = await queryhWallet.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult<List<Wallet>>> ManageDiscountCode(string searchDiscountCode, int tabs = 1,
            FunctionStatus status = FunctionStatus.None)
        {
            ViewData["Title"] = "پنل مدیریت | کد تخفیف";

            ViewBag.selectTab = tabs;
            ViewBag.Status = status;

            IQueryable<DiscountCode> queryDiscountCode = unitOfWork.GenericRepository<DiscountCode>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchDiscountCode))
            {
                queryDiscountCode = queryDiscountCode.Where(x =>
                    x.Title.Contains(searchDiscountCode) || x.Code.Contains(searchDiscountCode));
            }

            ViewBag.DiscountCode = await queryDiscountCode.OrderByDescending(x => x.Id).ToListAsync();
            return View();
        }

        public async Task<ActionResult> UploadImage()
        {
            return View();
        }
    }
}