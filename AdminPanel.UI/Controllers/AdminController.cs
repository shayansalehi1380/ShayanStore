using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

namespace AdminPanel.UI.Controllers
{
    public class AdminController(
        UserManager<User> userManager,
        IMediator mediator,
        RoleManager<Role> roleManager,IUnitOfWork unitOfWork)
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

           ViewBag.Cities = await queryCity.OrderByDescending(x=>x.Id).ToListAsync();
           ViewBag.State = await queryState.OrderByDescending(x=>x.Id).ToListAsync();
           return View();
       }


        public async Task<IActionResult> ManageUser(string? search)
        {
            var users = new List<UserDto>();
            var query = userManager.Users.AsTracking()
                .Include(c => c.City).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.PhoneNumber.Contains(search) || x.Name.Contains(search) || x.UserName.Contains(search));
            }

            var entityUsers = await query.ToListAsync();
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            ViewBag.Cities = await unitOfWork.GenericRepository<City>().TableNoTracking.ToListAsync();

            //var newUser = new User
            //{
            //    UserName = "09015538133",
            //    Family = string.Empty,
            //    Email = string.Empty,
            //    PhoneNumber = string.Empty,
            //    Name = string.Empty,
            //    CityId = 1,
            //    Sheba = string.Empty,
            //    ImageName = string.Empty,
            //    InsertDate = DateTime.Now,
            //    NationalCode = string.Empty,
            //    ConcurrencyStamp = string.Empty,
            //    SecurityStamp = string.Empty,
            //    ConfirmCode = string.Empty,
            //};
            //await userManager.CreateAsync(newUser);
            //var newUser1 = new User
            //{
            //    UserName = "0921129482",
            //    Family = string.Empty,
            //    Email = string.Empty,
            //    PhoneNumber = string.Empty,
            //    Name = string.Empty,
            //    CityId = 1,
            //    Sheba = string.Empty,
            //    ImageName = string.Empty,
            //    InsertDate = DateTime.Now,
            //    NationalCode = string.Empty,
            //    ConcurrencyStamp = string.Empty,
            //    SecurityStamp = string.Empty,
            //    ConfirmCode = string.Empty,
            //};
            //await userManager.CreateAsync(newUser1);

            //if (!await roleManager.RoleExistsAsync("Admin"))
            //{
            //    await roleManager.CreateAsync(new Role
            //    {
            //        Name = "Admin",
            //    });
            //}
            //if (!await roleManager.RoleExistsAsync("User"))
            //{
            //    await roleManager.CreateAsync(new Role
            //    {
            //        Name = "User",
            //    });
            //}
            //await userManager.AddToRoleAsync(newUser, "Admin");
            //await userManager.AddToRoleAsync(newUser1, "Admin");
            //await userManager.AddToRoleAsync(newUser1, "User");
            //await userManager.AddToRoleAsync(newUser, "User");



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


        public async Task<ActionResult<List<MainCategory>>> ManageCategory(string? searchMainCategory, string? searchCategory, string? searchSubCategory, int tabs = 1)
        {
            ViewBag.selectTab = tabs;

            IQueryable<MainCategory> queryMainCategory = unitOfWork.GenericRepository<MainCategory>()
                .TableNoTracking
                .Include(x => x.Categories);

            if (!string.IsNullOrEmpty(searchMainCategory))
            {
                queryMainCategory = queryMainCategory.Where(x => x.Title.Contains(searchMainCategory));
            }

            ViewBag.MainCategories = await queryMainCategory.ToListAsync();


            IQueryable<Category> queryCategory = unitOfWork.GenericRepository<Category>()
                .TableNoTracking
                .Include(x => x.MainCategory);

            if (!string.IsNullOrEmpty(searchCategory))
            {
                queryCategory = queryCategory.Where(x => x.Name.Contains(searchCategory));
            }

            ViewBag.Categories = await queryCategory.ToListAsync(); 


            IQueryable<SubCategory> querySubCategory = unitOfWork.GenericRepository<SubCategory>()
                .TableNoTracking
                .Include(x => x.Category);

            if (!string.IsNullOrEmpty(searchSubCategory))
            {
                querySubCategory = querySubCategory.Where(x => x.Title.Contains(searchSubCategory));
            }

            ViewBag.SubCategories = await querySubCategory.ToListAsync();
            return View();
        }

        public async Task<ActionResult> UploadImage()
       {
           return View();
       }
        
    }
}