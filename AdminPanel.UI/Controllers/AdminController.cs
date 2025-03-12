using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Interface;
using Application.Users.v1.Commands.LoginAdmin;
using Application.Users.v1.Commands.LogOutAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

       public async Task<ActionResult> UploadImage()
       {
           return View();
       }
        
    }
}